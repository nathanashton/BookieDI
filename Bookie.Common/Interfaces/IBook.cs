using System;
using System.Collections.Generic;
using Bookie.Common.Entities;

namespace Bookie.Common.Interfaces
{
    public interface IBook : IEntity
    {
        string Title { get; set; }
        string Abstract { get; set; }
        DateTime? DatePublished { get; set; }
        int? Pages { get; set; }
        string Isbn10 { get; set; }
        string Isbn13 { get; set; }
        bool Favourite { get; set; }
        int Rating { get; set; }
        CoverImage CoverImage { get; set; }
        ICollection<BookFile> BookFiles { get; set; }
        ICollection<Author> Authors { get; set; }
        ICollection<Publisher> Publishers { get; set; }
    }
}