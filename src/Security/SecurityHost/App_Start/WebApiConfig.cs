using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SecurityHost
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var dirName = AppDomain.CurrentDomain.BaseDirectory; // Starting Dir
            FileInfo fileInfo = new FileInfo(dirName);
            DirectoryInfo parentDir = fileInfo.Directory.Parent.Parent;
            string parentDirName = Path.Combine(parentDir.FullName, @"Settings\Tokens.Config");
            var configFile = AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE");

            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", parentDirName);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
