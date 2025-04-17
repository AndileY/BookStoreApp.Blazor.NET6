using BookStoreApp.Blazor.Server.UI.Service.Base;

namespace BookStoreApp.Blazor.Server.UI.Service
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnyDto>>> Get();
        Task<Response<AuthorDetailsDto>> Get(int id);
        Task<Response<AuthorUpdateDto>> GetForUpdateAuthor(int id);
        Task<Response<int>> CreateAuthor(AuthorCreateDto author);
        
        Task<Response<int>> EditAuthor(int id, AuthorUpdateDto author);

        Task<Response<int>>Delete(int id);
    }
}
