using Bookie.ViewModels;

namespace Bookie.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel windowViewModel)
        {
            InitializeComponent();
            DataContext = windowViewModel;
        }
    }
}