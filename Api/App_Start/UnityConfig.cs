using Microsoft.Practices.Unity;
using System.Web.Http;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Repository;
using ParcelForce.Test.WebApi.Controllers;
using Unity.WebApi;

namespace ParcelForce.Test.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            container.RegisterType<ILocation, Location>();
            container.RegisterType<ILocation, Location>();
            container.RegisterType<ILawnMowerMachine, LawnMowerMachine>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILawn, Lawn>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISlmmRepository, SlmmInMemoryRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILawnCommandsService, LawnCommandsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILawnQueyService, LawnQueyService>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}