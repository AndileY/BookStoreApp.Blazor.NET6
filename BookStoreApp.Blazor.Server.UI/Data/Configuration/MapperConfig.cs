using AutoMapper;
using BookStoreApp.Blazor.Server.UI.Service.Base;
using BookStoreAppApI.Data;

namespace BookStoreApp.Blazor.Server.UI.Data.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
         
            CreateMap<AuthorReadOnyDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();




            CreateMap<BookDetailsDto, BookUpdateDto>().ReverseMap();
            //.ForMember(dest => dest.ImageData, opt => opt.Ignore())
            //.ForMember(dest => dest.OriginalImageName, opt => opt.Ignore());




















        }

    }
}
