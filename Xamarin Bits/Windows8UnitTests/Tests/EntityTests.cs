using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows8UnitTests.Base;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MiniModel.Contract.Service;
using MiniModel.Entity;
using XamlingCore.Portable.Data.Extensions;

namespace Windows8UnitTests.Tests
{
    [TestClass]
    public class EntityTests : TestBase
    {
        [TestMethod]
        public async Task EntitySaveAndLoad()
        {
            var p = new Person {Id = Guid.NewGuid()};

            var service = Resolve<IPersonService>();

            var pSaved = await service.Save(p);

            //load the person again
            var pLoaded = await service.Load(p.Id);
            
            Assert.IsTrue(Object.ReferenceEquals(pSaved, pLoaded));

            var p2 = new Person
            {
                Id = p.Id,
                Age = 21,
                Name = "Billingsworth Canterburington"
            };

            p2 = await p2.Set();

            Assert.AreEqual(p.Name, p2.Name);
            Assert.IsTrue(Object.ReferenceEquals(p, p2));
        } 
    }
}
