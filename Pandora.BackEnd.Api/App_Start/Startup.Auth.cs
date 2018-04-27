using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Pandora.BackEnd.Bussines.Providers;
using System;
using System.Configuration;
using System.Web.Http;

namespace Pandora.BackEnd.Api
{
    public partial class Startup
    {
        private HttpConfiguration _httpConfig;
        private string _issuer;
        private string _audienceId;
        private byte[] _audienceSecret;

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // set token values options
            _issuer = "http://localhost:12345";
            _audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            _audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            _httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            ConfigureSocialAuth(_httpConfig);

            WebApiConfig.Register(_httpConfig);

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(_httpConfig);
        }


        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/auth/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(12),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(_issuer, _audienceId, _audienceSecret),
                RefreshTokenProvider = new TokenRefreshProvider(),
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            // Api controllers with an [Authorize] attribute will be validated with JWT
            var oAuthServerOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { _audienceId },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                   new SymmetricKeyIssuerSecurityTokenProvider(_issuer, _audienceSecret)
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
