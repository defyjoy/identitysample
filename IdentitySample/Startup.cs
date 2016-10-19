using IdentitySample.Config;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
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

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888


            app.Map("/identity", (idsrv) =>
            {
                IdentityServerServiceFactory Factory = new IdentityServerServiceFactory()
                                                            .UseInMemoryClients(Clients.Get())
                                                            .UseInMemoryUsers(Users.Get())
                                                            .UseInMemoryScopes(Scopes.Get());
                idsrv.UseIdentityServer(new IdentityServerOptions
                {
                    RequireSsl = true,
                    SiteName = "IdentityServer3Sample",
                    IssuerUri = Authority,
                    Factory = Factory,
                    //PublicOrigin = "https://identity-dev.com/identity",
                    SigningCertificate = GetCertificate()
                });
            });

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Authority,
                DelayLoadMetadata = true,
                ValidationMode = ValidationMode.Both,
                RequiredScopes = new[] { "api" }
            });

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            app.UseWebApi(WebApiConfig.Register());


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
