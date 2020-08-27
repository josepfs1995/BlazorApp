//using System.Generic.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorApp.Model;
using OneOf;

namespace BlazorApp.Services
{
  public class Respuesta
  {
    public string name { get; set; }
  }
  public interface IPersonasService
  {
    Task<IEnumerable<Persona>> GetAll();
    Task<Persona> GetById(string id);
    Task<OneOf<bool, Respuesta>> Create(Persona model);
    Task<OneOf<bool, Respuesta>> Edit(string id, Persona model);
    Task<bool> Delete(string id);
  }
  public class PersonasService : IPersonasService
  {
    private readonly HttpClient _http;
    public PersonasService()
    {
      _http = new HttpClient()
      {
        BaseAddress = new System.Uri("https://pruebafetch-6073c.firebaseio.com/")
      };
    }
    public async Task<IEnumerable<Persona>> GetAll()
    {
      var response = await _http.GetFromJsonAsync<IDictionary<string, Persona>>("Persona.json");
      List<Persona> personas = new List<Persona>();
      foreach (var item in response)
      {
        item.Value.Codigo = item.Key;
        personas.Add(item.Value);
      }
      return personas;
    }
    public async Task<Persona> GetById(string id)
    {
      var response = await _http.GetFromJsonAsync<Persona>(string.Format("Persona/{0}.json", id));
      response.Codigo = id;
      return response;
    }
    public async Task<OneOf<bool, Respuesta>> Create(Persona model)
    {
      var response = await _http.PostAsJsonAsync("Persona.json", model);
      if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
        return await response.Content.ReadFromJsonAsync<Respuesta>();
      return false;
    }
    public async Task<OneOf<bool, Respuesta>> Edit(string id, Persona model)
    {
      var response = await _http.PutAsJsonAsync(string.Format("Persona/{0}.json", id), model);
      if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
        return new Respuesta
        {
          name = id
        };
      return false;
    }
    public async Task<bool> Delete(string id)
    {
      var response = await _http.DeleteAsync(string.Format("Persona/{0}.json", id));
      return response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK;
    }
  }
}