﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoreAuthenticationServer.Model
{
    public class Xattribute
    {
        public Xattribute()
        {
            var a = new AuthorizeAttribute();
        }
    }
}
