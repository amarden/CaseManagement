using CaseManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CaseManagement.Configurations
{
    public class AppUserConfiguration : EntityTypeConfiguration<AppUser>
    {
        public AppUserConfiguration()
        {
            ToTable("Users");
        }
    }
}