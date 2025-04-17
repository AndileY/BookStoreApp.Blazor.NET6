using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Service.Base;
using System;
using System.Reflection.Metadata.Ecma335;

namespace BookStoreApp.Blazor.Server.UI.Service
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient client;
        private readonly IMapper mapper;
        public AuthorService(IClient client, ILocalStorageService localStorageService,IMapper mapper) : base(client, localStorageService)
        {
            this.client = client;
            this.mapper = mapper;
            
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
        {
            Response<int> response = new ();
            try
            {
                await GetBearerToken();
                await client.AuthorsPOSTAsync(author);

            }
            catch(ApiException exception)
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
                await client.AuthorsDELETEAsync(id);

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);

            }
            return response;

        }

        public async Task<Response<int>> EditAuthor(int id,AuthorUpdateDto author)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.AuthorsPUTAsync(id, author);
            }
            catch(ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);

            }
            return response;
        }

     

        public async Task<Response<AuthorDetailsDto>> Get(int id)
        {
            Response<AuthorDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorDetailsDto>
                {
                    Data = data,
                    Success = true
                };

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<AuthorDetailsDto>(exception);
            }

            return response;
        }

        public async Task<Response<List<AuthorReadOnyDto>>> Get()
        {
            Response<List<AuthorReadOnyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnyDto>>
                {
                    Data = data.ToList(), 
                    Success = true
                };

            }
            catch(ApiException exception)
            {
                response = ConvertApiExceptions<List<AuthorReadOnyDto>>(exception);
            }

            return response;
        }

        public async Task<Response<AuthorUpdateDto>> GetForUpdateAuthor(int id)
        {
            Response<AuthorUpdateDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorUpdateDto>
                {
                    Data = mapper.Map < AuthorUpdateDto > (data),
                    Success = true
                };

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<AuthorUpdateDto>(exception);
            }

            return response;
        }
    }
}
