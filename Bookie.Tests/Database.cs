using Bookie.Common.Interfaces;
using Bookie.DependencyResolver;
using Microsoft.Practices.Unity;
using System.IO;

namespace Bookie.Tests
{
    public static class Database
    {
        public static IUnityContainer CleanDatabase()
        {
            var container = Resolver.Bootstrap();
            var settings = container.Resolve<ISettings>();

            if (File.Exists(settings.DatabasePath))
            {
                File.Delete(settings.DatabasePath);
            }
            return container;
        }
    }
}