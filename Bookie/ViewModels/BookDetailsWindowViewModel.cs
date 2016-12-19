using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
using Bookie.Views;
using PropertyChanged;
using System.Windows.Input;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class BookDetailsWindowViewModel
    {
        private readonly IBookCore _bookCore;
        private readonly SelectAuthorWindow _selectAuthorWindow;

        public BookDetailsWindowViewModel(IBookCore bookcore, SelectAuthorWindow selectAuthorWindow)
        {
            _bookCore = bookcore;
            _selectAuthorWindow = selectAuthorWindow;
        }

        public Book Book { get; set; }
        public Author SelectedAuthor { get; set; }

        public bool EditMode { get; set; }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(Save, x => true); }
        }

        public ICommand RemoveAuthorCommand
        {
            get { return new RelayCommand(RemoveAuthor, x => SelectedAuthor != null); }
        }

        public ICommand AddAuthorCommand
        {
            get { return new RelayCommand(AddAuthor, x => true); }
        }

        public ICommand RemoveCoverCommand
        {
            get { return new RelayCommand(RemoveCover, x => true); }
        }

        private void Save(object obj)
        {
            _bookCore.Persist(Book);
        }

        private void RemoveCover(object obj)
        {
            Book.CoverImage = new CoverImage();
        }

        private void RemoveAuthor(object obj)
        {
            Book.Authors.Remove(SelectedAuthor);
        }

        private void AddAuthor(object obj)
        {
            _selectAuthorWindow.ViewModel.Book = Book;
            _selectAuthorWindow.ShowDialog();
        }

        public void UpdateBook()
        {
        }
    }
}