using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Pandora.BackEnd.Business;
using Pandora.BackEnd.Bussines.Providers;
using Pandora.BackEnd.Data.AccountManager;
using Pandora.BackEnd.Data.Concrets;
using System;
using System.Configuration;
using System.Web.Http;

namespace Pandora.BackEnd.Api
{
    public partial class Startup
    {
        private HttpConfiguration httpConfig;

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Set AutoMapper Mapping
            AutoMapperConfig.Register();

            httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            ConfigureSocialAuth(httpConfig);

            WebApiConfig.Register(httpConfig);

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            app.MapSignalR();           
        }


        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);


            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/auth/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost"),
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = "http://localhost";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            var oAuthServerOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                   new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                }
            };

            app.UseJwtBearerAuthentication(oAuthServerOptions);
        }

        private void ConfigureSocialAuth(HttpConfiguration httpConfig)
        {
            // Uncomment the following lines to enable logging in with third party login providers

            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
