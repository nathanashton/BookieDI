using Bookie.Common.Entities;

namespace Bookie.Common.EventArgs
{
    public class AuthorEventArgs : System.EventArgs
    {
        public Author Author { get; set; }
        public int? Progress { get; set; }
    }
}