using System;
using System.Threading.Tasks;

Console.WriteLine("=== Simulateur de commande ===\n");

// On lance le processus
await TraiterCommandeAsync("Pizza Margherita");

Console.WriteLine("\nProgramme terminé.");

// --- Définition des méthodes ---

async Task TraiterCommandeAsync(string plat)
{
    Console.WriteLine($"[1] Commande reçue : {plat}");

    await PreparerPlatAsync(plat);
    await LivrerAsync();

    Console.WriteLine("[4] Commande terminée ! Bon appétit 🍕");
}

async Task PreparerPlatAsync(string plat)
{
    Console.WriteLine($"[2] Préparation de {plat} en cours...");
    await Task.Delay(2000); // simule 2 secondes de préparation
    Console.WriteLine("[2] Plat prêt !");
}

async Task LivrerAsync()
{
    Console.WriteLine("[3] Livraison en cours...");
    await Task.Delay(1500); // simule 1.5 secondes de livraison
    Console.WriteLine("[3] Livraison effectuée !");
}
