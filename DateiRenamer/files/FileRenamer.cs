using System;
using System.IO;

public class FileRenamer
{
    public void RenameFile(string absoluteFilePath, string newName)
    {
        if (string.IsNullOrEmpty(absoluteFilePath))
        {
            throw new ArgumentException("Absolute file path cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(newName))
        {
            throw new ArgumentException("New name cannot be null or empty.");
        }

        string directory = Path.GetDirectoryName(absoluteFilePath);
        string fileName = Path.GetFileName(absoluteFilePath);
        string extension = Path.GetExtension(absoluteFilePath);

        string newFilePath = Path.Combine(directory, newName + extension);

        if (File.Exists(newFilePath))
        {
            throw new InvalidOperationException($"A file with the name '{newName}' already exists.");
        }

        File.Move(absoluteFilePath, newFilePath);
        Console.WriteLine($"File '{fileName}' renamed to '{Path.GetFileName(newFilePath)}'.");
    }
}
