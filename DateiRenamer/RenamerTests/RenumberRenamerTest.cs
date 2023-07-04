namespace RenamerTests
{
    [TestFixture]
    public class RenumberRenamerTest
    {
        private string TestDirectoryPath;
        private RenumberRenamer renumberRenamer;

        [SetUp]
        public void Setup()
        {
            TestDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestDirectory");
            Directory.CreateDirectory(TestDirectoryPath);

            string path1 = Path.Combine(TestDirectoryPath, "1-img.txt");
            string path2 = Path.Combine(TestDirectoryPath, "3-img.txt");
            string path3 = Path.Combine(TestDirectoryPath, "4img.txt");
            string path4 = Path.Combine(TestDirectoryPath, "12-img.txt");
            string path5 = Path.Combine(TestDirectoryPath, "5-img.txt");

            File.WriteAllText(path1, "Text");
            File.WriteAllText(path2, "Text");
            File.WriteAllText(path3, "Text");
            File.WriteAllText(path4, "Text");
            File.WriteAllText(path5, "Text");

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
    }
}
