using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public interface ISomeModel
    {
        string GetSomeString();
    }

    public class SomeModel : ISomeModel
    {
        public string GetSomeString()
        {
            return "This is some string";
        }
    }
}
