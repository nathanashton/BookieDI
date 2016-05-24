using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Bookie.Common.Interfaces;

namespace Bookie.Common.Entities
{
    public class BookFile : IBookFile
    {
        public int Id { get; set; }
        public string FullPathAndFileName { get; set; }
        public long FileSize { get; set; }
        public DateTime? ModifiedDateTime { get; set; }


        [NotMapped]
        public string FileName
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
        public string FileExtension
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
    }
}