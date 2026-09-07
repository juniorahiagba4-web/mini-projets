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
                    var res = manager.Rechercher(Console.ReadLine());
                    res.ForEach(c => Console.WriteLine($"{c.Id} - {c.Nom} {c.Prenom}"));
                    break;

                case "4":
                    Console.WriteLine("Liste des contacts :");
                    manager.Lister();
                    Console.Write("ID du contact à modifier : ");
                    int idMod = int.Parse(Console.ReadLine());
                    manager.Modifier(idMod, SaisirContact());
                    break;

                case "5":
                    Console.Write("ID: ");
                    int idSup = int.Parse(Console.ReadLine());
                    manager.Supprimer(idSup);
                    break;

                case "6":
                    Console.Write("Categorie (Ami/Famille/Travail): ");
                    var cat = Enum.Parse<Categorie>(Console.ReadLine(), true);
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
            tel = Console.ReadLine();
        } while (!long.TryParse(tel, out _));

        string email;
        do
        {
            Console.Write("Email: ");
            email = Console.ReadLine();
        } while (!email.Contains("@"));

        Console.Write("Nom: ");
        string nom = Console.ReadLine();

        Console.Write("Prénom: ");
        string prenom = Console.ReadLine();

        Console.Write("Categorie (Ami/Famille/Travail): ");
        var cat = Enum.Parse<Categorie>(Console.ReadLine(), true);

        return new Contact
        {
            Nom = nom,
            Prenom = prenom,
            Telephone = tel,
            Email = email,
            Categorie = cat
        };
    }
}
