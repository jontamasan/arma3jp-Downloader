using System;
using NUnit.Framework;

namespace arma3jpDownloader.Tests {
    [TestFixture]
    class a3jpDownloaderTests {

        [Test]
        public void TestPasswordDecrypt() {
            string user = "unko";
            string pass = "manco";
            string crypt = Password.Encrypt(user, pass); // 暗号化
            string expect = Password.Decrypt(user, crypt); // 復号化

            Assert.That(expect == "manco");
        }

        [Test]
        public void TestGetTimeStamp() {
            var timespan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            UInt32 expect = (uint)timespan.TotalSeconds;
            UInt32 now = PBO.GetTimeStamp();
            Assert.That(expect == now);
        }
    }
}
