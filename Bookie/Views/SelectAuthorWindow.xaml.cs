using Bookie.Common.Entities;
using Bookie.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bookie.Views
{
    /// <summary>
    ///     Interaction logic for SelectAuthorWindow.xaml
    /// </summary>
    public partial class SelectAuthorWindow
    {
        public SelectAuthorWindow(SelectAuthorsWindowViewModel viewmodel)
        {
            InitializeComponent();
            ViewModel = viewmodel;
            DataContext = viewmodel;
            Closing += SelectAuthorWindow_Closing;
            IsVisibleChanged += SelectAuthorWindow_IsVisibleChanged;
        }

        private void SelectAuthorWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel.GetAuthors();
            ViewModel.Filter = string.Empty;
            FilterBox.Focus();
        }

        public SelectAuthorsWindowViewModel ViewModel { get; set; }
        public bool CloseAllowed { get; set; }

        
        private void SelectAuthorWindow_Closing(object sender, CancelEventArgs e)
        {
            if (CloseAllowed) return;
            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilteredAuthors =
                new ObservableCollection<Author>(
                    ViewModel.Authors.Where(x => x.FullName.ToLower().Contains(ViewModel.Filter.ToLower())));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}