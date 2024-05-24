using ElmBookShelf.Domain.Entities;
using System.Collections.Generic;

namespace ElmBookShelf.Domain.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
    }
}
