using System.Windows;
using Bookie.DependencyResolver;

namespace Bookie.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Initialize(new CoreBindings(), new RepositoryBindings(), new ViewModelBindings());



            base.OnStartup(e);
        }
    }
}
