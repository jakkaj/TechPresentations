using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAuthenticationServer.Model.Entity
{
    public class TokenSet
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
