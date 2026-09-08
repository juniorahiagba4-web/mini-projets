namespace BiblioApi.Models {
    public class Film {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Realisateur { get; set; }
        public int Annee { get; set; }
        public ICollection<Seance> Seances { get; set; }
    }
}