using ElmBookShelf.Domain.Entities;

namespace ElmBookShelf.Domain.ViewModels
{
    public class BookViewModel 
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
         
        public DateTime PublishDate { get; set; }
        public DateTime LastModified { get; set; }

        public string CoverBase64 { get; set; }
    }
}
