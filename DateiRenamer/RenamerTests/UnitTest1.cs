using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class FileRenamerTests
{
    private string testFolderPath;
    private FileRenamer fileRenamer;

    [SetUp]
    public void Setup()
    {
        testFolderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFolder");
        Directory.CreateDirectory(testFolderPath);
        fileRenamer = new FileRenamer();
    }

    [TearDown]
    public void TearDown()
    {
        Directory.Delete(testFolderPath, true);
    }

    [Test]
    public void RenameFile_ThrowsException_WhenAbsoluteFilePathIsNull()
    {
        // Arrange
        string absoluteFilePath = null;
        string newName = "NewName";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => fileRenamer.RenameFile(absoluteFilePath, newName));
    }

    [Test]
    public void RenameFile_ThrowsException_WhenAbsoluteFilePathIsEmpty()
    {
        // Arrange
        string absoluteFilePath = string.Empty;
        string newName = "NewName";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => fileRenamer.RenameFile(absoluteFilePath, newName));
    }

    [Test]
    public void RenameFile_ThrowsException_WhenNewNameIsNull()
    {
        // Arrange
        string absoluteFilePath = "C:\\Path\\To\\File.txt";
        string newName = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => fileRenamer.RenameFile(absoluteFilePath, newName));
    }

    [Test]
    public void RenameFile_ThrowsException_WhenNewNameIsEmpty()
    {
        // Arrange
        string absoluteFilePath = "C:\\Path\\To\\File.txt";
        string newName = string.Empty;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => fileRenamer.RenameFile(absoluteFilePath, newName));
    }

    [Test]
    public void RenameFile_ThrowsException_WhenFileWithNewNameAlreadyExists()
    {
        // Arrange
        string filePath = Path.Combine(testFolderPath, "File.txt");
        string newFilePath = Path.Combine(testFolderPath, "NewFile.txt");
        File.WriteAllText(filePath, "File content");
        File.WriteAllText(newFilePath, "Existing file content");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => fileRenamer.RenameFile(filePath, "NewFile"));
    }

    [Test]
    public void RenameFile_RenamesFile_WhenValidArgumentsProvided()
    {
        // Arrange
        string filePath = Path.Combine(testFolderPath, "File.txt");
        string newFilePath = Path.Combine(testFolderPath, "NewFile.txt");
        File.WriteAllText(filePath, "File content");

        // Act
        fileRenamer.RenameFile(filePath, "NewFile");

        // Assert
        Assert.IsFalse(File.Exists(filePath));
        Assert.IsTrue(File.Exists(newFilePath));
    }

    [Test]
    public void ChangePrefixOfFilesInFolder_RenamesFilesWithMatchingPrefix()
    {
        // Arrange
        string file1 = Path.Combine(testFolderPath, "Prefix_File1.txt");
        string file2 = Path.Combine(testFolderPath, "Prefix_File2.txt");
        string file3 = Path.Combine(testFolderPath, "File3.txt");
        File.WriteAllText(file1, "File1 content");
        File.WriteAllText(file2, "File2 content");
        File.WriteAllText(file3, "File3 content");

        string currentPrefix = "Prefix";
        string newPrefix = "NewPrefix";

        // Act
        fileRenamer.ChangePrefixOfFilesInFolder(testFolderPath, currentPrefix, newPrefix);

        // Assert
        Assert.IsTrue(File.Exists(Path.Combine(testFolderPath, "NewPrefix_File1.txt")));
        Assert.IsTrue(File.Exists(Path.Combine(testFolderPath, "NewPrefix_File2.txt")));
        Assert.IsTrue(File.Exists(file3)); // File3 should not be renamed
    }
}
