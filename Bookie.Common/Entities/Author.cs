using Bookie.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PropertyChanged;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Author : IAuthor
    {
        public virtual int Id { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Biography { get; set; }

        public virtual ISet<Book> Books { get; set; } = new HashSet<Book>();

        [NotMapped]
        public virtual string FullName => LastName + ", " + FirstName;

        [NotMapped]
        public string BooksToString
        {
            get
            {
                var b = Books.ToList();
                var p = string.Join<Book>(",", b.ToArray());
                return p;
            }
        }

        public virtual void AddBook(Book book)
        {
            book.Authors.Add(this);
            Books.Add(book);
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}