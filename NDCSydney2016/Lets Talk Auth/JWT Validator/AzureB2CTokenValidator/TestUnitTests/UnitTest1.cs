using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestJWTFunctionClass;

namespace TestUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var key = File.ReadAllText("testKey.txt");
            var token = File.ReadAllText("testToken.txt");

            var jw = new JWTValidator();
            var result = jw.Validate(token, key);
            Assert.IsTrue(result);

        }
    }
}
