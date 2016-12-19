using Bookie.Common.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Author : IAuthor
    {
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

        public virtual int Id { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Biography { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        [NotMapped]
        public virtual string FullName => LastName + ", " + FirstName;

        public override string ToString()
        {
            return FullName;
        }
    }
}