using Bookie.ViewModels;
using System;
using System.Globalization;
using System.Windows.Threading;

namespace Bookie.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel windowViewModel)
        {
            InitializeComponent();
            _viewModel = windowViewModel;
            DataContext = windowViewModel;
            // ReSharper disable once UnusedVariable
            var timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal,
                delegate { _viewModel.Time = DateTime.Now.ToString(CultureInfo.InvariantCulture); }, Dispatcher);
        }
    }
}