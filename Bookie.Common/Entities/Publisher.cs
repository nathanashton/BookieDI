using Bookie.Common.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class Publisher : IPublisher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new ObservableCollection<Book>();
    }
}