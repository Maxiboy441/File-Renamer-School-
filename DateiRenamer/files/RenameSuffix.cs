using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.CompilerServices;

public class RenameSuffix
{
    private string OriginalSuffix = "";
    private string NewSuffix = "";
    private string DirectoryPath = "";

    public void execute()
    {
        DirectoryPath = getPath();
        OriginalSuffix = getOriginalSuffix();
        NewSuffix = getNewSuffix();

        changeSuffix(DirectoryPath, OriginalSuffix, NewSuffix);
    }

    private string getPath()
    {
        Console.WriteLine("Provide a path to the folder:");
        return Console.ReadLine();
    }

    private string getOriginalSuffix()
    {
        Console.WriteLine("Please enter original Suffix or 'n' if is irrelevant:");
        return Console.ReadLine();
    }

    private string getNewSuffix()
    {
        Console.WriteLine("Please enter new Suffix:");
        return Console.ReadLine();
    }

    public void changeSuffix(string DirectoryPath, string OriginalSuffix, string NewSuffix)
    {
        //Fehler abfangen
        if (string.IsNullOrEmpty(DirectoryPath)) { throw new ArgumentException("Absolute file path cannot be null or empty."); }
        if (string.IsNullOrEmpty(NewSuffix)) { throw new ArgumentException("New name cannot be null or empty."); }
        if (string.IsNullOrEmpty(OriginalSuffix)) { throw new ArgumentException("New name cannot be null or empty."); }
        if (!Directory.Exists(DirectoryPath)) { throw new Exception("Path doesn't exist."); }

        //Zur Übersichtlichen Ausgabe
        Console.WriteLine("---");

        //Umbenennen der Files
        int counter = 0;
        foreach (string fileOld in Directory.GetFiles(DirectoryPath))
        {
            if (OriginalSuffix == "n" || fileOld.EndsWith(OriginalSuffix))
            {
                string originalFileName = Path.GetFileName(fileOld);
                string originalFileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileOld);

                string newFilePath = Path.Combine(DirectoryPath, originalFileNameWithoutExtension + "." + NewSuffix);

                if (File.Exists(newFilePath))
                {
                    throw new InvalidOperationException($"A file with the name '{originalFileNameWithoutExtension}' and suffix '{NewSuffix}' already exists.");
                }
             
                moveFile(fileOld, newFilePath);
                
                Console.WriteLine($"The file '{originalFileName}' renamed to '{Path.GetFileName(newFilePath)}'.");
                
                counter++;
            }
        }

        if (counter != 0)
        {
            Console.WriteLine();
            Console.WriteLine("Executed!");
        }
        else 
        { 
            Console.WriteLine($"No file with the suffix '{OriginalSuffix}' found."); 
        }


    }

    private void moveFile(string fileOld, string newFilePath)
    {
        try
        {
            File.Move(fileOld, newFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming file: {fileOld}. {ex.Message}");
        }
    }
}