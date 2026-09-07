// See https://aka.ms/new-console-template for more information
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Saisie d'informations
Console.WriteLine("Générateur de carte de visite");
Console.Write("Veuillez entrer votre prenom : ");
string prenom = Console.ReadLine()!;

Console.Write("Veuillez entrer votre nom : ");
string nom = Console.ReadLine()!;

Console.Write("Veuillez entrer votre poste : ");
string poste = Console.ReadLine()!;

Console.Write("Veuillez entrer votre email : ");
string email = Console.ReadLine()!;

// Génération de la carte
string nomComplet = $"{prenom} {nom}".ToUpper();
string ligne_top = new string('▀', 40);
string separateur = new string('▂', 40);
string ligne_bot = new string('▄', 40);

Console.WriteLine();
Console.WriteLine($"▛{ligne_top}▜");
Console.WriteLine($"▌{"CARTE DE VISITE",28}            ▐");
Console.WriteLine($"▌{separateur}▐");
Console.WriteLine($"▌ 👤   {nomComplet,-34}▐"); // -34 align left on 34 char
Console.WriteLine($"▌      {poste,-34}▐");
Console.WriteLine($"▌{separateur}▐");
Console.WriteLine($"▌  📩  {email,-34}▐");
Console.WriteLine($"▙{ligne_bot}▟");

