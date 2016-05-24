using System;
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

        [NotMapped]
        public string FullName => LastName + ", " + FirstName;

        [NotMapped]
        public DateTime? ModifiedDateTime { get; set; }
    }
}