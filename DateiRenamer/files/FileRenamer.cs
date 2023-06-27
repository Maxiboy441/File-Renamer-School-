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

    //bonus Feaure
    public void RenameFilesInFolder(string folderPath, string newPrefix)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder does not exist.");
            return;
        }

        string[] files = Directory.GetFiles(folderPath);
        int counter = 1;

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);
            string newFileName = $"{newPrefix}{counter}{Path.GetExtension(fileName)}";

            try
            {
                RenameFile(filePath, newFileName);
                Console.WriteLine($"Renamed file: {fileName} to {newFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming file: {fileName}. {ex.Message}");
            }

            counter++;
        }
    }

    public void ChangePrefixOfFilesInFolder(string folderPath, string currentPrefix, string newPrefix)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder does not exist.");
            return;
        }

        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName.StartsWith(currentPrefix))
            {
                string newFileName = newPrefix + fileName.Substring(currentPrefix.Length);

                try
                {
                    RenameFile(filePath, newFileName);
                    Console.WriteLine($"Renamed file: {fileName} to {newFileName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error renaming file: {fileName}. {ex.Message}");
                }
            }
        }

        string[] subfolders = Directory.GetDirectories(folderPath);
        foreach (string subfolder in subfolders)
        {
            ChangePrefixOfFilesInFolder(subfolder, currentPrefix, newPrefix);
        }
    }

}
