using System;
using System.IO;

public class Programm
{
    public static void Main()
    {
        Start();
        selection();
        


        //FileRenamer renamer = new FileRenamer();
        //string FilePath = @"/Volumes/data/Berufsschule/E1FI5-DateiRenamer/DateiRenamer/files/testFiles";
        //renamer.ChangePrefixOfFilesInFolder(FilePath, "test", "haus");
        //
        //renamer.RenameFile(FilePath + "/max.txt", "maus");


        Console.ReadKey();
    }

    public static void Start()
    {
        Console.WriteLine("############################################################################################");
        Console.WriteLine("    FFFFFF  I  L       EEEEE  RRRRRR  EEEEE   N       N  AAAAAA  M       M  EEEE  RRRRRR    ");
        Console.WriteLine("    F       I  L       E      R    R  E       N  N    N  A    A  M  M   MM  E     R    R    ");
        Console.WriteLine("    FFFFF   I  L       E      R    R  E       N    N  N  A    A  M   M M M  E     R    R    ");
        Console.WriteLine("    F       I  L       EEEEE  RRRRRR  EEEEE   N     N N  AAAAAA  M    M  M  EEEE  RRRRRR    ");
        Console.WriteLine("    F       I  L       E      R  R    E       N      NN  A    A  M       M  E     R  R      ");
        Console.WriteLine("    F       I  L       E      R   R   E       N       N  A    A  M       M  E     R   R     ");
        Console.WriteLine("    F       I  LLLL    EEEEE  R    R  EEEEE   N       N  A    A  M       M  EEEE  R    R    ");
        Console.WriteLine("############################################################################################");

        Console.WriteLine("Was möchstes du tun?");
        Console.WriteLine("[1]Change name of a single file");
        Console.WriteLine("[2]Change präfix of all file in a directory");
        Console.WriteLine("[3]Change all stars in filenames of a folder into dashes");
        Console.WriteLine("[4]Fix dataformating for all files in a folder");
        Console.WriteLine("[4]Change sufix of all file in a directory [WIP]");
    }

    public static void selection()
    {
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                FileRenamer renamer = new FileRenamer();
                Console.WriteLine("Provide a absolute Path of the file");
                string absoluteFilePath = Console.ReadLine();
                Console.WriteLine("What is the new filename (without extension)");
                string newName = Console.ReadLine();

                if (absoluteFilePath == "" | newName == "")
                {
                    Console.WriteLine("Please provide valide data");
                }
                else
                {
                    renamer.RenameFile(absoluteFilePath, newName);
                }
                break;


            case "2":
                FileRenamer renamer2 = new FileRenamer();
                Console.WriteLine("Provide a Path to the folder (or * to get a folder in current directory)");
                string PathToFolder = Console.ReadLine();
                PathToFolder = currentIdentifier(PathToFolder);
                Console.WriteLine("What is the old präfix");
                string oldPräfix = Console.ReadLine();
                Console.WriteLine("What is the new präfix");
                string newPräfix = Console.ReadLine();

                if (PathToFolder == "" | newPräfix == "" | oldPräfix == "")
                {
                    Console.WriteLine("Please provide valide data");
                }
                else
                {
                    renamer2.ChangePrefixOfFilesInFolder(PathToFolder, oldPräfix, newPräfix);
                }
                break;
            case "3":
                FileRenamer renamer3 = new FileRenamer();
                Console.WriteLine("Provide a Path to the folder");
                string PathToFolder2 = Console.ReadLine();
                PathToFolder2 = currentIdentifier(PathToFolder2);
                if (PathToFolder2 == "")
                {
                    Console.WriteLine("Please provide valide data");
                }
                else
                {
                    renamer3.ChangefirstStar(PathToFolder2);
                }
                break;
            case "4":
                FileRenamer renamer4 = new FileRenamer();
                Console.WriteLine("Provide a Path to the folder");
                string PathToFolder3 = Console.ReadLine();
                PathToFolder3 = currentIdentifier(PathToFolder3);
                if (PathToFolder3 == "")
                {
                    Console.WriteLine("Please provide valide data");
                }
                else
                {
                    renamer4.ChangefirstStar(PathToFolder3);
                }
                break;
            default:
                Console.WriteLine("Please choose a valide option");
                selection();
                break;
        }
    }


    public static string currentDir()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string pathCurrent = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;

        Console.WriteLine("Give me the name of the folder in the current directory");
        string pathOfFolderInCurrent = Console.ReadLine();
        string pathInCurrent = pathCurrent + "/" + pathOfFolderInCurrent;
        return pathInCurrent;
    }

    public static string currentIdentifier(string input)
    {
        string path = "";

        if(input == "*")
        {
            path = currentDir();
            Console.WriteLine(path);
        }
        else
        {
            path = input;
        }

        return path;
    }
}
    
