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
        public void ValidateWithRsaKey()
        {
            var key = File.ReadAllText("testKeyRsa.txt");
            var token = File.ReadAllText("testToken.txt");

            var tokenFail = token.Replace("My1jMm", "-0---");

            var failResult = JwtValidator.ValidateWithRsaKey(tokenFail, key, "CentralAuthHost", "SomeDemoServer");
            Assert.IsFalse(failResult.IsValid);

            var failResult2 = JwtValidator.ValidateWithRsaKey(token, key, "CentralAuthHost2", "SomeDemoServer");
            Assert.IsFalse(failResult2.IsValid);

            var failResult3 = JwtValidator.ValidateWithRsaKey(token, key, "CentralAuthHost", "SomeDemoServer3");
            Assert.IsFalse(failResult3.IsValid);


            var result = JwtValidator.ValidateWithRsaKey(token, key, "CentralAuthHost", "SomeDemoServer");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ValidateWithJwkKey()
        {
            var key = File.ReadAllText("testKey.txt");
            var token = File.ReadAllText("testToken.txt");
            
            var result = JwtValidator.ValidateWithJwk(token, key, "https://login.microsoftonline.com/0a7110e8-b2aa-48cf-844f-c43e3533288d/v2.0/", "B2C_1_JordoTestSignInAndUp");
            Assert.IsTrue(result.IsValid);
        }


        [TestMethod]
        public async Task ValidateWithDiscovery()
        {
            var token = File.ReadAllText("testToken.txt");
            var result = await JwtDownloadKeyAndValidator.Validate(token, "B2C_1_JordoTestSignInAndUp");
            Assert.IsTrue(result.IsValid);

        }
    }
}
