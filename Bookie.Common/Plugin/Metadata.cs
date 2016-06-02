using Bookie.Common.Entities;
using System;
using System.Collections.Generic;

namespace Bookie.Common.Plugin
{
    public class Metadata
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Asin { get; set; }
        public string Isbn { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Subject { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public int? PageCount { get; set; }

        public Metadata()
        {
            Authors = new List<Author>();
            Publishers = new List<Publisher>();
        }
    }
}