using Bookie.WPF.ViewModels;
using Ninject.Modules;

namespace Bookie.WPF
{
    public class ViewModelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf().InTransientScope();
        }
    }
}
