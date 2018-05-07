using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplication.Services
{
    public class ApiHttpClient
    {
        const string BaseAddress = "http://localhost:50740";
        const string SessionKeyToken = "_Token";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ApiHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            String token = GetToken();

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public void StoreToken(String token)
        {
            _session.SetString(SessionKeyToken, token);
        }
        public String GetToken()
        {
            return _session.GetString(SessionKeyToken);
        }

        public void RemoveToken()
        {
            _session.Remove(SessionKeyToken);
        }

        public String GetBaseAddress()
        {
            return BaseAddress;
        }
    }
}
