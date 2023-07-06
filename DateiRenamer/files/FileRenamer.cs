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
            Console.WriteLine(fileName);

            if (fileName.StartsWith(currentPrefix))
            {
                string extension = Path.GetExtension(filePath);
                string NOTnewFileName = newPrefix + fileName.Substring(currentPrefix.Length);

                string newFileName = NOTnewFileName.Replace(extension, string.Empty);
                Console.WriteLine(newFileName);
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


    public void ChangefirstExclamationMark(string folderPath)
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

            char[] newfileName = fileName.ToCharArray();

            try
            {
                for (int i = 0; i < newfileName.Length; i++)
                {
                    if (newfileName[i] == '!')
                    {
                        newfileName[i] = '-';

                        break;
                    }

                }

                string newName = new string(newfileName);

                string newFilePath = Path.Combine(folderPath, newName);

                // Datei umbenennen
                File.Move(filePath, newFilePath);

                Console.Write("Neuer Name: ");
                Console.WriteLine(newfileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming file: {fileName}. {ex.Message}");
            }
        }
    }


    /// <summary>
    /// ZahlenBlock aufteilen
    /// </summary>
    /// <param name="folderPath"></param>
    /// 
    // die zahlen block aufteilen

    public void ZahlenblokAufteilen(string folderPath)
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

            try
            {
                string datum = $"{fileName.Substring(0, 2)}-{fileName.Substring(2, 2)}-{fileName.Substring(4, 4)}{fileName.Substring(8)}";

                // Neuen Dateinamen zusammenstellen
                string newFilePath = Path.Combine(folderPath, datum);

                // Datei umbenennen
                File.Move(filePath, newFilePath);

                Console.Write("Neuer Name: ");
                Console.WriteLine(datum);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming file: {fileName}. {ex.Message}");
            }
        }
    }

    // die zahlen verschieben
    public void ZahlenVerschieben(string folderPath)
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

            try
            {
                string img = fileName.Split('-')[0]; // output : img
                string stringZahl = fileName.Split('-')[1]; //output: 123.jpg
                string zahlen = stringZahl.Split(".")[0];  //output : 123
                string jpg = stringZahl.Split(".")[1]; //output: jpg

                string newName = $"{zahlen}-{img}.{jpg}";


                string newFilePath = Path.Combine(folderPath, newName);

                File.Move(filePath, newFilePath);

                Console.Write("Neuer Name: ");
                Console.WriteLine(newName);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming file: {fileName}. {ex.Message}");
            }
        }



    }



}
