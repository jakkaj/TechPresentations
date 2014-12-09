using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows8UnitTests.Glue;
using Autofac;

namespace Windows8UnitTests.Base
{
    public class TestBase
    {
        protected IContainer Container;
        public TestBase()
        {
            var glue = new ProjectGlue();
            glue.Init();

            Container = glue.Container;
        }

        public T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }
    }
}
