using Data;
using Ninject.Modules;

namespace DependencyResolver
{
    public class DataLayer : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookRepository>().To<TestBookRepository>();
        }
    }
}