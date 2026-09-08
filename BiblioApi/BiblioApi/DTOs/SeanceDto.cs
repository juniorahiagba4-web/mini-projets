using BiblioApi.Models;

namespace BiblioApi.DTOs
{
    public class SeanceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FilmId { get; set; }
    }

    public static class SeanceMapper
    {
        public static SeanceDto ToDto(Seance seance) => new SeanceDto
        {
            Id = seance.Id,
            Date = seance.Date,
            FilmId = seance.FilmId
        };

        public static Seance ToEntity(SeanceDto dto) => new Seance
        {
            Id = dto.Id,
            Date = dto.Date,
            FilmId = dto.FilmId
        };
    }
}
