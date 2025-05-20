using BookStoreApp.Blazor.Server.UI.Service.Base;


namespace BookStoreApp.Blazor.Server.UI.Service
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDto>>> Get();
        Task<Response<BookDetailsDto>> Get(int id);
        Task<Response<BookUpdateDto>> GetForUpdateBook(int id);
        Task<Response<int>> CreateBook(BookCreateDto author);

        Task<Response<int>> Edit(int id, BookUpdateDto author);

        Task<Response<int>> Delete(int id);
    }
}
