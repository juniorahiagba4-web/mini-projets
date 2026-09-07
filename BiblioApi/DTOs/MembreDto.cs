using BiblioApi.Models;

namespace BiblioApi.DTOs
{
    public class MembreDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
    }

    public static class MembreMapper
    {
        public static MembreDto ToDto(Membre membre) => new MembreDto
        {
            Id = membre.Id,
            Nom = membre.Nom,
            Email = membre.Email
        };

        public static Membre ToEntity(MembreDto dto) => new Membre
        {
            Id = dto.Id,
            Nom = dto.Nom,
            Email = dto.Email
        };
    }
}
