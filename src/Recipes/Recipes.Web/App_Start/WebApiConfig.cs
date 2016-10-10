﻿using System.Web.Http;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Recipes.CastleInstallers;

namespace Recipes.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new WindsorContainer();

            container.Register(
                Component.For<IMapper>()
                    .UsingFactoryMethod(x => Service.Mappings.AutoMapperConfig.GetMapperConfiguration().CreateMapper()));

            container.Install(new RepositoriesInstaller(), new ServicesInstaller(), new ControllerInstaller());
            config.DependencyResolver = new WindsorHttpDependencyResolver(container);

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
