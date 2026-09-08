using System.Diagnostics;

Console.WriteLine("=== Simulateur de commande ===\n");
Console.WriteLine("Entrez les plats à commander (un par ligne, ligne vide pour terminer) :");

var plats = new List<string>();
string? saisie;
while (!string.IsNullOrWhiteSpace(saisie = Console.ReadLine()))
{
    plats.Add(saisie.Trim());
}

if (plats.Count == 0)
{
    plats.Add("Pizza Margherita");
    Console.WriteLine("Aucun plat saisi, commande par défaut : Pizza Margherita\n");
}

var chrono = Stopwatch.StartNew();

// Task.WhenAll traite toutes les commandes en parallèle : le temps total
// correspond au temps d'UNE seule commande, pas à la somme de toutes.
await Task.WhenAll(plats.Select(TraiterCommandeAsync));

chrono.Stop();
Console.WriteLine($"\nToutes les commandes sont terminées en {chrono.Elapsed.TotalSeconds:F1} s.");
Console.WriteLine("Programme terminé.");

// --- Définition des méthodes ---

async Task TraiterCommandeAsync(string plat)
{
    Console.WriteLine($"[1] Commande reçue : {plat}");

    await PreparerPlatAsync(plat);
    await LivrerAsync(plat);

    Console.WriteLine($"[4] Commande terminée pour {plat} ! Bon appétit 🍕");
}

async Task PreparerPlatAsync(string plat)
{
    Console.WriteLine($"[2] Préparation de {plat} en cours...");
    await Task.Delay(2000); // simule 2 secondes de préparation
    Console.WriteLine($"[2] {plat} prêt !");
}

async Task LivrerAsync(string plat)
{
    Console.WriteLine($"[3] Livraison de {plat} en cours...");
    await Task.Delay(1500); // simule 1.5 secondes de livraison
    Console.WriteLine($"[3] Livraison de {plat} effectuée !");
}
