using Bookie.Common;
using Bookie.Common.Interfaces;
using Bookie.Core;
using Bookie.Core.AuthorCore;
using Bookie.Core.BookCore;
using Bookie.Core.Interfaces;
using Bookie.Core.SupportedFormatPlugins;
using Bookie.Logging;
using Microsoft.Practices.Unity;

namespace Bookie.DependencyResolver
{
    public class Resolver
    {
        public static UnityContainer Container { get; set; }

        public static UnityContainer Bootstrap()
        {
            Container = new UnityContainer();

            Container.RegisterType<Ctx>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IImporter, Importer>(new ContainerControlledLifetimeManager());

            Container.RegisterType<ISupportedFormats, SupportedFormats>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IAuthorCore, AuthorCore>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IBookCore, BookCore>(new ContainerControlledLifetimeManager());

            Container.RegisterType<ISettings, Settings>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILog, Log>(new ContainerControlledLifetimeManager());
            return Container;
        }
    }
}