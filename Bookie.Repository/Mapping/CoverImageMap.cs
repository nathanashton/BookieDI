using Bookie.Common.Entities;
using FluentNHibernate.Mapping;

namespace Bookie.Repository.Mapping
{
    public class CoverImageMap : ClassMap<CoverImage>
    {
        public CoverImageMap()
        {
            Id(c => c.Id);
            Map(c => c.FullPathAndFileName);
            Map(c => c.ModifiedDateTime);
            References(x => x.Book).Unique();
        }
    }
}