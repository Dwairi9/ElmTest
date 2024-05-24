using System.Collections.Generic;
  
namespace ElmBookShelf.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
