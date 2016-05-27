using Bookie.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Bookie.Common.Entities
{
    public class BookFile : IBookFile
    {
        public virtual int Id { get; set; }
        public virtual string FullPathAndFileName { get; set; }
        public virtual long FileSize { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }

        [NotMapped]
        public virtual string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(FullPathAndFileName))
                {
                    return string.Empty;
                }
                return Path.GetFileName(FullPathAndFileName);
            }
        }

        [NotMapped]
        public virtual string FileExtension
        {
            get
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    return string.Empty;
                }
                return Path.GetExtension(FileName);
            }
        }

        public virtual Book Book { get; set; }

        public virtual void AddBook(Book book)
        {
            book.BookFiles.Add(this);
            Book = book;
        }
    }
}