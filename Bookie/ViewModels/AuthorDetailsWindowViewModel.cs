using Bookie.Common.Entities;
using PropertyChanged;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class AuthorDetailsWindowViewModel
    {
        public Author Author { get; set; }
    }
}