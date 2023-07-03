using System.Text.RegularExpressions;

public class RenumberRenamer
{ 
    public void execute(string DirectoryPath)
    {
        //Fehler abfangen
        if (string.IsNullOrEmpty(DirectoryPath)) { throw new ArgumentException("Absolute file path cannot be null or empty."); }
        if (!Directory.Exists(DirectoryPath)) { throw new Exception("Path doesn't exist."); }


        int counter = 1;

        //Umbenennen der Files
        foreach (string originalFile in Directory.GetFiles(DirectoryPath))
        {
            string originalFileName = Path.GetFileName(originalFile);

            if (Regex.IsMatch(originalFileName, "^[0-9]+-*"))
            {
                string newFileName = Regex.Replace(originalFileName, "^[0-9]+-*", $"{counter}-");
                string newFilePath = Path.Combine(DirectoryPath, newFileName);

                Console.WriteLine(newFileName);
                
                if (File.Exists(newFilePath))
                {
                    throw new InvalidOperationException($"A file with the name '{newFileName}' already exists.");
                }

                File.Move(originalFile, newFilePath);
                Console.WriteLine($"The file '{originalFileName}' renamed to '{newFileName}'.");
                
                counter++;
            }

            
        }

        Console.WriteLine();
        Console.WriteLine("Executed!");
    }
}
