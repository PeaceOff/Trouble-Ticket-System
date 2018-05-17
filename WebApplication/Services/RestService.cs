using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplication.Services
{
    public class RestService
    {
        public static string BaseAddress = "http://localhost:51568";
        private static HttpClient Client;

        public static void SetClient()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (Token != null)
            {
                Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
            }
        }

        public static void StoreLoginResponse(LoginResponse token)
        {
            Token = token.AccessToken.Token;
            Username = token.Username;
            Role = token.Role;
            SetClient();
        }

        public static void StoreRegisterResponse(RegisterResponse token)
        {
            Token = token.AccessToken;
            Role = token.Role;
            Username = token.Username;
            SetClient();
        }

        public static String Token { get; set; }

        public static String Username { get; set; }

        public static String Role { get; set; }

        public static HttpClient GetClient()
        {
            return Client;
        }

        public static void RemoveToken()
        {
            Token = null;
        }
    }
}
