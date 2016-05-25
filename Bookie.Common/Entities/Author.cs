using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bookie.Common.Interfaces;
using PropertyChanged;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Author : IAuthor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        [NotMapped]
        public EntityState EntityState { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        [NotMapped]
        public string FullName => LastName + ", " + FirstName;



    }
}