using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnimalsApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalsApi.Services
{
    public static class DogsApiService
    {
        public static async Task<HttpResponseMessage> DoGetRequest(string uri = "", string payload = "")
        {
            var response = new HttpResponseMessage();
            using (var httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Add("x-api-key", Settings.DogsApiKey);
                string url = "https://api.thedogapi.com/v1/" + uri;
                //var response = await httpClient.GetStringAsync(new Uri(url)).Result;
                response = await httpClient.GetAsync(new Uri(url));
            }
            return response;

        }
    }
}