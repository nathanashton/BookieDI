using Core;
using Ninject.Modules;

namespace DependencyResolver
{
    public class CoreLayer : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookCore>().To<BookCore>();
        }
    }
}