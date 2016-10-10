using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Recipes.Web.CastleInstallers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("Recipes.DAL")
                .Where(type => type.Name.EndsWith("Repository"))
                .WithService.DefaultInterfaces().LifestyleTransient());
        }
    }
}