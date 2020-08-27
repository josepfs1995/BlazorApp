using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using BlazorApp.Model;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System;

namespace BlazorApp.Services
{
    public interface ICitasService
    {
        Task<IEnumerable<Citas>> GetAll(Func<Citas, bool> predicate);
    }
    public class CitasService:ICitasService
    {
        private readonly HttpClient _http;
        public CitasService()
        {
            _http = new HttpClient(){
                BaseAddress = new System.Uri("http://10.10.10.43:8091/api/")
            };
        }
        public async Task<IEnumerable<Citas>> GetAll(Func<Citas, bool> predicate){
             return (await _http.GetFromJsonAsync<Response<IEnumerable<Citas>>>("Citas/5")).data.Where(predicate) ?? new List<Citas>();
        }
    }
}