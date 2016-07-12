using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTNetTestBed
{
    public class TokenResult
    {
        public bool IsValid { get; set; }
        public string FailReason { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
