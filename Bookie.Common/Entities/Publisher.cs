using System;
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

        [NotMapped]
        public DateTime? ModifiedDateTime { get; set; }
    }
}