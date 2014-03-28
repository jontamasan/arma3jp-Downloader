using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
    }
}
