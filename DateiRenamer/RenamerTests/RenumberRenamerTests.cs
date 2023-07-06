[TestFixture]
public class RenumberRenamerTests
{
    string path1 = "";
    string path2 = "";
    string path3 = "";
    string path4 = "";
    string path5 = "";

    string newPath1 = "";
    string newPath2 = "";
    string newPath3 = "";
    string newPath4 = "";
    string newPath5 = "";

    private string TestDirectoryPath;
    private RenumberRenamer renumberRenamer;

    [SetUp]
    public void Setup()
    {
        TestDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestDirectory");
        Directory.CreateDirectory(TestDirectoryPath);

        path1 = Path.Combine(TestDirectoryPath, "6-img.txt");
        path2 = Path.Combine(TestDirectoryPath, "9-img.txt");
        path3 = Path.Combine(TestDirectoryPath, "11-img.txt");
        path4 = Path.Combine(TestDirectoryPath, "12-img.txt");
        path5 = Path.Combine(TestDirectoryPath, "7-img.txt");

        File.WriteAllText(path1, "Text");
        File.WriteAllText(path2, "Text");
        File.WriteAllText(path3, "Text");
        File.WriteAllText(path4, "Text");
        File.WriteAllText(path5, "Text");

        newPath1 = Path.Combine(TestDirectoryPath, "1-img.txt");
        newPath2 = Path.Combine(TestDirectoryPath, "3-img.txt");
        newPath3 = Path.Combine(TestDirectoryPath, "4-img.txt");
        newPath4 = Path.Combine(TestDirectoryPath, "5-img.txt");
        newPath5 = Path.Combine(TestDirectoryPath, "2-img.txt");

        renumberRenamer = new RenumberRenamer();
    }

    [TearDown]
    public void Cleanup()
    {
        Directory.Delete(TestDirectoryPath, true);
    }


    [Test]
    public void RenumberRenamer_ThrowsException_WhenAbsoluteFilePathIsNull()
    {
        // Arrange
        string absoluteFilePath = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => renumberRenamer.execute(absoluteFilePath));
    }

    [Test]
    public void RenumberRenamer_ThrowsException_WhenAbsoluteFilePathIsEmpty()
    {
        // Arrange
        string absoluteFilePath = string.Empty;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => renumberRenamer.execute(absoluteFilePath));
    }

    [Test]
    public void RenumberRenamer_ShouldRenameFile()
    {
        // Act
        renumberRenamer.execute(TestDirectoryPath);

        // Assert
        Assert.IsFalse(File.Exists(path1), "Original file 1 should not exist");
        Assert.IsTrue(File.Exists(newPath1), "Renamed file 1 should exist");

        Assert.IsFalse(File.Exists(path2), "Original file 2 should not exist");
        Assert.IsTrue(File.Exists(newPath2), "Renamed file 2 should exist");

        Assert.IsFalse(File.Exists(path3), "Original file 3 should not exist");
        Assert.IsTrue(File.Exists(newPath3), "Renamed file 3 should exist");

        Assert.IsFalse(File.Exists(path4), "Original file 4 should not exist");
        Assert.IsTrue(File.Exists(newPath4), "Renamed file 4 should exist");

        Assert.IsFalse(File.Exists(path5), "Original file 5 should not exist");
        Assert.IsTrue(File.Exists(newPath5), "Renamed file 5 should exist");
    }

}
