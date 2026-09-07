namespace BiblioApi.Models {
    public class Reservation {
        public int Id { get; set; }
        public int MembreId { get; set; }
        public Membre Membre { get; set; }
        public int SeanceId { get; set; }
        public Seance Seance { get; set; }
    }
}