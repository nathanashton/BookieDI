using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bookie.Common.Interfaces;

namespace Bookie.Common.Entities
{
    public class Author : IAuthor
    {
        public virtual int Id { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Biography { get; set; }

    
        public virtual ISet<Book> Books { get; set; }

        [NotMapped]
        public virtual string FullName => LastName + ", " + FirstName;

        public Author()
        {
            Books = new HashSet<Book>();
        }



    }
}