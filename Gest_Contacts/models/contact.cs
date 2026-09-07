using System;

public enum Categorie
{
    Ami,
    Famille,
    Travail
}

public class Contact
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public Categorie Categorie { get; set; }
    public DateTime DateAjout { get; set; } = DateTime.Now;
}