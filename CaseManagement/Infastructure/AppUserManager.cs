using CaseManagement.Models.SQL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CaseManagement.DataObjects
{
    public class AppStoreUserManager : UserManager<AppUser>
    {
        public AppStoreUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        public static AppStoreUserManager Create(IdentityFactoryOptions<AppStoreUserManager> options, IOwinContext context)
        {
            var manager = new AppStoreUserManager(new UserStore<AppUser>(context.Get<SQLDatabase>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Password Validations
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUser user, string authenticationType)
        {
            var userIdentity = await CreateIdentityAsync(user, authenticationType);

            return userIdentity;
        }
    }
}