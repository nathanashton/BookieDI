using Bookie.ViewModels;

namespace Bookie.UserControls.Books
{
    /// <summary>
    ///     Interaction logic for BooksControl.xaml
    /// </summary>
    public partial class BooksControl
    {
        private readonly BooksControlViewModel _controlViewModel;

        public BooksControl(BooksControlViewModel controlViewModel)
        {
            InitializeComponent();
            _controlViewModel = controlViewModel;
            DataContext = _controlViewModel;
        }
    }
}