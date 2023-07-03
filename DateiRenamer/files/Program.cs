using System;
using System.IO;

public class Programm
{
    public static void Main()
    {
        RenumberRenamer renamer = new RenumberRenamer();
        renamer.execute("\\\\Mac\\Home\\Documents\\Ausbildung\\Schule\\BFK-S\\Projekt\\Files");

        Console.ReadLine();
    }
}
    
