using System;
using System.Collections.Generic;
using Bookie.Common.Interfaces;

namespace Bookie.Common.Entities
{
    public class Publisher : IPublisher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }

        public virtual ISet<Book> Books { get; set; }

        public Publisher()
        {
            Books = new HashSet<Book>();
        }

    }
}