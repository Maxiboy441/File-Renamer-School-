using System;
using System.IO;

public class FileRenamer
{
    public static void Main()
    {
    }


    public static void RenameFile(string filePath, string newFileName)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        string directory = Path.GetDirectoryName(filePath);
        string fileExtension = Path.GetExtension(filePath);
        string newFilePath = Path.Combine(directory, newFileName + fileExtension);

        try
        {
            File.Move(filePath, newFilePath);
            Console.WriteLine("File renamed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while renaming the file: {ex.Message}");
        }
    }
}