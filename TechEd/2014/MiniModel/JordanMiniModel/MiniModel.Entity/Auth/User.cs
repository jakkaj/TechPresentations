using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniModel.Entity.Auth
{
    public class User
    {
        public Guid Id { get; set; }
        public string Handle { get; set; }
    }
}
