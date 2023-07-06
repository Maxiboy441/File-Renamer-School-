using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;




namespace files
{
    [TestFixture]
    public class DeleteFirstStar
    {
        private string folderPath;
        private string NewFilePath;
        private FileRenamer fileRenamer;

        [SetUp]
        public void SetUp()
        {
            folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFolder");
            Directory.CreateDirectory(folderPath);
            fileRenamer = new FileRenamer();
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(folderPath, true);
        }

        [Test]
        public void ChangeFirstExclamationMark_ThrowsException_WhenAbsoluteFilePathIsNull()
        {
            // Arrange
            string folderPath = null;


            // Act & Assert
            Assert.Throws<ArgumentException>(() => fileRenamer.ChangefirstStar(folderPath));
        }

        [Test]
        public void ChangeFirstStar_ThrowsException_WhenAbsoluteFilePathIsEmpty()
        {
            // Arrange
            string folderPath = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fileRenamer.ChangefirstStar(folderPath));
        }


        [Test]
        public void ChangefirstStar_RenamesFileWithFirstStar()
        {
            // Arrange
            string file1 = Path.Combine(folderPath);//,"123!abc!txt");
            string file2 = Path.Combine(folderPath);//,"aaaa!4444!bbb!txt");

            File.WriteAllText(file1, "File1 content");
            File.WriteAllText(file2, "File2 content");
            // Act
            fileRenamer.ChangefirstStar(folderPath);

            // Assert
            Assert.IsFalse(File.Exists(file1));
            Assert.IsTrue(File.Exists(file2));
        }
    }

}
