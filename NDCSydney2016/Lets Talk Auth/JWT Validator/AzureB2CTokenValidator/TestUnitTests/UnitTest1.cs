using System;
using System.IO;
using System.Threading.Tasks;
using JWTNetTestBed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateWithKey()
        {
            var key = File.ReadAllText("testKey.txt");
            var token = File.ReadAllText("testToken.txt");

            var jw = new JWTValidator();
            var result = jw.Validate(token, key);
            Assert.IsTrue(result);

        }


        [TestMethod]
        public async Task ValidateWithDiscovery()
        {
           
            var token = File.ReadAllText("testToken.txt");

            var jw = new JWTDownloadKeyAndValidator();
            var result = await jw.Validate(token, "B2C_1_JordoTestSignInAndUp");
            Assert.IsTrue(result);

        }
    }
}
