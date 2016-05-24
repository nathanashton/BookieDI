using System.Collections.ObjectModel;
using System.Windows.Input;
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
            Get();
        }

        public ObservableCollection<IBook> Books { get; set; }

        public ICommand GetBooksCommand
        {
            get { return new RelayCommand(AddBook, x => true); }
        }


        private void Get()
        {
            Books = _bookCore.GetAllBooks();
        }


        private void AddBook(object obj)
        {
            //    Books = _bookCore.GetAllBooks();
        }
    }
}