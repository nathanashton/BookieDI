using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class BooksViewViewModel
    {
        public ObservableCollection<Book> Books { get; set; }

        private readonly IBookCore _bookCore;

        public BooksViewViewModel(IBookCore bookCore, IImporter importer)
        {
            _bookCore = bookCore;
            // _importer.StartScan(@"C:\temp\books\", false);
            Books = _bookCore.GetAllBooksFromRepository();
        }
    }
}