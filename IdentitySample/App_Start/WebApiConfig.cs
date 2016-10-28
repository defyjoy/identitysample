using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IdentitySample
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            HttpConfiguration config = new HttpConfiguration();
            // Web API configuration and services
            //config.SuppressDefaultHostAuthentication();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            return config;
        }
    }
}
