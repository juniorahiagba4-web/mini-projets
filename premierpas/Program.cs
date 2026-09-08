using System;

class Program
{
    const int TentativesMax = 10;
    const string FichierScore = "meilleur_score.txt";

    static void Main(string[] args)
    {
        int? meilleurScore = ChargerMeilleurScore();

        do
        {
            JouerUnePartie(ref meilleurScore);
        } while (DemanderRejouer());

        Console.WriteLine("Merci d'avoir joué !");
    }

    static void JouerUnePartie(ref int? meilleurScore)
    {
        Random rnd = new Random();
        int nombreADeviner = rnd.Next(1, 101); // 1 à 100 inclus
        int tentatives = 0;

        Console.WriteLine($"\nDevinez le nombre entre 1 et 100 ! Vous avez {TentativesMax} tentatives.");
        if (meilleurScore is not null)
            Console.WriteLine($"Meilleur score actuel : {meilleurScore} tentative(s).");

        while (true)
        {
            Console.Write($"Tentative {tentatives + 1}/{TentativesMax} - Votre proposition : ");
            string? saisie = Console.ReadLine();
            if (!int.TryParse(saisie, out int essai))
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

                if (meilleurScore is null || tentatives < meilleurScore)
                {
                    meilleurScore = tentatives;
                    SauvegarderMeilleurScore(tentatives);
                    Console.WriteLine("Nouveau meilleur score !");
                }

                return;
            }

            if (tentatives >= TentativesMax)
            {
                Console.WriteLine($"Perdu ! Vous avez atteint la limite de {TentativesMax} tentatives. Le nombre était {nombreADeviner}.");
                return;
            }
        }
    }

    static bool DemanderRejouer()
    {
        Console.Write("\nVoulez-vous rejouer ? (O/N) : ");
        return (Console.ReadLine() ?? "").Trim().ToUpper() == "O";
    }

    static int? ChargerMeilleurScore()
    {
        if (File.Exists(FichierScore) && int.TryParse(File.ReadAllText(FichierScore), out int score))
            return score;
        return null;
    }

    static void SauvegarderMeilleurScore(int score)
    {
        File.WriteAllText(FichierScore, score.ToString());
    }
}
