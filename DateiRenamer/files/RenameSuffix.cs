using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

public class RenameSuffix
{
    private string OriginalSuffix = "";
    private string NewSuffix = "";
    private string DirectoryPath = "";
    private int counter = 0;

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

    public void changeSuffix(string directoryPath, string originalSuffix, string newSuffix)
    {
        OriginalSuffix = originalSuffix;
        NewSuffix = newSuffix;
        DirectoryPath = directoryPath;

        //catch exception
        if (string.IsNullOrEmpty(DirectoryPath)) { throw new ArgumentException("Absolute file path cannot be null or empty."); }
        if (string.IsNullOrEmpty(NewSuffix)) { throw new ArgumentException("New name cannot be null or empty."); }
        if (string.IsNullOrEmpty(OriginalSuffix)) { throw new ArgumentException("New name cannot be null or empty."); }
        if (!Directory.Exists(DirectoryPath)) { throw new Exception("Path doesn't exist."); }

        //rename files
        foreach (string fileOld in Directory.GetFiles(DirectoryPath))
        {
            if (OriginalSuffix == "n" || fileOld.EndsWith(OriginalSuffix))
            {
                rename(fileOld);
            }
        }

        //check for subdirectories and rename rekursiv
        foreach (string subdirectoryPath in Directory.GetDirectories(DirectoryPath))
        {
            changeSuffix(subdirectoryPath, OriginalSuffix, NewSuffix);
        }

        //finishing output
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

    private void rename(string fileOld)
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