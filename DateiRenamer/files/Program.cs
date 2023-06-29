using System;
using System.IO;

public class Programm
{
    public static void Main()
    {
        FileRenamer renamer = new FileRenamer();
        string FilePath = @"/Volumes/data/Berufsschule/E1FI5-DateiRenamer/DateiRenamer/files/testFiles";
        renamer.ChangePrefixOfFilesInFolder(FilePath, "test", "haus");

        renamer.RenameFile(FilePath + "/max.txt", "maus");


        Console.ReadKey();
    }
}
    
