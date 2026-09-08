using Microsoft.EntityFrameworkCore;
using BiblioApi.Models;

namespace BiblioApi.Data
{
    public class BiblioContext : DbContext
    {
        public BiblioContext(DbContextOptions<BiblioContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Membre)
                .WithMany(m => m.Reservations)
                .HasForeignKey(r => r.MembreId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seance)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SeanceId);
        }
    }
}
