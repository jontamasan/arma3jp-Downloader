using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Mocks;

namespace arma3jpDownloader.Tests {
    [TestFixture]
    class a3jpDownloaderTests {
        private Context context;

        [SetUp]
        protected void SetUp() {
            context = new Context();
        }

        [Test]
        public void UserNmae() {
            context.username = "test user";
            Assert.AreEqual("test user", context.username);
            
        }

        //[Test]
        //public void StartTempFileTest() {
        //    A3JPXmlCreator xmlCreator= new A3JPXmlCreator(new System.IO.FileStream("C:\\Users\\jon\\Documents\\Visual Studio 2013\\Projects\\arma3jp Downloader\\arma3jpDownloader\\test.txt", System.IO.FileMode.Append, System.IO.FileAccess.Write));
        //    var ex = Assert.Throws<Exception>(() => xmlCreator.StartTempFile(""));

        //    //Assert.That(ex.Message == "");
        //}

        [Test]
        public void TestWriteTempBody() {
            string TEMP_FILENAME = System.IO.Directory.GetCurrentDirectory() + "\\temp.dat";
            System.IO.FileStream fs = new System.IO.FileStream(TEMP_FILENAME, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            A3JPXmlCreator xmlCreator = new A3JPXmlCreator(fs);

            DynamicMock mockWriter = new DynamicMock(typeof(A3JPXmlCreator));


        }

        [Test]
        public void TestPasswordDecrypt() {
            string user = "unko";
            string pass = "manco";
            Password password = new Password(user, pass);
            string crypt = password.Encrypt(); // 暗号化
            password = new Password(user, crypt);
            string expect = password.Decrypt(); // 復号化

            Assert.That(expect == "manco");
        }
    }
}
