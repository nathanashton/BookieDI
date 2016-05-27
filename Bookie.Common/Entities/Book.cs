using Bookie.Common.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;

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
        public virtual ISet<BookFile> BookFiles { get; set; } = new HashSet<BookFile>();
        public virtual ISet<Author> Authors { get; set; } = new HashSet<Author>();
        public virtual ISet<Publisher> Publishers { get; set; } = new HashSet<Publisher>();

        public virtual void AddAuthor(Author author)
        {
            author.Books.Add(this);
            Authors.Add(author);
        }

        public virtual void AddPublisher(Publisher publisher)
        {
            publisher.Books.Add(this);
            Publishers.Add(publisher);
        }

        public virtual void AddBookFile(BookFile bookFile)
        {
            bookFile.Book = this;
            BookFiles.Add(bookFile);
        }

        public virtual void AddCoverImage(CoverImage cover)
        {
            cover.Book = this;
            CoverImage = cover;
        }
    }
}