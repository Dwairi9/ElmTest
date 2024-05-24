namespace ElmBookShelf.Domain.ViewModels
{
    public class BookInfoViewModel
    {
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public List<string> CoverBase64 { get; set; } = new List<string>();
    }
}
