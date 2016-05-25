using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bookie.Common.Interfaces;
using PropertyChanged;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Publisher : IPublisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        [NotMapped]
        public EntityState EntityState { get; set; }
    }
}