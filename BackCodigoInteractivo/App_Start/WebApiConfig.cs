using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BackCodigoInteractivo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //COrs 
            config.EnableCors();

            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "UserCourseApi",
                routeTemplate: "api/UserCourse/{username}",
                defaults: new { controller = "UserCourse", action = "Get", username = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute("UserCourseAPI","api/UserCourse/{username}",new { controller = "UserCourse",action= "Get", username = RouteParameter.Optional});

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Formatters.Remove(config.Formatters.XmlFormatter);
           config.Formatters.Add(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.S‌​erializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        }
    }
}
