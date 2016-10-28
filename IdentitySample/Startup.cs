using IdentitySample.Config;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Serilog;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web.Routing;

[assembly: OwinStartup(typeof(IdentitySample.Startup))]

namespace IdentitySample
{
    public class Startup
    {
        private string Authority = "https://identity-dev.com/identity";
        private string BaseUri = "https://identity-dev.com";
        //private string ConnectionString = "server=localhost;Database=IdentityServerSampleDb;Trusted_Connection=True";

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888


            app.Map("/identity", (idsrv) =>
            {
                //EntityFrameworkServiceOptions efConfig = new EntityFrameworkServiceOptions
                //{
                //    ConnectionString = ConnectionString,
                //    //SynchronousReads = true
                //};

                IdentityServerServiceFactory Factory = new IdentityServerServiceFactory()
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryUsers(Users.Get())
                        .UseInMemoryScopes(Scopes.Get());

                //Factory.RegisterConfigurationServices(efConfig);
                //Factory.RegisterOperationalServices(efConfig);

                //Factory.ConfigureClientStoreCache();
                //Factory.ConfigureScopeStoreCache();

                idsrv.UseIdentityServer(new IdentityServerOptions
                {
                    RequireSsl = true,
                    SiteName = "IdentityServer3Sample",
                    IssuerUri = Authority,
                    Factory = Factory,
                    PublicOrigin = BaseUri,
                    SigningCertificate = GetCertificate(),

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        EnablePostSignOutAutoRedirect = true,
                        //IdentityProviders = ConfigureIdentityProviders
                    }
                });
            });

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Authority,
                DelayLoadMetadata = true,
                ValidationMode = ValidationMode.Both,
                RequiredScopes = new[] { "api" },
            });
            app.UseWebApi(WebApiConfig.Register());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
                //LogoutPath = new PathString("https://identity-dev.com/")
            });


            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "mvc",
                Authority = Authority,
                RedirectUri = BaseUri,
                ResponseType = "token id_token",
                Scope = "openid api manager employee",
                ClientSecret = "secret",
                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",
                PostLogoutRedirectUri = "https://identity-dev.com/"
            });

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.RollingFile(pathFormat: @"IdSvrAdmin-{Date}.log")
                        .CreateLogger();

            //LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

        }

        private X509Certificate2 GetCertificate()
        {
            return new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\idsrv3test.pfx"), "idsrv3test");
        }
    }
}
