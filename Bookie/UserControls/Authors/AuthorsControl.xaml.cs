using Bookie.ViewModels;

namespace Bookie.UserControls.Authors
{
    /// <summary>
    ///     Interaction logic for AuthorsControl.xaml
    /// </summary>
    public partial class AuthorsControl
    {
        private readonly AuthorsControlViewModel _controlViewModel;

        public AuthorsControl(AuthorsControlViewModel authorsControlViewModel)
        {
            InitializeComponent();
            _controlViewModel = authorsControlViewModel;
            DataContext = _controlViewModel;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _controlViewModel.GetAll(null);
        }
    }
}