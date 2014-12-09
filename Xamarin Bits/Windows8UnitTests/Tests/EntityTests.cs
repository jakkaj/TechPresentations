using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows8UnitTests.Base;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MiniModel.Contract.Service;
using MiniModel.Entity;

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
        } 
    }
}
