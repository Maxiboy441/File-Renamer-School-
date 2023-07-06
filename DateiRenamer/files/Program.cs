using System;
using System.IO;

public class Programm
{
    public static void Main()
    {
        printFileRenamer();

        selectRenamer();
        executeSelection();

        Console.ReadLine();
    }

    public static void printFileRenamer()
    {
        Console.WriteLine("############################################################################################");
        Console.WriteLine("    FFFFFF  I  L      EEEEE  RRRRRR  EEEEE  NN      N  AAAAAA  M        M  EEEEE  RRRRRR    ");
        Console.WriteLine("    F       I  L      E      R    R  E      N  N    N  A    A  M  M   M M  E      R    R    ");
        Console.WriteLine("    FFFFF   I  L      E      R    R  E      N    N  N  A    A  M   M M  M  E      R    R    ");
        Console.WriteLine("    F       I  L      EEEEE  RRRRRR  EEEEE  N     N N  AAAAAA  M    M   M  EEEEE  RRRRRR    ");
        Console.WriteLine("    F       I  L      E      R  R    E      N      NN  A    A  M        M  E      R  R      ");
        Console.WriteLine("    F       I  L      E      R   R   E      N       N  A    A  M        M  E      R   R     ");
        Console.WriteLine("    F       I  LLLLL  EEEEE  R    R  EEEEE  N       N  A    A  M        M  EEEEE  R    R    ");
        Console.WriteLine("############################################################################################");
    }

    public static void selectRenamer()
    {
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("What would you like to do?");
        Console.WriteLine("[1] Change name of a single file");
        Console.WriteLine("[2] Change präfix of all file in a directory");
        //Console.WriteLine("[3] Change all stars in filenames of a folder into dashes");
        Console.WriteLine("[4] Replace first exclamation mark with hyphen");
        Console.WriteLine("[5] Fix dataformating for all files in a folder");
        Console.WriteLine("[6] Put präfix behind numbers");
        Console.WriteLine("[7] Renumber filename");
        Console.WriteLine("[8] Rename suffix");

        Console.WriteLine();

        Console.Write("Enter option: ");
    }

    public static void executeSelection()
    {
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                FileRenamer renamer = new FileRenamer();
                string[] questions = new string[] { "Provide an absolute path of the file", "What is the new filename (without extension)" };
                string[] answers = (string[])aksQuestions(questions);

                string absoluteFilePath = answers[0];

                string newName = answers[1];

                if (absoluteFilePath == "" | newName == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer.RenameFile(absoluteFilePath, newName);
                }
                break;

            case "2":
                FileRenamer renamer2 = new FileRenamer();

                string[] questions2 = new string[] { "Provide a path to the folder (or * to get a folder in current directory)", "What is the old präfix" , "What is the new präfix" };
                string[] answers2 = (string[])aksQuestions(questions2);

                string PathToFolder = answers2[0];
                PathToFolder = currentIdentifier(PathToFolder);

                string oldPräfix = answers2[1];

                string newPräfix = answers2[2]; 

                if (PathToFolder == "" | newPräfix == "" | oldPräfix == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer2.ChangePrefixOfFilesInFolder(PathToFolder, oldPräfix, newPräfix);
                }
                break;

            /*case "3":
                FileRenamer renamer3 = new FileRenamer();

                string[] questions3 = new string[] { "Provide a path to the folder: " };
                string[] answers3 = (string[])aksQuestions(questions3);

                string PathToFolder2 = answers3[0];

                PathToFolder2 = currentIdentifier(PathToFolder2);
                if (PathToFolder2 == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer3.ChangefirstStar(PathToFolder2);
                }
                break; */

            case "4":
                FileRenamer renamer4 = new FileRenamer();
                string[] questions4 = new string[] { "Provide a path to the folder: " };
                string[] answers4 = (string[])aksQuestions(questions4);

                string PathToFolder3 = answers4[0];

                PathToFolder3 = currentIdentifier(PathToFolder3);
                if (PathToFolder3 == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer4.ChangefirstExclamationMark(PathToFolder3);
                }
                break;

            case "5":
                FileRenamer renamer5 = new FileRenamer();
                string[] questions5 = new string[] { "Provide a path to the folder: " };
                string[] answers5 = (string[])aksQuestions(questions5);

                string PathToFolder4 = answers5[0];

                PathToFolder4 = currentIdentifier(PathToFolder4);
                if (PathToFolder4 == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer5.ZahlenblokAufteilen(PathToFolder4);
                }
                break;
            case "6":
                FileRenamer renamer6 = new FileRenamer();
                string[] questions6 = new string[] { "Provide a path to the folder: "};
                string[] answers6 = (string[])aksQuestions(questions6);

                string PathToFolder5 = answers6[0];

                PathToFolder5 = currentIdentifier(PathToFolder5);
                if (PathToFolder5 == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer6.ZahlenVerschieben(PathToFolder5);
                }
                break;

            case "7":
                RenumberRenamer renamer7 = new RenumberRenamer();
                string[] questions7 = new string[] { "Provide a path to the folder: "};
                string[] answers7 = (string[])aksQuestions(questions7);

                string PathToFolder6 = answers7[0];

                PathToFolder5 = currentIdentifier(PathToFolder6);
                if (PathToFolder6 == "")
                {
                    Console.WriteLine("Please provide valide data: ");
                }
                else
                {
                    renamer7.execute(PathToFolder6);
                }
                break;

            case "8":
                RenameSuffix renamer8 = new RenameSuffix();
                renamer8.execute();
                break;


            default:
                Console.WriteLine("Please choose a valide option: ");
                executeSelection();
                break;
        }
    }


    public static string currentDir()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string pathCurrent = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;

        Console.WriteLine("Give me the name of the folder in the current directory: ");
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

    public static string aksQuestion(string question)
    {
        Console.WriteLine(question);
        string text = Console.ReadLine();

        return text;
    }

    public static Array aksQuestions(string[] questions)
    {
        string[] answers = new string[questions.Length];
        int count = 0;

        foreach (string question in questions)
        {
            answers[count] = aksQuestion(question);
            count++;
        }

        return answers;
    }
}