using BookStoreApp.Blazor.WebAssembly.UI.Service.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Service
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnyDto>>> Get();
        Task<Response<AuthorDetailsDto>> GetAuthor(int id);
 
        Task<Response<AuthorUpdateDto>> GetForUpdateAuthor(int id);
        Task<Response<int>> CreateAuthor(AuthorCreateDto author);
        
        Task<Response<int>> Edit(int id, AuthorUpdateDto author);

        Task<Response<int>>Delete(int id);
    }
}
