using BiblioApi.Models;

namespace BiblioApi.DTOs
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Realisateur { get; set; }
        public int Annee { get; set; }
    }

    public static class FilmMapper
    {
        public static FilmDto ToDto(Film film) => new FilmDto
        {
            Id = film.Id,
            Titre = film.Titre,
            Realisateur = film.Realisateur,
            Annee = film.Annee
        };

        public static Film ToEntity(FilmDto dto) => new Film
        {
            Id = dto.Id,
            Titre = dto.Titre,
            Realisateur = dto.Realisateur,
            Annee = dto.Annee
        };
    }
}
