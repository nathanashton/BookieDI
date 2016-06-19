using System;
using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
using Bookie.UserControls.Books;
using Bookie.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Bookie.Common.EventArgs;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class BooksControlViewModel
    {
        private readonly BookDetailsWindow _bookDetailsWindow;
        private readonly IBookCore _bookCore;

        public BooksControlViewModel(IBookCore bookcore, BookDetailsWindow bookDetailsWindow)
        {
            Books = new ObservableCollection<Book>();
            _bookDetailsWindow = bookDetailsWindow;
            _bookCore = bookcore;
            Books = new ObservableCollection<Book>();
            _bookCore.BookChanged += _bookCore_BookChanged;
            SelectedBooks = new ObservableCollection<Book>();
            SetBookTiles(null);
        }

        private void _bookCore_BookChanged(object sender, Common.EventArgs.BookEventArgs e)
        {
            if (e.State != BookEventArgs.BookState.Updated) return;
            var existing = Books.FirstOrDefault(x => x.Id == e.Book.Id);
            if (existing == null)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Books.Add(e.Book);
                    Books = new ObservableCollection<Book>(Books.OrderBy(x => x.Title).ToList());
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Books.Remove(existing);
                    Books.Add(e.Book);
                    Books = new ObservableCollection<Book>(Books.OrderBy(x => x.Title).ToList());
                });
            }
        }

        public BookTiles BookTiles { get; set; }
        public BookList BookList { get; set; }
        public ContentControl BookView { get; set; }
        public ObservableCollection<Book> Books { get; set; }
        public Book SelectedBook { get; set; }
        public ObservableCollection<Book> SelectedBooks { get; set; }

        public ICommand TilesCommand
        {
            get { return new RelayCommand(SetBookTiles, x => true); }
        }

        public ICommand ListCommand
        {
            get { return new RelayCommand(SetBookList, x => true); }
        }

        public ICommand BookDetailsCommand
        {
            get { return new RelayCommand(BookDetails, x => SelectedBook != null && SelectedBooks.Count == 1); }
        }

        public ICommand BatchActionsCommand
        {
            get { return new RelayCommand(BatchActions, x => SelectedBooks.Count > 1); }
        }

        public ICommand GetAllBooksCommand
        {
            get { return new RelayCommand(GetAllBooks, x => true); }
        }

        private void SetBookTiles(object obj)
        {
            if (BookTiles == null)
            {
                BookTiles = new BookTiles();
            }
            BookView = BookTiles;
        }

        private void SetBookList(object obj)
        {
            if (BookList == null)
            {
                BookList = new BookList();
            }
            BookView = BookList;
        }

        private void BookDetails(object obj)
        {
            _bookDetailsWindow.ViewModel.Book = SelectedBook;
            _bookDetailsWindow.ShowDialog();
        }

        private void BatchActions(object obj)
        {
        }

        private void GetAllBooks(object obj)
        {
            Books = _bookCore.GetAllBooks();
        }
    }
}