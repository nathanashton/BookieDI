using Bookie.Common.Entities;
using FluentNHibernate.Mapping;

namespace Bookie.Repository.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(c => c.Id);
            Map(c => c.FirstName);
            Map(c => c.LastName);
            Map(c => c.ModifiedDateTime);
            HasManyToMany(x => x.Books).Cascade.All().Inverse().Table("BookAuthors");
        }
    }
}