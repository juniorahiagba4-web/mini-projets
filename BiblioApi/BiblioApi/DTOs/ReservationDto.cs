using BiblioApi.Models;

namespace BiblioApi.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int MembreId { get; set; }
        public int SeanceId { get; set; }
    }

    public static class ReservationMapper
    {
        public static ReservationDto ToDto(Reservation reservation) => new ReservationDto
        {
            Id = reservation.Id,
            MembreId = reservation.MembreId,
            SeanceId = reservation.SeanceId
        };

        public static Reservation ToEntity(ReservationDto dto) => new Reservation
        {
            Id = dto.Id,
            MembreId = dto.MembreId,
            SeanceId = dto.SeanceId
        };
    }
}
