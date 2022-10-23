using E_Library;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace E_Library_Test
{
    public class Tests
        {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetIntFromStringReturned_True()
        {
            // arrange
            string inputStr = "2";
            int expected_input = 2;

            // act
            int result = E_Library.Program.GetIntFromString(inputStr);

            // assert
            Assert.That(result, Is.EqualTo(expected_input));
        }
        

        [Test]
        public void DeletefromFileReturned_False()
        {
            // arrange
            int id = 2;
            bool expected_result = false;
           
            // act
            E_Library.Library e = new E_Library.Library();
            bool result = e.DeletefromFile(id);
            
            // assert
            Assert.That(result, Is.EqualTo(expected_result));
        }


        [Test]
        public void CorrectBookInfoReturned_False()
        {
            // arrange
            int id = 2;
            string title = "Деньги-деньги";
            string author = "Киосаки";
            string description = "Финансы и пирамиды";
            string genre = "Комедия";
            string filenamebook = "Kiosaki.pdf";

            bool expected_result = false;

            // act
            E_Library.Library e = new E_Library.Library();
            bool result = e.CorrectBookInfo(id, title, author, description, genre, filenamebook);

            // assert
            Assert.That(result, Is.EqualTo(expected_result));
        }
    }
}