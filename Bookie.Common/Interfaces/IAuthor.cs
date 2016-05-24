namespace Bookie.Common.Interfaces
{
    public interface IAuthor : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Biography { get; set; }
        string FullName { get; }
    }
}