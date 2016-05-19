using System.Collections.ObjectModel;
using Bookie.Common;
using Bookie.Core.Interfaces;
using PropertyChanged;

namespace Bookie.WPF.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public ObservableCollection<Book> Books { get; set; }
        private readonly IBookCore _bookCore;

        public MainViewModel(IBookCore bookCore)
        {
            _bookCore = bookCore;
            var b = _bookCore.GetAllBooks();
            Books = new ObservableCollection<Book>(b);
        }
    }
}
