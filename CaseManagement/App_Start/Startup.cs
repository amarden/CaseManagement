using CaseManagement.Configurations;
using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace CaseManagement
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuthentication(IAppBuilder app)
        {
            // User a single instance of StoreContext and AppUserManager per request
            app.CreatePerOwinContext(SQLDatabase.Create);
            app.CreatePerOwinContext<AppStoreUserManager>(AppStoreUserManager.Create);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(10),
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}