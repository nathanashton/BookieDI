using Bookie.Common.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Book : IBook
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Abstract { get; set; }
        public virtual DateTime? DatePublished { get; set; }
        public virtual int? Pages { get; set; }
        public virtual string Isbn10 { get; set; }
        public virtual string Isbn13 { get; set; }
        public virtual bool Favourite { get; set; }
        public virtual int Rating { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }

        public virtual CoverImage CoverImage { get; set; }
        public virtual ICollection<BookFile> BookFiles { get; set; } = new ObservableCollection<BookFile>();
        public virtual ICollection<Author> Authors { get; set; } = new ObservableCollection<Author>();
        public virtual ICollection<Publisher> Publishers { get; set; } = new ObservableCollection<Publisher>();

        public override string ToString()
        {
            return Title;
        }
    }
}