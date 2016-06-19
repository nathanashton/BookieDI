using System.Collections.ObjectModel;
using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using PropertyChanged;
using System.Linq;
using System.Windows.Input;
using Bookie.Helpers;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class SelectAuthorsWindowViewModel
    {
        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Author> FilteredAuthors { get; set; }
        public Author SelectedAuthor { get; set; }
        public Book Book { get; set; }
        public string Filter { get; set; }
        private IAuthorCore _authorCore;
        private IBookCore _bookCore;

        public SelectAuthorsWindowViewModel(IAuthorCore authorCore, IBookCore bookcore)
        {
            _authorCore = authorCore;
            _bookCore = bookcore;
            GetAuthors();
        }

        public void GetAuthors()
        {
            Authors = new ObservableCollection<Author>(_authorCore.GetAllAuthors().OrderBy(x => x.LastName));
            FilteredAuthors = Authors;
        }

        public ICommand ChooseAuthorCommand
        {
            get { return new RelayCommand(ChooseAuthor, x => SelectedAuthor != null); }
        }

        private void ChooseAuthor(object obj)
        {
            SelectedAuthor.AddBook(Book);
            Book.AddAuthor(SelectedAuthor);
            _bookCore.Persist(Book);
        }

    }
}
