﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Bookie.Common.Interfaces;

namespace Bookie.Common.Entities
{
    public class CoverImage : IBookFile
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

    }
}