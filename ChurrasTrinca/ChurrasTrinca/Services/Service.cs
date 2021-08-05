using ChurrasTrinca.Models;
using ChurrasTrinca.Models.ResponseService;
using ChurrasTrinca.ViewModel.Bbq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static System.Net.WebRequestMethods;

namespace ChurrasTrinca.Services
{
    public class Service
    {
        public string tok { get; set; }

        private static Service _ServiceClienteInstance;
        public static Service ServiceClientInstance
        {
            get
            {
                if (_ServiceClienteInstance == null)
                    _ServiceClienteInstance = new Service();
                return _ServiceClienteInstance;
            }
        }
        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient client;


        public Service()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://trinca-api.herokuapp.com");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.ToString();
        }

        public async Task<ResponseService<CredentialResponse>> AuthenticateUserAsync(string Username, string Password)
        {
            Credential LoginModel = new Credential()
            {
                username = Username,
                password = Password

            };
            var content = new StringContent(JsonConvert.SerializeObject(LoginModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://trinca-api.herokuapp.com/bbq/auth", content);
            ResponseService<CredentialResponse> responseService = new ResponseService<CredentialResponse>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<ResponseService<CredentialResponse>>(json);
                    Preferences.Set("authToken", jsoncontent.token);
                    tok = jsoncontent.token;
                    return jsoncontent;
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Bbq>> PostBbq(Bbq bbq)
        {

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            var content = new StringContent(JsonConvert.SerializeObject(bbq), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://trinca-api.herokuapp.com/bbq/", content);

            ResponseService<Bbq> responseService = new ResponseService<Bbq>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<ResponseService<Bbq>>(json);
                    return jsoncontent;
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }



        public async Task<ResponseService<Bbq>> putBbq(Bbq bbq)
        {

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            var content = new StringContent(JsonConvert.SerializeObject(bbq), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("https://trinca-api.herokuapp.com/bbq/", content);

            ResponseService<Bbq> responseService = new ResponseService<Bbq>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<ResponseService<Bbq>>(json);
                    return jsoncontent;
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }
        //        public async Task<ResponseService<List<Bbq>>> GetAllBbq(int numberOfPage = 1)
        public async Task<ResponseService<List<Bbq>>> GetAllBbq()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            //  HttpResponseMessage response = await client.GetAsync($"https://trinca-api.herokuapp.com/bbq/?paginated=true&page={numberOfPage}");
            HttpResponseMessage response = await client.GetAsync($"https://trinca-api.herokuapp.com/bbq/");
            ResponseService<List<Bbq>> responseService = new ResponseService<List<Bbq>>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseService<List<Bbq>>>(content);
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }

            return responseService;

        }


        public async Task<ResponseService<Participants>> PostUser(Participants participants)
        {


            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            var content = new StringContent(JsonConvert.SerializeObject(participants), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://trinca-api.herokuapp.com/bbq/participant", content);

            ResponseService<Participants> responseService = new ResponseService<Participants>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return responseService;
            }
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<ResponseService<Participants>>(json);
                    return jsoncontent;
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Participants>> putUser(Participants participants)
        {


         /*   Participants participants = new Participants()
            {
                BbqId = bbqid,
                name = Name,
                confirmed = Confirmed,
                value_paid = Value_Paid


            };*/
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            var content = new StringContent(JsonConvert.SerializeObject(participants), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("https://trinca-api.herokuapp.com/bbq/participant", content);

            ResponseService<Participants> responseService = new ResponseService<Participants>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<ResponseService<Participants>>(json);
                    return jsoncontent;
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }


        public async Task<ResponseService<List<Participants>>> GetAllUsers(int id)
        {

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            HttpResponseMessage response = await client.GetAsync($"https://trinca-api.herokuapp.com/bbq/participants/{id}");
            ResponseService<List<Participants>> responseService = new ResponseService<List<Participants>>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return responseService;
            }

            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseService<List<Participants>>>(content);
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }

            return responseService;

        }

        public async Task<ResponseService<Bbq>> DeleteBbq(int id)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            HttpResponseMessage response = await client.DeleteAsync($"https://trinca-api.herokuapp.com/bbq/{id}");
            ResponseService<Bbq> responseService = new ResponseService<Bbq>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseService<Bbq>>(content);
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }

            return responseService;
        }
        public async Task<ResponseService<Participants>> DeleteParticipant(int id)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tok);
            HttpResponseMessage response = await client.DeleteAsync($"https://trinca-api.herokuapp.com/bbq/participant/{id}");
            ResponseService<Participants> responseService = new ResponseService<Participants>();
            responseService.isSucess = response.IsSuccessStatusCode;
            responseService.statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseService<Participants>>(content);
                }
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<CredentialResponse>>(problemResponse);
                responseService.Errors = erros.Errors;
            }

            return responseService;
        }
    }
}
