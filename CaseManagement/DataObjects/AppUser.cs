﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseManagement.DataObjects
{
    public class AppUser : IdentityUser
    {
        public bool Approved { get; set; }
        public bool Admin { get; set; }
    }
}