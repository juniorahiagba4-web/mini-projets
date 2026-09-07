namespace BiblioApi.Models {
    public class Seance {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}