using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;



[TestFixture]
public class ProgramTests
{
    private string testDirectory;

    [SetUp]
    public void Setup()
    {
        // Erstelle ein temporäres Verzeichnis für die Tests
        testDirectory = Path.Combine(Path.GetTempPath(), "TestDirectory");
        Directory.CreateDirectory(testDirectory);
    }

    [TearDown]
    public void TearDown()
    {
        // Lösche das temporäre Verzeichnis nach den Tests
        Directory.Delete(testDirectory, true);
    }

    [Test]
    public void Test_RenameFiles()
    {
        // Erstelle eine Testdatei im temporären Verzeichnis, die mit "Clipboard" beginnt
        string filePath = Path.Combine(testDirectory, "ClipboardFile.txt");
        File.WriteAllText(filePath, "Test content");

        // Führe die Methode zum Umbenennen der Dateien aus
        Program.Main(null);

        // Überprüfe, ob die Datei umbenannt wurde
        string newFilePath = Path.Combine(testDirectory, "img-File.png");
        Assert.True(File.Exists(newFilePath), "Die Datei wurde nicht richtig umbenannt.");
    }

    [Test]
    public void Test_MoveZahlenblock()
    {
        // Erstelle eine Testdatei im temporären Verzeichnis mit einem gültigen Zahlenblock
        string filePath = Path.Combine(testDirectory, "Renamer001.txt");
        File.WriteAllText(filePath, "Test content");

        // Führe die Methode zum Verschieben des Zahlenblocks aus
        Program.Main(null);

        // Überprüfe, ob der Zahlenblock um 10 erhöht wurde
        string newFilePath = Path.Combine(testDirectory, "Renamer011.txt");
        Assert.True(File.Exists(newFilePath), "Der Zahlenblock wurde nicht korrekt verschoben.");
    }

    [Test]
    public void Test_InsertLeadingZeros()
    {
        // Erstelle eine Testdatei im temporären Verzeichnis, die mit "1-" beginnt
        string filePath = Path.Combine(testDirectory, "1-File.txt");
        File.WriteAllText(filePath, "Test content");

        // Führe die Methode zum Einfügen führender Nullen aus
        Program.Main(null);

        // Überprüfe, ob führende Nullen erfolgreich eingefügt wurden
        string newFilePath = Path.Combine(testDirectory, "1-001-File.txt");
        Assert.True(File.Exists(newFilePath), "Die führenden Nullen wurden nicht korrekt eingefügt.");
    }

    [Test]
    public void FuehrendeNullenEinfuegen_Entfernen_Test()
    {
        // Arrange
        string[] dateien = new[]
        {
            "1-001.txt",
            "1-010.txt",
            "001-001.txt",
            "001-010.txt",
            "2-001.txt",
            "2-010.txt",
        };

        // Act
        foreach (string datei in dateien)
        {
            string pfad = Path.GetDirectoryName(datei);
            string dateiname = Path.GetFileNameWithoutExtension(datei);

            // Prüfen, ob der Dateiname mit "1-" beginnt
            if (dateiname.StartsWith("1-"))
            {
                // Führende Nullen einführen
                string neuerName = dateiname.Substring(3).PadLeft(3, '0');
                neuerName = neuerName.Insert(0, "1-");
                neuerName += Path.GetExtension(datei);
                File.Move(datei, Path.Combine(pfad, neuerName));
            }
            // Prüfen, ob der Dateiname mit "001-" beginnt
            else if (dateiname.StartsWith("001-"))
            {
                // Führende Nullen entfernen
                string neuerName = dateiname.Substring(4).TrimStart('0');
                neuerName = neuerName.Insert(0, "1-");
                neuerName += Path.GetExtension(datei);
                File.Move(datei, Path.Combine(pfad, neuerName));
            }
        }

        // Assert
        Assert.True(File.Exists("1-001.txt"));
        Assert.True(File.Exists("1-010.txt"));
        Assert.True(File.Exists("1-001.txt"));
        Assert.True(File.Exists("1-010.txt"));
        Assert.False(File.Exists("2-001.txt")); // Sollte nicht umbenannt werden
        Assert.False(File.Exists("2-010.txt")); // Sollte nicht umbenannt werden
    }
}


