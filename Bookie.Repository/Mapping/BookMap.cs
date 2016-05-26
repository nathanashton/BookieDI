using Bookie.Common.Entities;
using FluentNHibernate.Mapping;

namespace Bookie.Repository.Mapping
{
    public class BookMap : ClassMap<Book>
    {

        public BookMap()
        {
            Id(c => c.Id);
            Map(c => c.Title);
            Map(c => c.Abstract);
            Map(c => c.DatePublished);
            Map(c => c.Favourite);
            Map(c => c.Isbn10);
            Map(c => c.Isbn13);
            Map(c => c.ModifiedDateTime);
            Map(c => c.Pages);
            Map(c => c.Rating);
            HasOne(x => x.CoverImage);
            HasManyToMany(x => x.Authors).Cascade.All().Table("BookAuthors");
            HasManyToMany(x => x.Publishers).Cascade.All().Table("BookPublishers");
            HasMany(x => x.BookFiles).Cascade.All();
        }
    }
}