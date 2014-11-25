using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Entity.DTO.Auth
{
    public class TokenGroup
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
