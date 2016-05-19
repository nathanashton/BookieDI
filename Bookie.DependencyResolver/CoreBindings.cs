using Bookie.Core;
using Bookie.Core.Interfaces;
using Ninject.Modules;

namespace Bookie.DependencyResolver
{
    public class CoreBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookCore>().To<BookCore>();
        }
    }
}