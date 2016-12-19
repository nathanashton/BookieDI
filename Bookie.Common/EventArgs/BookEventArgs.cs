using Bookie.Common.Entities;

namespace Bookie.Common.EventArgs
{
    public class BookEventArgs : System.EventArgs
    {
        public Book Book { get; set; }
        public int? Progress { get; set; }
    }
}