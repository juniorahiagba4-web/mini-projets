using GestionEtudiants;
using Xunit;

namespace classe_etudiant.Tests;

public class EtudiantTests
{
    [Theory]
    [InlineData(9.99, "Ajourné")]
    [InlineData(10, "Passable")]
    [InlineData(11.99, "Passable")]
    [InlineData(12, "Assez Bien")]
    [InlineData(13.99, "Assez Bien")]
    [InlineData(14, "Bien")]
    [InlineData(15.99, "Bien")]
    [InlineData(16, "Très Bien")]
    [InlineData(20, "Très Bien")]
    public void GetMention_RetourneLaBonneMentionAuxBornes(double moyenne, string mentionAttendue)
    {
        var etudiant = new Etudiant("Test", new[] { moyenne, moyenne });

        Assert.Equal(mentionAttendue, etudiant.GetMention());
    }

    [Fact]
    public void Moyenne_SansNotes_RetourneZero()
    {
        var etudiant = new Etudiant("Test", Array.Empty<double>());

        Assert.Equal(0, etudiant.Moyenne);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(20.01)]
    public void Constructeur_AvecNoteHorsBornes_LeveUneException(double noteInvalide)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Etudiant("Test", new[] { noteInvalide }));
    }
}
