using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using Bookie.Helpers;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class SelectAuthorsWindowViewModel
    {
        private readonly IAuthorCore _authorCore;
        private readonly IBookCore _bookCore;

        public SelectAuthorsWindowViewModel(IAuthorCore authorCore, IBookCore bookcore)
        {
            _authorCore = authorCore;
            _bookCore = bookcore;
            GetAuthors();
        }

        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Author> FilteredAuthors { get; set; }
        public Author SelectedAuthor { get; set; }
        public Book Book { get; set; }
        public string Filter { get; set; }

        public ICommand ChooseAuthorCommand
        {
            get { return new RelayCommand(ChooseAuthor, x => SelectedAuthor != null); }
        }

        public void GetAuthors()
        {
            Authors = new ObservableCollection<Author>(_authorCore.GetAllAuthors().OrderBy(x => x.LastName));
            FilteredAuthors = Authors;
        }

        private void ChooseAuthor(object obj)
        {
            Book.Authors.Add(SelectedAuthor);
            _bookCore.Persist(Book);
        }
    }
}