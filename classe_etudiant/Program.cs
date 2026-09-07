using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionEtudiants
{
    class Etudiant
    {
        public string Prenom { get; set; }
        public double[] Notes { get; set; }

        // Propriété calculée
        public double Moyenne
        {
            get
            {
                if (Notes == null || Notes.Length == 0)
                    return 0;

                return Notes.Average();
            }
        }

        public Etudiant(string prenom, double[] notes)
        {
            Prenom = prenom;
            Notes = notes;
        }

        // Retourne la mention selon la moyenne
        public string GetMention()
        {
            if (Moyenne >= 16)
                return "Très Bien";
            else if (Moyenne >= 14)
                return "Bien";
            else if (Moyenne >= 12)
                return "Assez Bien";
            else if (Moyenne >= 10)
                return "Passable";
            else
                return "Ajourné";
        }

        // Affichage du bulletin complet
        public void AfficherBulletin()
        {
            Console.WriteLine($"Étudiant : {Prenom}");
            Console.WriteLine("Notes : " + string.Join(" - ", Notes));
            Console.WriteLine($"Moyenne : {Moyenne:F2}");
            Console.WriteLine($"Mention : {GetMention()}");
            Console.WriteLine(new string('-', 30));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Etudiant> etudiants = new List<Etudiant>
            {
                new Etudiant("Alice", new double[] { 15, 18, 14, 16 }),
                new Etudiant("Bruno", new double[] { 12, 11, 13, 10 }),
                new Etudiant("Claire", new double[] { 17, 19, 18, 16 }),
                new Etudiant("David", new double[] { 9, 8, 10, 11 })
            };

            Console.WriteLine("===== BULLETINS =====\n");

            foreach (var etudiant in etudiants)
            {
                etudiant.AfficherBulletin();
            }

            Console.WriteLine("\n===== CLASSEMENT =====\n");

            var classement = etudiants
                .OrderByDescending(e => e.Moyenne)
                .ToList();

            for (int i = 0; i < classement.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {classement[i].Prenom} - {classement[i].Moyenne:F2}");
            }
        }
    }
}
