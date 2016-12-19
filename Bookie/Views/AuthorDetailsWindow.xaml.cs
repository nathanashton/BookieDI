using Bookie.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Bookie.Views
{
    /// <summary>
    ///     Interaction logic for BookDetailsWindow.xaml
    /// </summary>
    public partial class AuthorDetailsWindow
    {
        public AuthorDetailsWindow(AuthorDetailsWindowViewModel viewmodel)
        {
            InitializeComponent();
            ViewModel = viewmodel;
            DataContext = ViewModel;
        }

        public bool CloseAllowed { get; set; }
        public AuthorDetailsWindowViewModel ViewModel { get; set; }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!CloseAllowed)
            {
                Visibility = Visibility.Hidden;
                e.Cancel = true;
            }
        }
    }
}