using AutoMapper;
using BookStoreAppApI.Data;
using BookStoreAppApI.Models.Author;
using BookStoreAppApI.Models.Book;

namespace BookStoreAppApI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnyDto, Author>().ReverseMap();
            CreateMap<BookReadOnlyDto, Book>().ReverseMap();
        }
        
    }
}
