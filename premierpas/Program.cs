using System;

class Program
{
	static void Main(string[] args)
	{
		//Générer un nombre aléatoire entre 1 et 100
		Random rnd = new Random();
		int nombreADeviner = rnd.Next(1, 101); // 1 à 100 inclus
		int essai = 0;
		int tentatives = 0;
		Console.WriteLine("Devinez le nombre entre 1 et 100 !");
		while (true) // boucle virtuelle
		{
			Console.Write("Votre proposition : ");
			string? saisie = Console.ReadLine();
			if (!int.TryParse(saisie, out essai))
			{
				Console.WriteLine("Veuillez entrer un nombre valide.");
				continue;
			}
			if (essai < 1 || essai > 100)
			{
				Console.WriteLine("Le nombre doit être compris entre 1 et 100.");
				continue;
			}
			tentatives++;
			if (essai > nombreADeviner)
			{
				Console.WriteLine("Trop grand");
			}
			else if (essai < nombreADeviner)
			{
				Console.WriteLine("Trop petit");
			}
			else
			{
				Console.WriteLine($"Bravo, vous avez trouvé en {tentatives} tentative(s) !");
				break;
			}
		}
	}
}

