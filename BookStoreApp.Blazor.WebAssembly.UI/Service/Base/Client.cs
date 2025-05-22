namespace BookStoreApp.Blazor.WebAssembly.UI.Service.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }


    }
}
