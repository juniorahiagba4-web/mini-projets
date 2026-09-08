using System;

namespace BibliothequeUtilitaires
{
    class Utilitaires
    {
        // Factorielle récursive
        public static long Factorielle(int n)
        {
            if (n < 0)
                throw new ArgumentException("Le nombre doit être positif.");

            if (n > 20)
                throw new ArgumentException("Le nombre est trop grand : 21! dépasse la capacité d'un long (maximum accepté : 20).");

            if (n == 0 || n == 1)
                return 1;

            return n * Factorielle(n - 1);
        }

        // Vérification nombre premier 
        public static bool EstPremier(int n)
        {
            if (n < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        // PGCD avec algorithme d'Euclide
        public static int PGCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int reste = a % b;
                a = b;
                b = reste;
            }

            return a;
        }

        //  Fibonacci
        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new ArgumentException("Le rang doit être positif.");

            if (n == 0)
                return 0;
            if (n == 1)
                return 1;

            int a = 0, b = 1, c = 0;

            for (int i = 2; i <= n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return b;
        }

       public static void afficherFibonacci(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(Fibonacci(i) + " ");
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static int LireEntier(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out var valeur))
                    return valeur;
                Console.WriteLine("Veuillez entrer un nombre entier valide.");
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== MENU UTILITAIRES =====");
                Console.WriteLine("1. Calculer Factorielle");
                Console.WriteLine("2. Vérifier Nombre Premier");
                Console.WriteLine("3. Calculer PGCD");
                Console.WriteLine("4. Suite de Fibonacci");
                Console.WriteLine("0. Quitter");

                int choix = LireEntier("Votre choix : ");

                if (choix == 0)
                {
                    Console.WriteLine("Au revoir !");
                    break;
                }

                try
                {
                    switch (choix)
                    {
                        case 1:
                            int n = LireEntier("Entrer un nombre : ");
                            Console.WriteLine($"Factorielle({n}) = {Utilitaires.Factorielle(n)}");
                            break;

                        case 2:
                            int p = LireEntier("Entrer un nombre : ");
                            Console.WriteLine(Utilitaires.EstPremier(p)
                                ? "Le nombre est premier."
                                : "Le nombre n'est pas premier.");
                            break;

                        case 3:
                            int a = LireEntier("Entrer le premier nombre : ");
                            int b = LireEntier("Entrer le deuxième nombre : ");
                            Console.WriteLine($"PGCD({a}, {b}) = {Utilitaires.PGCD(a, b)}");
                            break;

                        case 4:
                            int f = LireEntier("Entrer le rang n : ");
                            Console.WriteLine($"Suite de Fibonacci jusqu'au rang {f} :");
                            Utilitaires.afficherFibonacci(f);
                            break;

                        default:
                            Console.WriteLine("Choix invalide.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erreur : {ex.Message}");
                }
            }
        }
    }
}
