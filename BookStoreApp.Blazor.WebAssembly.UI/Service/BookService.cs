using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Service.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Service
{
    public class BookService : BaseHttpService, IBookService
    {
        private readonly IClient client;
        private readonly IMapper mapper;
        public BookService(IClient client, ILocalStorageService localStorageService, IMapper mapper) : base(client, localStorageService)
        {
            this.client = client;
            this.mapper = mapper;

        }

        public async Task<Response<int>> CreateBook(BookCreateDto book)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.BooksPOSTAsync(book);

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);

            }
            return response;

        }

        public async Task<Response<int>> Delete(int id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.BooksDELETEAsync(id);

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);

            }
            return response;

        }

        public async Task<Response<int>> Edit(int id, BookUpdateDto book)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.BooksPUTAsync(id, book);
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);

            }
            return response;
        }



        public async Task<Response<BookDetailsDto>> Get(int id)
        {
            Response<BookDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.BooksGETAsync(id);
                response = new Response<BookDetailsDto>
                {
                    Data = data,
                    Success = true
                };

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<BookDetailsDto>(exception);
            }

            return response;
        }

        public async Task<Response<List<BookReadOnlyDto>>> Get()
        {
            Response<List<BookReadOnlyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await client.BooksAllAsync();
                response = new Response<List<BookReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };



            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<List<BookReadOnlyDto>>(exception);
            }

            return response;
        }

        public async Task<Response<BookUpdateDto>> GetForUpdateBook(int id)
        {
            var response = new Response<BookUpdateDto>();

            try
            {
                var book = await client.BooksGETAsync(id);

                var mapped = mapper.Map<BookUpdateDto>(book); // suspect line
                response.Data = mapped;
                response.Success = true;
            }
            catch (AutoMapperMappingException ex)
            {
                Console.WriteLine("AutoMapper failed to map BookDetailsDto to BookUpdateDto");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);

                response.Success = false;
                response.Message = "Mapping error occurred. See logs.";
            }

            return response;
        }
    }
}

