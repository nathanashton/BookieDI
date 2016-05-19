using Bookie.WPF.ViewModels;

namespace Bookie.WPF.Helpers
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
    }
}
