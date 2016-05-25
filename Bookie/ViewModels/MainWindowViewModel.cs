using System.Collections.ObjectModel;
using System.Windows.Input;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
using PropertyChanged;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        private readonly IBookCore _bookCore;
        private readonly IAuthorCore _authorCore;

        private readonly ILog _log;

        public MainWindowViewModel(IBookCore bookCore, IAuthorCore authorCore, ILog log)
        {
            _bookCore = bookCore;
            _authorCore = authorCore;
            _log = log;
        }

        public ObservableCollection<IBook> Books { get; set; }

        public ICommand GetBooksCommand
        {
            get { return new RelayCommand(Get, x => true); }
        }

        public ICommand AddBooksCommand
        {
            get { return new RelayCommand(AddBook, x => true); }
        }

        private void Get(object obj)
        {
            Books = _bookCore.GetAllBooks();
        }

        private void AddBook(object obj)
        {
            var author = new Author
            {
                FirstName = "David",
                LastName = "Jones"
            };
            author = _authorCore.AddAuthor(author);
            var book = new Book {Title = "test tiel"};
            book.Authors.Add(author);
            book = _bookCore.AddBook(book);
        }
    }
}