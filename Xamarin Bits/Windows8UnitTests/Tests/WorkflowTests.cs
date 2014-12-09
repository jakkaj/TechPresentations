using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows8UnitTests.Base;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MiniModel.Contract.Service;
using MiniModel.Entity;
using MiniModel.Model.Workflow;
using XamlingCore.Portable.Data.Extensions;
using XamlingCore.Portable.Workflow.Flow;

namespace Windows8UnitTests.Tests
{
    [TestClass]
    public class WorkflowTests : TestBase
    {
        [TestMethod]
        public async Task WorkflowTest()
        {
            var service = Resolve<IWorkflowService>();
            
            await service.SetupFlows();

            var p = new Person { Id = Guid.NewGuid(), Name = "Bob Peringtonton", Age = 40 };

            await p.Set();

            var activeFlow = await p.StartWorkflow(FlowNames.UploadPerson);
             
            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    var f = activeFlow;

                    if ((await activeFlow.GetInProgressItems()).Count == 0)
                    {
                        break;
                    }
                }
            });

            Assert.IsTrue((await activeFlow.GetInProgressItems()).Count == 0);
           
            Assert.IsTrue((await activeFlow.GetAllItems()).Count >= 1);
        }
    }
}
