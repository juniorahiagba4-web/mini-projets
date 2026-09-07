
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

// Classe représentant une tâche
class Tache
{
	private static int _compteurId = 1;
	public int Id { get; set; }
	public string Titre { get; set; }
	public bool Faite { get; set; }
	public DateTime DateCreation { get; set; }

	public Tache()
	{
		Id = _compteurId++;
	}
}

class Program
{
	static string FichierTaches = "tasks.json";
	static List<Tache> Taches = new List<Tache>();

	static void Main()
	{
		ChargerTaches();
		while (true)
		{
			Console.WriteLine("\n--- Gestionnaire de tâches ---");
			Console.WriteLine("1. Lister les tâches");
			Console.WriteLine("2. Ajouter une tâche");
			Console.WriteLine("3. Marquer une tâche comme faite");
			Console.WriteLine("4. Supprimer une tâche");
			Console.WriteLine("5. Quitter");
			Console.Write("Choix : ");
			var choix = Console.ReadLine();
			try
			{
				switch (choix)
				{
					case "1":
						ListerTaches();
						break;
					case "2":
						AjouterTache();
						break;
					case "3":
						MarquerTacheFaite();
						break;
					case "4":
						SupprimerTache();
						break;
					case "5":
						SauvegarderTaches();
						Console.WriteLine("Au revoir !");
						return;
					default:
						Console.WriteLine("Choix invalide.");
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur : {ex.Message}");
			}
		}
	}

	static void ChargerTaches()
	{
		if (File.Exists(FichierTaches))
		{
			try
			{
				string json = File.ReadAllText(FichierTaches);
				Taches = JsonSerializer.Deserialize<List<Tache>>(json) ?? new List<Tache>();
			}
			catch
			{
				Taches = new List<Tache>();
			}
		}
	}

	static void SauvegarderTaches()
	{
		string json = JsonSerializer.Serialize(Taches, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(FichierTaches, json);
	}

	static void ListerTaches()
	{
		if (Taches.Count == 0)
		{
			Console.WriteLine("Aucune tâche trouvée.");
			return;
		}
		foreach (var t in Taches)
		{
			Console.WriteLine($"[{t.Id}] {(t.Faite ? "[X]" : "[ ]")} {t.Titre} (créée le {t.DateCreation:dd/MM/yyyy HH:mm})");
		}
	}

	static void AjouterTache()
	{
		Console.Write("Titre de la tâche : ");
		string titre = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(titre))
		{
			Console.WriteLine("Le titre ne peut pas être vide.");
			return;
		}
		int id = Taches.Count > 0 ? Taches.Max(t => t.Id) + 1 : 1;
		var tache = new Tache { Id = id, Titre = titre, Faite = false, DateCreation = DateTime.Now };
		Taches.Add(tache);
		SauvegarderTaches();
		Console.WriteLine("Tâche ajoutée.");
	}

	static void MarquerTacheFaite()
	{
		Console.Write("Id de la tâche à marquer comme faite : ");
		if (!int.TryParse(Console.ReadLine(), out int id))
		{
			Console.WriteLine("Id invalide.");
			return;
		}
		var tache = Taches.FirstOrDefault(t => t.Id == id);
		if (tache == null)
		{
			Console.WriteLine("Tâche non trouvée.");
			return;
		}
		tache.Faite = true;
		SauvegarderTaches();
		Console.WriteLine("Tâche marquée comme faite.");
	}

	static void SupprimerTache()
	{
		Console.Write("Id de la tâche à supprimer : ");
		if (!int.TryParse(Console.ReadLine(), out int id))
		{
			Console.WriteLine("Id invalide.");
			return;
		}
		var tache = Taches.FirstOrDefault(t => t.Id == id);
		if (tache == null)
		{
			Console.WriteLine("Tâche non trouvée.");
			return;
		}
		Taches.Remove(tache);
		SauvegarderTaches();
		Console.WriteLine("Tâche supprimée.");
	}
}

