namespace Bookie.Common
{
    public class Book
    {
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}