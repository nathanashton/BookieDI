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

        private readonly ILog _log;

        public MainWindowViewModel(IBookCore bookCore, ILog log)
        {
            _bookCore = bookCore;
            _log = log;
            Books = _bookCore.GetAllBooksFromRepository();
        }

        public ObservableCollection<Book> Books { get; set; }

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
            var existing = _bookCore.GetBookByTitle("TEst");
            existing[0].Title = "New title";
            _bookCore.Persist(existing[0]);
        }

        private void AddBook(object obj)
        {

            var book = new Book();
            book.Title = "TEst";
            _bookCore.Persist(book);

          

        }
    }
}