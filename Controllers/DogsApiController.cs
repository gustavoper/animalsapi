using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalsApi.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using AnimalsApi.Services;
using AnimalsApi.Repositories;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
/**

NOTA MUITO IMPORTANTE

BOA PARTE DAS AÇÔES ESTAO NA CONTROLLER QUE É PARA VOCES ENTENDEREM O QUE ESTÀ ACONTECENDO

PARA UMA FUTURA IMPLEMENTAÇÃO ROBUSTA, USE E ABUSE DE SERVIÇOS, REPOSITORIOS, ETC

**/


namespace AnimalsApi.Controllers
{
    [Route("v1/animals")]
    public class DogsApiController : Controller
    {

        HttpClient _httpClient = new HttpClient();

        public DogsApiController()
        {
            _httpClient.DefaultRequestHeaders.Add("x-api-key", Settings.DogsApiKey);
        }



        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.username, model.password);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            var token = TokenService.GenerateJwtToken(user);
            user.password = "";
            return new 
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("checkauth")]
        [Authorize]
        public async Task<ActionResult<dynamic>> CheckAuthenticated() 
        {
            return new {
                message = String.Format("Olá,{0}, sua sessão ainda está valida", User.Identity.Name) 
            };
        }



        [HttpGet]
        [Route("dog/random")]
        [Authorize]
        public async Task<ActionResult<dynamic>>  GetRandomDog()
        {
            try {
                string url = Settings.DogsApiUrl+ "/images/search";  
                using var httpResponse = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                
                if (httpResponse.Content !is object && httpResponse.Content.Headers.ContentType.MediaType != "application/json") {
                    return NotFound("Doguinho Not Found =(");
                }
                var json = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode) {
                    return BadRequest(json);
                }

                List<Dog> DogsList = new List<Dog>();
                //var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Dog>>(json);
                Dog dogresult = new Dog();
                foreach (var dog in result) {
                    dogresult.Id = dog.Id;
                    dogresult.Image.Url = dog.Image.Url;

                }
                return Ok(dogresult);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return Problem("Oops");
            }
        }


        [HttpPost]
        [Route("dog/favourite")]
        [Authorize]
        public async Task<ActionResult<dynamic>>  FavoriteDog([FromBody]Subscription sub) 
        {

            try {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("x-api-key", Settings.DogsApiKey);
                string url = "https://api.thedogapi.com/v1/favourites";  
                StringContent httpContent = new StringContent(
                    JsonConvert.SerializeObject(sub), 
                    System.Text.Encoding.UTF8, 
                    "application/json"
                );

                using var httpResponse = await httpClient.PostAsync(url, httpContent);

                var json = await httpResponse.Content.ReadAsStringAsync();

                //httpResponse.EnsureSuccessStatusCode();
                if (httpResponse.Content !is object && httpResponse.Content.Headers.ContentType.MediaType != "application/json") {
                    return NotFound(json);
                }
                if (!httpResponse.IsSuccessStatusCode) {
                    return BadRequest(json);
                }

                
                return Ok(json);

            } catch (Exception e) {
                return Problem(e.Message);    
            }


        }


        [HttpGet]
        [Route("dog/favourite")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<dynamic>>  GetAllFavourites()
        {
            try {
                string url = Settings.DogsApiUrl+"/favourites";  
                using var httpResponse = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                httpResponse.EnsureSuccessStatusCode();
                if (httpResponse.Content !is object && httpResponse.Content.Headers.ContentType.MediaType != "application/json") {
                    return NotFound("Nenhum doguinho");
                }
                string json =  await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Dog>>(json);   
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return Problem("Oops");
            }
        }
    }
}