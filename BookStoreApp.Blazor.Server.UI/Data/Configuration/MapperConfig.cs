using AutoMapper;
using BookStoreApp.Blazor.Server.UI.Service.Base;

namespace BookStoreApp.Blazor.Server.UI.Data.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
         
            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();

        }

    }
}
