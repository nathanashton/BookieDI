using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Bookie.Common.Interfaces;
using PropertyChanged;

namespace Bookie.Common.Entities
{
    [ImplementPropertyChanged]
    public class CoverImage : IBookFile
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

        public virtual Book Book { get; set; }

        [NotMapped]
        public EntityState EntityState { get; set; }
    }
}