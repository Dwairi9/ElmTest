using ElmBookShelf.Domain.Entities;

namespace ElmBookShelf.Domain.ViewModels
{
    public class BookViewModel : BaseViewModel
    { 
        public long CategoryId { get; set; }
        public string BookInfo { get; set; }
        public BookInfoViewModel BookDetails { get; set; }

        public virtual CategoryViewModel Category { get; set; }
    }
}
