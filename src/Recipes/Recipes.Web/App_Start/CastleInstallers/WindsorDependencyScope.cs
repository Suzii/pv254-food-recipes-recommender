using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace Recipes.Web.CastleInstallers
{
    internal class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
            _scope = container.BeginScope();
        }

        public object GetService(Type t)
        {
            return _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}