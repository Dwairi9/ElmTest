using AutoMapper;
using ElmBookShelf.Domain.Entities;
using ElmBookShelf.Domain.ViewModels;

namespace ElmBookShelf.Application.Common
{
    public class MapperProfile
    {
        public static IMapper Mapper { get; set; }
        public void mapping()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {  
                _ = cfg.CreateMap<Book, BookViewModel>()
                    .ForMember(vm=> vm.BookDetails, opt=> opt.MapFrom(entity=> Utility.DeserializeBookDetails(entity.BookInfo))); 

                _ = cfg.CreateMap<BookViewModel, Book>()
                    .ForMember(entity => entity.BookInfo, opt => opt.MapFrom(vm => Utility.SerializeBookDetails(vm.BookDetails)));

                _ = cfg.CreateMap<Category, CategoryViewModel>().ReverseMap(); 
            });


            Mapper = config.CreateMapper();
        }
    }
}
