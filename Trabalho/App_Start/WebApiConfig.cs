using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Trabalho
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();


            // Padrao
            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );

            // Controllers with Actions
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "{controller}/{action}"
            );

          

        }
    }
}
