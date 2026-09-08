using System.Text;
using BiblioApi.Data;
using BiblioApi.DTOs;
using BiblioApi.Models;
using BiblioApi.Options;
using BiblioApi.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key manquant dans la configuration.");
var connectionString = builder.Configuration["Database:ConnectionString"]
    ?? throw new InvalidOperationException("Database:ConnectionString manquant dans la configuration.");

builder.Services.AddDbContext<BiblioContext>(options => options.UseSqlite(connectionString));
builder.Services.AddSingleton(new JwtService(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<BiblioApi.Middleware.LoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// ----- Auth -----
app.MapPost("/auth/register", async (RegisterRequest req, BiblioContext db) =>
{
    if (await db.Membres.AnyAsync(m => m.Email == req.Email))
        return Results.Conflict("Un membre avec cet email existe déjà.");

    var membre = new Membre
    {
        Nom = req.Nom,
        Email = req.Email,
        PasswordHash = BCryptHelper.HashPassword(req.Password)
    };
    db.Membres.Add(membre);
    await db.SaveChangesAsync();

    return Results.Created($"/membres/{membre.Id}", MembreMapper.ToDto(membre));
});

app.MapPost("/auth/login", async (LoginRequest req, BiblioContext db, JwtService jwtService) =>
{
    var membre = await db.Membres.FirstOrDefaultAsync(m => m.Email == req.Email);
    if (membre is null || !BCryptHelper.VerifyPassword(req.Password, membre.PasswordHash))
        return Results.Unauthorized();

    var token = jwtService.GenerateToken(membre.Id.ToString(), membre.Email);
    return Results.Ok(new TokenResponse(token));
});

// ----- Films -----
var films = app.MapGroup("/films");
films.MapGet("/", async (BiblioContext db) =>
    (await db.Films.ToListAsync()).Select(FilmMapper.ToDto));

films.MapGet("/{id:int}", async (int id, BiblioContext db) =>
    await db.Films.FindAsync(id) is { } film ? Results.Ok(FilmMapper.ToDto(film)) : Results.NotFound());

films.MapPost("/", async (FilmDto dto, BiblioContext db) =>
{
    var film = FilmMapper.ToEntity(dto);
    film.Id = 0;
    db.Films.Add(film);
    await db.SaveChangesAsync();
    return Results.Created($"/films/{film.Id}", FilmMapper.ToDto(film));
}).RequireAuthorization();

films.MapPut("/{id:int}", async (int id, FilmDto dto, BiblioContext db) =>
{
    var film = await db.Films.FindAsync(id);
    if (film is null) return Results.NotFound();
    film.Titre = dto.Titre;
    film.Realisateur = dto.Realisateur;
    film.Annee = dto.Annee;
    await db.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization();

films.MapDelete("/{id:int}", async (int id, BiblioContext db) =>
{
    var film = await db.Films.FindAsync(id);
    if (film is null) return Results.NotFound();
    db.Films.Remove(film);
    await db.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization();

// ----- Seances -----
var seances = app.MapGroup("/seances");
seances.MapGet("/", async (BiblioContext db) =>
    (await db.Seances.ToListAsync()).Select(SeanceMapper.ToDto));

seances.MapGet("/{id:int}", async (int id, BiblioContext db) =>
    await db.Seances.FindAsync(id) is { } seance ? Results.Ok(SeanceMapper.ToDto(seance)) : Results.NotFound());

seances.MapPost("/", async (SeanceDto dto, BiblioContext db) =>
{
    if (!await db.Films.AnyAsync(f => f.Id == dto.FilmId))
        return Results.BadRequest("FilmId inconnu.");

    var seance = SeanceMapper.ToEntity(dto);
    seance.Id = 0;
    db.Seances.Add(seance);
    await db.SaveChangesAsync();
    return Results.Created($"/seances/{seance.Id}", SeanceMapper.ToDto(seance));
}).RequireAuthorization();

seances.MapDelete("/{id:int}", async (int id, BiblioContext db) =>
{
    var seance = await db.Seances.FindAsync(id);
    if (seance is null) return Results.NotFound();
    db.Seances.Remove(seance);
    await db.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization();

// ----- Reservations -----
var reservations = app.MapGroup("/reservations").RequireAuthorization();
reservations.MapGet("/", async (BiblioContext db) =>
    (await db.Reservations.ToListAsync()).Select(ReservationMapper.ToDto));

reservations.MapPost("/", async (ReservationDto dto, BiblioContext db) =>
{
    if (!await db.Membres.AnyAsync(m => m.Id == dto.MembreId))
        return Results.BadRequest("MembreId inconnu.");
    if (!await db.Seances.AnyAsync(s => s.Id == dto.SeanceId))
        return Results.BadRequest("SeanceId inconnu.");

    var reservation = ReservationMapper.ToEntity(dto);
    reservation.Id = 0;
    db.Reservations.Add(reservation);
    await db.SaveChangesAsync();
    return Results.Created($"/reservations/{reservation.Id}", ReservationMapper.ToDto(reservation));
});

reservations.MapDelete("/{id:int}", async (int id, BiblioContext db) =>
{
    var reservation = await db.Reservations.FindAsync(id);
    if (reservation is null) return Results.NotFound();
    db.Reservations.Remove(reservation);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
