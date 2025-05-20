using AutoMapper;
using AutoMapper.Internal;
using BookStoreAppApI.Data;
using BookStoreAppApI.Models.Author;
using BookStoreAppApI.Models.Book;
using BookStoreAppApI.Models.User;


namespace BookStoreAppApI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AuthorDetailsDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnyDto, Author>().ReverseMap();
          

            

            //CreateMap<Author, AuthorDetailsDto>()
            //    .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

          

            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();

            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Book, BookDetailsDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
        
    }
}
