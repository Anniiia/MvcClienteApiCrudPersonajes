using ApiCrudPersonajes.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace MvcClienteApiCrudPersonajes.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes";
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);

            return data;
        }
        public async Task<Personaje> FindPersonajeAsync(int idPersonaje)
        {
            string request = "/api/Personajes/" + idPersonaje;
            Personaje data = await this.CallApiAsync<Personaje>(request);

            return data;
        }
        public async Task DeletePersonajesAsync(int idPersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                string request = "api/Personajes/DeletePersonaje/" + idPersonaje;
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage response = await client.DeleteAsync(request);

            }
        }

        public async Task InsertPersonajesAsync(int id, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/InsertPersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);


                Personaje per = new Personaje();
                per.IdPersonaje = id;
                per.Nombre = nombre;
                per.Imagen = imagen;
                per.Serie = serie;

                string json = JsonConvert.SerializeObject(per);


                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);

            }
        }

        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/UpdatePersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                Personaje per = new Personaje();
                per.IdPersonaje= id;
                per.Nombre = nombre;
                per.Imagen = imagen;
                per.Serie = serie;
                string json = JsonConvert.SerializeObject(per);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }

        }

        public async Task<List<string>> GetSeries()
        {
            string request = "api/Personajes/Series";
            List<string> data = await this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(string serie)
        {
            string request = "api/Personajes/PersonajesSerie/" + serie;
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);

            return data;
        }
    }
}
