using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
using Bookie.Views;
using Microsoft.Practices.Unity;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        private readonly IBookCore _bookCore;
        private readonly IImporter _importer;

        public MainWindowViewModel(IBookCore bookCore, IImporter importer)
        {
            _bookCore = bookCore;
            _importer = importer;
            Books = _bookCore.GetAllBooksFromRepository();
        }

        public ObservableCollection<Book> Books { get; set; }

        public ICommand GetBooksCommand
        {
            get { return new RelayCommand(Get, x => true); }
        }

        public ICommand TileViewCommand
        {
            get { return new RelayCommand(TileView, x => true); }
        }

        public ICommand AddBooksCommand
        {
            get { return new RelayCommand(AddBook, x => true); }
        }

        private void Get(object obj)
        {
            Books = _bookCore.GetAllBooksFromRepository();
        }

        private void AddBook(object obj)
        {
            _importer.StartScan(@"C:\temp\books", false);
        }

        private void TileView(object obj)
        {
            var container = DependencyResolver.Resolver.Bootstrap();
            var view = container.Resolve<BooksView>();
            view.Show();
        }
    }
}