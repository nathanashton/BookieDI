using System.Collections.ObjectModel;
using System.Windows;
using Bookie.ViewModels;
using System.Linq;
using Bookie.Common.Entities;

namespace Bookie.Views
{
    /// <summary>
    /// Interaction logic for SelectAuthorWindow.xaml
    /// </summary>
    public partial class SelectAuthorWindow : Window
    {

        public SelectAuthorsWindowViewModel ViewModel { get; set; }
        public bool CloseAllowed { get; set; }


        public SelectAuthorWindow(SelectAuthorsWindowViewModel viewmodel)
        {
            InitializeComponent();
            ViewModel = viewmodel;
            DataContext = viewmodel;
            Closing += SelectAuthorWindow_Closing;
            Loaded += SelectAuthorWindow_Loaded;
        }

        private void SelectAuthorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetAuthors();
        }

        private void SelectAuthorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseAllowed) return;
            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ViewModel.FilteredAuthors = new ObservableCollection<Author>(ViewModel.Authors.Where(x => x.FullName.ToLower().Contains(ViewModel.Filter.ToLower())));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = true;
            //Close();
        }
    }
}
