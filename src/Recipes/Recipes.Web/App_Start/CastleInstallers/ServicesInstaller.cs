using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Recipes.Web.CastleInstallers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // configuration for Recipes.Service.Stores 
            container.Register(Classes.FromAssemblyNamed("Recipes.Service")
                .InNamespace("Recipes.Service.Stores.Implementation")
                .WithService.DefaultInterfaces().LifestylePerWebRequest());

            // configuration for Recipes.Service.Recommendations
            container.Register(Classes.FromAssemblyNamed("Recipes.Service")
                .InNamespace("Recipes.Service.Recommendations.Implementation")
                .WithService.DefaultInterfaces().LifestylePerWebRequest());
        }
    }
}