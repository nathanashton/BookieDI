using Bookie.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Bookie.Views
{
    /// <summary>
    ///     Interaction logic for BookDetailsWindow.xaml
    /// </summary>
    public partial class BookDetailsWindow
    {
        public BookDetailsWindow(BookDetailsWindowViewModel viewmodel)
        {
            InitializeComponent();
            ViewModel = viewmodel;
            DataContext = ViewModel;
            IsVisibleChanged += BookDetailsWindow_IsVisibleChanged;
        }

        public bool CloseAllowed { get; set; }
        public BookDetailsWindowViewModel ViewModel { get; set; }

        private void BookDetailsWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BookTitle.Text = ViewModel.Book.Title;
            BookAbstract.Text = ViewModel.Book.Abstract;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (CloseAllowed) return;
            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bookTitle = BookTitle.GetBindingExpression(TextBox.TextProperty);
            var bookAbstract = BookAbstract.GetBindingExpression(TextBox.TextProperty);

            bookTitle?.UpdateSource();
            bookAbstract?.UpdateSource();
            Close();
        }

    }
}