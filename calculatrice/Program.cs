namespace mini_projet1;

public class calculatrice
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("CALCULATRICE");
        Console.WriteLine("Entrez le premier nombre");
        double n1 = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Entrez le deuxième nombre");
        double n2 = double.Parse(Console.ReadLine()!);

        // Menu des opérations
        Console.WriteLine();
        Console.WriteLine("---- Opérations ----");
        Console.WriteLine(" [1] addition (+)");
        Console.WriteLine(" [2] soustraction (-)");
        Console.WriteLine(" [3] multiplication (x)");
        Console.WriteLine(" [4] division (/)");
        Console.WriteLine("---------------------");

        Console.Write("Votre choix (1 à 4) : ");
        string choix = Console.ReadLine()!;

        // Calcul avec switch expression
        double? resultat = choix switch
        {
            "1" => n1 + n2,
            "2" => n1 - n2,
            "3" => n1 * n2,
            "4" => null,
            _ => throw new InvalidOperationException("Choix Invalide"),
        };
        string symbol = choix switch
        {
            "1" => "+",
            "2" => "-",
            "3" => "*",
            "4" => "/",
            _ => "?"
        };
        // gestion de la division par 0
        if (choix == "4")
        {
            if (n2 == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Erreur : division par 0 imposiible");
                return;
            }
            resultat = n1 / n2;
        }

        Console.WriteLine();
        Console.WriteLine("----------------");
        Console.WriteLine($"|| {n1,6:F2} {symbol} {n2,6:F2} ||");
        Console.WriteLine($"resultat = {resultat,10:F2}");
    }
}

