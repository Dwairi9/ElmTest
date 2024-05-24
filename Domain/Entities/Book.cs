namespace ElmBookShelf.Domain.Entities
{
    public class Book : Entity
    { 
        public int CategoryId { get; set; }
        public string BookInfo { get; set; } 

        public virtual Category Category { get; set; }
    }
}
