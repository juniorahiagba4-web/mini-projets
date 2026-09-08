using System.Globalization;

namespace mini_projet1;

public class Calculatrice
{
    static double LireDouble(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            var saisie = Console.ReadLine();
            if (double.TryParse(saisie, NumberStyles.Float, CultureInfo.InvariantCulture, out var valeur))
                return valeur;
            Console.WriteLine("Veuillez entrer un nombre valide (ex: 3.14).");
        }
    }

    static string LireChoixOperation()
    {
        while (true)
        {
            Console.Write("Votre choix (1 à 4) : ");
            var choix = Console.ReadLine();
            if (choix is "1" or "2" or "3" or "4")
                return choix;
            Console.WriteLine("Choix invalide, entrez un nombre entre 1 et 4.");
        }
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("CALCULATRICE");

        do
        {
            double n1 = LireDouble("Entrez le premier nombre");
            double n2 = LireDouble("Entrez le deuxième nombre");

            // Menu des opérations
            Console.WriteLine();
            Console.WriteLine("---- Opérations ----");
            Console.WriteLine(" [1] addition (+)");
            Console.WriteLine(" [2] soustraction (-)");
            Console.WriteLine(" [3] multiplication (x)");
            Console.WriteLine(" [4] division (/)");
            Console.WriteLine("---------------------");

            string choix = LireChoixOperation();

            string symbol = choix switch
            {
                "1" => "+",
                "2" => "-",
                "3" => "*",
                "4" => "/",
                _ => "?"
            };

            if (choix == "4" && n2 == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Erreur : division par 0 impossible");
            }
            else
            {
                double resultat = choix switch
                {
                    "1" => n1 + n2,
                    "2" => n1 - n2,
                    "3" => n1 * n2,
                    "4" => n1 / n2,
                    _ => 0
                };

                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine($"|| {n1,6:F2} {symbol} {n2,6:F2} ||");
                Console.WriteLine($"resultat = {resultat,10:F2}");
            }

            Console.Write("\nEffectuer un autre calcul ? (O/N) : ");
        } while ((Console.ReadLine() ?? "").Trim().ToUpper() == "O");

        Console.WriteLine("Au revoir !");
    }
}
