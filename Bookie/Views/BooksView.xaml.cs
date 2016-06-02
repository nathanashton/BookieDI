using Bookie.ViewModels;

namespace Bookie.Views
{
    public partial class BooksView
    {
        public BooksView(BooksViewViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}