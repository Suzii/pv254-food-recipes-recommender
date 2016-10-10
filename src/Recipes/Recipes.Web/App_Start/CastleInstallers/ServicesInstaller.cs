using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Recipes.Web.CastleInstallers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("Recipes.Service")
                .Where(type => type.Namespace.Contains("Implementation"))
                .WithService.DefaultInterfaces().LifestylePerWebRequest());
        }
    }
}