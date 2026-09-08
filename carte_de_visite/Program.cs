// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;

const int LargeurChamp = 34;

// Saisie d'informations
Console.WriteLine("Générateur de carte de visite");
Console.Write("Veuillez entrer votre prenom : ");
string prenom = Console.ReadLine() ?? "";

Console.Write("Veuillez entrer votre nom : ");
string nom = Console.ReadLine() ?? "";

Console.Write("Veuillez entrer votre poste : ");
string poste = Console.ReadLine() ?? "";

string email;
do
{
    Console.Write("Veuillez entrer votre email : ");
    email = Console.ReadLine() ?? "";
    if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        Console.WriteLine("Email invalide, exemple attendu : nom@domaine.com");
} while (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));

// Génération de la carte (les champs trop longs sont tronqués pour ne pas casser le cadre)
string nomComplet = Tronquer($"{prenom} {nom}".ToUpper(), LargeurChamp);
poste = Tronquer(poste, LargeurChamp);
email = Tronquer(email, LargeurChamp);

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

static string Tronquer(string texte, int largeur)
{
    if (texte.Length <= largeur)
        return texte;
    return texte[..(largeur - 1)] + "…";
}
