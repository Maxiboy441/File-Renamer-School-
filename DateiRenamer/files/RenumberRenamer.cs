using System.Text.RegularExpressions;

public class RenumberRenamer
{
    private string newFileName;
    private string newFilePath;

    public void execute(string DirectoryPath)
    {
        //catch exceptions
        if (string.IsNullOrEmpty(DirectoryPath)) { throw new ArgumentException("Absolute file path cannot be null or empty."); }
        if (!Directory.Exists(DirectoryPath)) { throw new Exception("Path doesn't exist."); }


        //arrange
        string[] files = Directory.GetFiles(DirectoryPath);
        string[] filenames = getSortedFileNames(files);
        
        //rename
        foreach (string originalFileName in filenames)
        {
            rename(DirectoryPath, originalFileName, filenames.Length);
        }

        Console.WriteLine();
        Console.WriteLine("Executed!");
    }

    private void rename(string DirectoryPath,string originalFileName, int fileNumber)
    {
        //arrange
        for (int i = 1; i <= fileNumber; i++)
        {
            newFileName = Regex.Replace(originalFileName, "^[0-9]+-*", $"{i}-");
            newFilePath = Path.Combine(DirectoryPath, newFileName);
            if (newFileName == originalFileName) { break; }
            if (!File.Exists(newFilePath)) { break; }
        }

        //move
        if (newFileName != originalFileName) {
            string originalFilePath = Path.Combine(DirectoryPath, originalFileName);

            moveFile(originalFilePath, newFilePath);
            Console.WriteLine($"The file '{originalFileName}' was renamed to '{newFileName}'.");
        }
    }

    private void moveFile(string originalFile, string newFilePath)
    {
        try
        {
            File.Move(originalFile, newFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming file: {originalFile}. {ex.Message}");
        }
    }

    private string[] getSortedFileNames(string[] files)
    {
        string[] fileNamesTemp = new string[files.Length];
        int counter2 = 0;
        foreach (string f in files)
        {
            if (Regex.IsMatch(Path.GetFileName(f), "^[0-9]+-*"))
            {
                fileNamesTemp[counter2] = Path.GetFileName(f);
                counter2++;
            }
        }
        string[] fileNames = fileNamesTemp.Where(c => c != null).ToArray();


        Array.Sort(fileNames, (a, b) => int.Parse(Regex.Replace(a, "[^0-9]", "")) - int.Parse(Regex.Replace(b, "[^0-9]", "")));

        return fileNames;
    }
}
