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
                _ = cfg.CreateMap<Book, BookViewModel>().ReverseMap();
            });


            Mapper = config.CreateMapper();
        }
    }
}
