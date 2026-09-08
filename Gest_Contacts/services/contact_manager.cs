using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class ContactManager
{
    private List<Contact> contacts = new List<Contact>();
    private int nextId = 1;
    private string filePath = "contacts.json";

    public ContactManager()
    {
        Charger();
    }

    private void Charger()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
                nextId = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;
            }
        }
        catch
        {
            Console.WriteLine("⚠️ Fichier JSON corrompu. Réinitialisation.");
            contacts = new List<Contact>();
            nextId = 1;
        }
    }

    private void Sauvegarder()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(contacts, options));
    }

    // ----------------- AJOUT -----------------
    public void Ajouter(Contact contact)
    {
        contact.Id = nextId++;
        contact.DateAjout = DateTime.Now;
        contacts.Add(contact);
        Sauvegarder();
    }

    // ----------------- LISTE -----------------
    public void Lister(int page = 1, int pageSize = 10)
    {
        var paged = contacts.Skip((page - 1) * pageSize).Take(pageSize);

        Console.WriteLine("ID | Nom        | Prenom     | Tel        | Email                | Catégorie | Date d'ajout");
        Console.WriteLine("-----------------------------------------------------------------------------------------------");

        foreach (var c in paged)
        {
            Console.WriteLine($"{c.Id,-3}| {c.Nom,-10}| {c.Prenom,-10}| {c.Telephone,-10}| {c.Email,-20}| {c.Categorie,-10}| {c.DateAjout:yyyy-MM-dd}");
        }

        // Statistiques par catégorie
        Console.WriteLine("\nStatistiques par catégorie :");
        foreach (Categorie cat in Enum.GetValues(typeof(Categorie)))
        {
            int count = contacts.Count(c => c.Categorie == cat);
            Console.WriteLine($"{cat} : {count}");
        }
    }

    // ----------------- RECHERCHE -----------------
    public List<Contact> Rechercher(string terme)
    {
        terme ??= "";
        return contacts.Where(c =>
            (c.Nom ?? "").Contains(terme, StringComparison.OrdinalIgnoreCase) ||
            (c.Prenom ?? "").Contains(terme, StringComparison.OrdinalIgnoreCase) ||
            c.Categorie.ToString().Equals(terme, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    // ----------------- MODIFIER -----------------
    public void Modifier(int id, Contact updated)
    {
        var contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            Console.WriteLine("Contact introuvable.");
            return;
        }

        contact.Nom = updated.Nom;
        contact.Prenom = updated.Prenom;
        contact.Telephone = updated.Telephone;
        contact.Email = updated.Email;
        contact.Categorie = updated.Categorie;

        Sauvegarder();
        Console.WriteLine("Contact modifié avec succès !");
    }

    // ----------------- SUPPRIMER -----------------
    public void Supprimer(int id)
    {
        var contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            Console.WriteLine("Contact introuvable.");
            return;
        }

        Console.Write("Confirmer suppression (O/N): ");
        if ((Console.ReadLine() ?? "").Trim().ToUpper() == "O")
        {
            contacts.Remove(contact);
            Sauvegarder();
            Console.WriteLine("Contact supprimé avec succès !");
        }
    }

    // ----------------- EXPORT -----------------
    public void Exporter(Categorie cat)
    {
        var data = contacts.Where(c => c.Categorie == cat).ToList();
        string file = $"export_{cat}.json";

        File.WriteAllText(file, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));

        Console.WriteLine("Export terminé.");
    }

    // ----------------- STATS -----------------
    public void Stats()
    {
        Console.WriteLine($"Total: {contacts.Count}");
        foreach (Categorie c in Enum.GetValues(typeof(Categorie)))
        {
            Console.WriteLine($"{c}: {contacts.Count(x => x.Categorie == c)}");
        }
    }
}