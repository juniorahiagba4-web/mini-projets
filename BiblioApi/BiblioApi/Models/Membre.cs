namespace BiblioApi.Models {
    public class Membre {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}