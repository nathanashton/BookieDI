using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bookie.Common.Interfaces;
using PropertyChanged;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public DateTime? DatePublished { get; set; }
        public int? Pages { get; set; }
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }
        public bool Favourite { get; set; }
        public int Rating { get; set; }

        [NotMapped]
        public DateTime? ModifiedDateTime { get; set; }

        public virtual CoverImage CoverImage { get; set; }
        public virtual ICollection<BookFile> BookFiles { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}