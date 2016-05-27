using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Common.Plugin;
using Bookie.Core;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
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

        private readonly ILog _log;

        public MainWindowViewModel(IBookCore bookCore, ILog log, IImporter importer)
        {
            _bookCore = bookCore;
            _importer = importer;
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
            Books = _bookCore.GetAllBooksFromRepository();
            LoadPlugins();
        }

        private void AddBook(object obj)
        {
            _importer.StartScan(@"C:\temp\books\", false);
        }

        private void LoadPlugins()
        {
            var plugins = FormatPlugins<IFormatPlugin>.Load("Plugins");
            foreach (var item in plugins)
            {
                item.Plugin.Activate();
            }
        }
    }
}