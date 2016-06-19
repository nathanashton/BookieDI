using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
using Bookie.UserControls.Authors;
using Bookie.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class AuthorsControlViewModel
    {
        private readonly IAuthorCore _authorCore;
        private readonly AuthorDetailsWindow _authorDetailsWindow;

        public AuthorsControlViewModel(IAuthorCore authorCore, AuthorDetailsWindow authorDetailsWindow)
        {
            _authorDetailsWindow = authorDetailsWindow;
            _authorCore = authorCore;
            SelectedAuthors = new ObservableCollection<Author>();
            SetAuthorList(null);
        }

        public ObservableCollection<Author> Authors { get; set; }
        public AuthorList AuthorList { get; set; }
        public ContentControl AuthorView { get; set; }
        public Author SelectedAuthor { get; set; }
        public ObservableCollection<Author> SelectedAuthors { get; set; }

        public ICommand GetAuthorsCommand
        {
            get { return new RelayCommand(GetAuthors, x => true); }
        }

        public ICommand ListCommand
        {
            get { return new RelayCommand(SetAuthorList, x => true); }
        }

        public ICommand DebugCommand
        {
            get { return new RelayCommand(GetAll, x => true); }
        }

        public ICommand AuthorDetailsCommand
        {
            get { return new RelayCommand(AuthorDetails, x => SelectedAuthor != null && SelectedAuthors.Count == 1); }
        }

        private void GetAuthors(object obj)
        {
            Authors = _authorCore.GetAllAuthors();
        }

        private void GetAll(object obj)
        {
            Authors = _authorCore.GetAllAuthors();
        }

        private void SetAuthorList(object obj)
        {
            if (AuthorList == null)
            {
                AuthorList = new AuthorList();
            }
            AuthorView = AuthorList;
        }

        private void AuthorDetails(object obj)
        {
            _authorDetailsWindow.ViewModel.Author = SelectedAuthor;
            //if (_authorDetailsWindow.Visibility == Visibility.Hidden)
            //{
            //    _authorDetailsWindow.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    _authorDetailsWindow.Visibility = Visibility.Visible;
            //}
            _authorDetailsWindow.ShowDialog();
        }
    }
}