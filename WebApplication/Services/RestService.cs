using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplication.Services
{
    public class RestService
    {
        const string BaseAddress = "http://localhost:51568";
        const string SessionKeyToken = "_Token";
        const string SessionKeyRole = "_Role";
        const string SessionKeyUsername = "_Username";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public RestService(IHttpContextAccessor httpContextAccessor)
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

        public void StoreLoginResponse(LoginResponse token)
        {
            _session.SetString(SessionKeyToken, token.AccessToken.Token);
            _session.SetString(SessionKeyRole, token.Role);
            _session.SetString(SessionKeyUsername, token.Username);
        }

        public void StoreRegisterResponse(RegisterResponse token)
        {
            _session.SetString(SessionKeyToken, token.AccessToken);
            _session.SetString(SessionKeyRole, token.Role);
            _session.SetString(SessionKeyUsername, token.Username);
        }

        public void RemoveToken()
        {
            _session.Remove(SessionKeyToken);
        }

        public String GetToken()
        {
            return _session.GetString(SessionKeyToken);
        }

        public String GetUsername()
        {
            return _session.GetString(SessionKeyUsername);
        }

        public String GetRole()
        {
            return _session.GetString(SessionKeyRole);
        }

        public String GetBaseAddress()
        {
            return BaseAddress;
        }
    }
}
