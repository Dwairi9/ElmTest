using ElmBookShelf.Domain.ViewModels;
using Newtonsoft.Json;

namespace ElmBookShelf.Application.Common
{
    public class Utility
    {
        public static BookInfoViewModel DeserializeBookDetails(string bookInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(bookInfo))
                {
                    var bookDetails = JsonConvert.DeserializeObject<BookInfoViewModel>(bookInfo);
                    return bookDetails;
                }
            }
            catch (Exception ex)
            {
                var x = 0;
            }

            return null;
        }

        public static string SerializeBookDetails(BookInfoViewModel bookDetails)
        {
            try
            { 
                var bookInfo = JsonConvert.SerializeObject(bookDetails);

                return bookInfo; 
            }
            catch (Exception ex)
            {
                var x = 0;
            }

            return "";
        }
    }
}
