using Bookie.Repository;
using Bookie.Repository.Interfaces;
using Ninject.Modules;

namespace Bookie.DependencyResolver
{
    public class RepositoryBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookRepository>().To<TestBookRepository>().InSingletonScope();
        }
    }
}