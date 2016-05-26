using Bookie.Common.Entities;
using FluentNHibernate.Mapping;

namespace Bookie.Repository.Mapping
{
    public class PublisherMap : ClassMap<Publisher>
    {

        public PublisherMap()
        {
            Id(c => c.Id);
            Map(c => c.Name);
            Map(c => c.ModifiedDateTime);
            HasManyToMany(x => x.Books).Cascade.All().Inverse().Table("BookPublishers");
        }
    }
}