namespace BookStoreApp.Blazor.Server.UI.Service.Base
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
