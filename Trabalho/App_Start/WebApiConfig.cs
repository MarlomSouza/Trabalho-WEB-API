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
              routeTemplate: "{controller}/{id}/{user_id}",
              defaults: new { id = RouteParameter.Optional, user_id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "FunçãoEspecificaPorUsuarioOuLista",
              routeTemplate: "{controller}/{_id}",
              defaults: new { _id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "FunçãoEspecifica",
              routeTemplate: "{controller}/{_listaId}/{task_id}",
              defaults: new { _listaId = RouteParameter.Optional, task_id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "ListaTodasTarefasLista",
            routeTemplate: "{controller}/{_listaId}/{user_id}",
            defaults: new { _listaId = RouteParameter.Optional, user_id = RouteParameter.Optional }
          );



            // Controllers with Actions
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "{controller}/{action}"
            );

          

        }
    }
}
