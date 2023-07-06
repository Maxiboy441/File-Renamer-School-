using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class RenameSuffixTest
{
    private const string OriginalFileName1 = "testfile1.txt";
    private const string NewFileName1 = "testfile1.jpg";
    private const string OriginalFileName2 = "testfile2.txt";
    private const string NewFileName2 = "testfile2.jpg";
    private const string OriginalFileName3 = "testfile3.txt";
    private const string NewFileName3 = "testfile3.jpg";

    private const string OriginalFileSuffix = "txt";
    private const string NewFileSuffix = "jpg";

    private string TestDirectoryPath = "";
    private string SubTestDirectoryPath = "";

    private RenameSuffix Renamer = new RenameSuffix();

    [SetUp]
    public void Setup()
    {
        TestDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestDirectory");
        Directory.CreateDirectory(TestDirectoryPath);

        string path1 = Path.Combine(TestDirectoryPath, OriginalFileName1);
        string path2 = Path.Combine(TestDirectoryPath, OriginalFileName2);

        File.WriteAllText(path1, "Text1");
        File.WriteAllText(path2, "Text2");

        //subdirectory
        SubTestDirectoryPath = Path.Combine(TestDirectoryPath, "test");
        Directory.CreateDirectory(SubTestDirectoryPath);

        string subPath1 = Path.Combine(SubTestDirectoryPath, OriginalFileName3);

        File.WriteAllText(subPath1, "Text3");
    }

    [TearDown]
    public void Cleanup()
    {
        Directory.Delete(TestDirectoryPath, true);
    }

    [Test]
    public void RenameSuffix_ShouldRenameFile()
    {
        // Arrange
        string originalFilePath1 = Path.Combine(TestDirectoryPath, OriginalFileName1);
        string renamedFilePath1 = Path.Combine(TestDirectoryPath, NewFileName1);
        string originalFilePath2 = Path.Combine(TestDirectoryPath, OriginalFileName2);
        string renamedFilePath2 = Path.Combine(TestDirectoryPath, NewFileName2);
        string originalFilePath3 = Path.Combine(SubTestDirectoryPath, OriginalFileName3);
        string renamedFilePath3 = Path.Combine(SubTestDirectoryPath, NewFileName3);

        // Act
        Renamer.changeSuffix(TestDirectoryPath, OriginalFileSuffix, NewFileSuffix);

        // Assert
        Assert.IsFalse(File.Exists(originalFilePath1), "Original file 1 should not exist");
        Assert.IsTrue(File.Exists(renamedFilePath1), "Renamed file 1 should exist");

        Assert.IsFalse(File.Exists(originalFilePath2), "Original file 2 should not exist");
        Assert.IsTrue(File.Exists(renamedFilePath2), "Renamed file 2 should exist");

        Assert.IsFalse(File.Exists(originalFilePath3), "Original file 3 should not exist");
        Assert.IsTrue(File.Exists(renamedFilePath3), "Renamed file 3 should exist");
    }

    [Test]
    public void RenameSuffix_ThrowsException_WhenAbsoluteFilePathIsNull()
    {
        // Arrange
        string absoluteFilePath = null;

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => Renamer.changeSuffix(absoluteFilePath, OriginalFileSuffix, NewFileSuffix));
    }

    [Test]
    public void RenameSuffix_ThrowsException_WhenAbsoluteFilePathIsEmpty()
    {
        // Arrange
        string absoluteFilePath = string.Empty;

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => Renamer.changeSuffix(absoluteFilePath, OriginalFileSuffix, NewFileSuffix));
    }

    [Test]
    public void RenameSuffix_ThrowsException_WhenOriginalFileSuffixIsNull()
    {
        // Arrange
        string originalFileSuffix = null;

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => Renamer.changeSuffix(TestDirectoryPath, originalFileSuffix, NewFileSuffix));
    }

    [Test]
    public void RenameSuffix_ThrowsException_WhenOriginalFileSuffixIsEmpty()
    {
        // Arrange
        string originalFileSuffix = string.Empty;

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => Renamer.changeSuffix(TestDirectoryPath, originalFileSuffix, NewFileSuffix));
    }

    [Test]
    public void RenameSuffix_ThrowsException_WhenNewFileSuffixIsNull()
    {
        // Arrange
        string newFileSuffix = null;

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => Renamer.changeSuffix(TestDirectoryPath, OriginalFileSuffix, newFileSuffix));
    }

    [Test]
    public void RenameSuffix_ThrowsException_WhenNewFileSuffixIsEmpty()
    {
        // Arrange
        string newFileSuffix = string.Empty;

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => Renamer.changeSuffix(TestDirectoryPath, OriginalFileSuffix, newFileSuffix));
    }
}