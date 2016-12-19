using Bookie.Common.Entities;
using System.Data.Entity;

namespace Bookie.Core
{
    public class Ctx : DbContext
    {
        public Ctx()
            : base(@"Data Source=test.sdf")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Ctx>());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookFile> BookFiles { get; set; }
        public DbSet<CoverImage> CoverImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(s => s.Authors)
                .WithMany(c => c.Books)
                .Map(c =>
                {
                    c.MapLeftKey("BookId");
                    c.MapRightKey("AuthorId");
                    c.ToTable("BookAuthors");
                });

            modelBuilder.Entity<Book>()
                .HasMany(s => s.Publishers)
                .WithMany(c => c.Books);

            modelBuilder.Entity<Book>()
                .HasOptional(s => s.CoverImage)
                .WithRequired(ad => ad.Book).WillCascadeOnDelete(true);

            modelBuilder.Entity<Book>()
                .HasMany(s => s.BookFiles)
                .WithRequired(ad => ad.Book).WillCascadeOnDelete(true);
        }
    }
}