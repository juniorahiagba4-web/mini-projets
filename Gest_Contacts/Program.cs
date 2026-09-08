using System;

class Program
{
    static void Ajouter(ContactManager manager)
    {
        var contact = SaisirContact();
        manager.Ajouter(contact);
        Console.WriteLine("Contact ajouté avec succès !");
    }
    static void Main()
    {
        var manager = new ContactManager();

        manager.Stats();

        while (true)
        {
            Console.WriteLine("\n1. Ajouter");
            Console.WriteLine("2. Lister");
            Console.WriteLine("3. Rechercher");
            Console.WriteLine("4. Modifier");
            Console.WriteLine("5. Supprimer");
            Console.WriteLine("6. Exporter");
            Console.WriteLine("0. Quitter");

            Console.Write("Choix: ");
            var choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Ajouter(manager);
                    break;

                case "2":
                    manager.Lister();
                    break;

                case "3":
                    Console.Write("Terme: ");
                    var terme = Console.ReadLine() ?? "";
                    var res = manager.Rechercher(terme);
                    res.ForEach(c => Console.WriteLine($"{c.Id} - {c.Nom} {c.Prenom}"));
                    break;

                case "4":
                    Console.WriteLine("Liste des contacts :");
                    manager.Lister();
                    int idMod = LireEntier("ID du contact à modifier : ");
                    manager.Modifier(idMod, SaisirContact());
                    break;

                case "5":
                    int idSup = LireEntier("ID: ");
                    manager.Supprimer(idSup);
                    break;

                case "6":
                    var cat = LireCategorie("Categorie (Ami/Famille/Travail): ");
                    manager.Exporter(cat);
                    break;

                case "0":
                    return;
            }
        }
    }

    static Contact SaisirContact()
    {
        string tel;
        do
        {
            Console.Write("Téléphone (chiffres uniquement): ");
            tel = Console.ReadLine() ?? "";
        } while (!long.TryParse(tel, out _));

        string email;
        do
        {
            Console.Write("Email: ");
            email = Console.ReadLine() ?? "";
        } while (!email.Contains('@'));

        string nom = LireTexteNonVide("Nom: ");
        string prenom = LireTexteNonVide("Prénom: ");
        var cat = LireCategorie("Categorie (Ami/Famille/Travail): ");

        return new Contact
        {
            Nom = nom,
            Prenom = prenom,
            Telephone = tel,
            Email = email,
            Categorie = cat
        };
    }

    static int LireEntier(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out var valeur))
                return valeur;
            Console.WriteLine("Veuillez entrer un nombre valide.");
        }
    }

    static Categorie LireCategorie(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (Enum.TryParse<Categorie>(Console.ReadLine(), true, out var cat))
                return cat;
            Console.WriteLine("Catégorie invalide. Valeurs possibles : Ami, Famille, Travail.");
        }
    }

    static string LireTexteNonVide(string message)
    {
        string texte;
        do
        {
            Console.Write(message);
            texte = Console.ReadLine() ?? "";
        } while (string.IsNullOrWhiteSpace(texte));
        return texte;
    }
}
