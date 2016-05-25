using System.Data.Entity;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;

namespace Bookie.Repository
{
    public class SqlCeContext : DbContext
    {
        public SqlCeContext() : base(@"Data Source=" + Settings.DatabasePath)
        {
            // Database.SetInitializer(new CreateDatabaseIfNotExists<SqlCeContext>());

            // Seed Data
        //    Database.SetInitializer(new BookieDbInitialiser());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static ISettings Settings { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookFile> BookFiles { get; set; }
        public DbSet<CoverImage> CoverImages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Book>()
                .HasOptional(e => e.CoverImage).WithRequired(e=> e.Book).WillCascadeOnDelete(true);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Authors).WithMany(e => e.Books).Map(c =>
                {
                    c.MapLeftKey("Bookid");
                    c.MapRightKey("AuthorId");
                    c.ToTable("BookAuthors");
                });

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Publishers).WithMany(e => e.Books);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookFiles).WithOptional(e => e.Book);
        }
    }
}