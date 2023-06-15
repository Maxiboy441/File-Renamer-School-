using System;
using System.IO;

public class Programm
{
    public static void Main()
    {
        FileRenamer renamer = new FileRenamer();
        string absoluteFilePath = @"/Volumes/data/Berufsschule/E1FI5-DateiRenamer/DateiRenamer/files/testFiles/testfile.txt";
        string newName = "haus";
        renamer.RenameFile(absoluteFilePath, newName);


        Console.ReadKey();
    }
}
    
