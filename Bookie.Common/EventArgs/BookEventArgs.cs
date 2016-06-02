using Bookie.Common.Entities;

namespace Bookie.Common.EventArgs
{
    public class BookEventArgs : System.EventArgs
    {
        public enum BookState
        {
            Added,
            Removed,
            Updated,
            NoChange
        }

        public Book Book { get; set; }
        public BookState State { get; set; }
        public int? Progress { get; set; }
    }
}