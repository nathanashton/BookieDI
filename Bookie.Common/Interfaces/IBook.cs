using Bookie.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        ISet<BookFile> BookFiles { get; set; }
        ISet<Author> Authors { get; set; }
        ISet<Publisher> Publishers { get; set; }
    }
}