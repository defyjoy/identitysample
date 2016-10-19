using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using IdentitySample.Config;
using System.Web.Http;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using IdentityServer3.AccessTokenValidation;
using System.Web.Routing;
using System.Net;
using Serilog;
using IdentityServer3.Core.Logging;
using Serilog.Events;

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

                IdentityServerOptions options = new IdentityServerOptions
                {
                    RequireSsl = true,
                    SiteName = "IdentityServer3Sample",
                    IssuerUri = Authority,
                    Factory = Factory,
                    //PublicOrigin = "http://identity-dev.com/identity",
                    SigningCertificate = new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\idsrv3test.pfx"), "idsrv3test")
                };
                idsrv.UseIdentityServer(options);
            });

            IdentityServerBearerTokenAuthenticationOptions authOptions = new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Authority,
                DelayLoadMetadata = true,
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "api" },
            };

            app.UseIdentityServerBearerTokenAuthentication(authOptions);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                                .WriteTo.RollingFile(pathFormat: @"IdSvrAdmin-{Date}.log")
                        .CreateLogger();

            //LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

        }
    }
}
