
[TestFixture]
public class FileRenamerTests
{
    private const string TestFilesDirectory = "/Volumes/data/Berufsschule/E1FI5-DateiRenamer/DateiRenamer/files/testFiles/";
    private const string OriginalFileName = "testfile.txt";
    private const string NewFileName = "test2.txt";

    [SetUp]
    public void Setup()
    {
        // Create the original file for testing
        string originalFilePath = Path.Combine(TestFilesDirectory, OriginalFileName);
        File.WriteAllText(originalFilePath, "This is a test file.");
    }

    [TearDown]
    public void Cleanup()
    {
        // Clean up the test file
        string originalFilePath = Path.Combine(TestFilesDirectory, OriginalFileName);
        string renamedFilePath = Path.Combine(TestFilesDirectory, NewFileName);

        if (File.Exists(originalFilePath))
        {
            File.Delete(originalFilePath);
        }

        if (File.Exists(renamedFilePath))
        {
            File.Move(renamedFilePath, originalFilePath);
        }
    }

    [Test]
    public void RenameFile_ShouldRenameFile()
    {
        // Arrange
        FileRenamer renamer = new FileRenamer();
        string originalFilePath = Path.Combine(TestFilesDirectory, OriginalFileName);
        string renamedFilePath = Path.Combine(TestFilesDirectory, NewFileName);

        // Act
        renamer.RenameFile(originalFilePath, "test2");

        // Assert
        Assert.IsFalse(File.Exists(originalFilePath), "Original file should not exist");
        Assert.IsTrue(File.Exists(renamedFilePath), "Renamed file should exist");
    }
}