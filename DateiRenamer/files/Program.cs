using System;
using System.IO;

class Programm
{
    public static void Main(string[] args)
    {
        RenameSuffix renamer = new RenameSuffix();
        renamer.execute();

        Console.ReadLine();
    }
}

