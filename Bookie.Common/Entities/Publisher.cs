using Bookie.Common.Interfaces;
using System;
using System.Collections.Generic;
using PropertyChanged;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]

    public class Publisher : IPublisher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }

        public virtual ISet<Book> Books { get; set; } = new HashSet<Book>();

        public virtual void AddBook(Book book)
        {
            book.Publishers.Add(this);
            Books.Add(book);
        }
    }
}