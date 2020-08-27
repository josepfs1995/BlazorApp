using BlazorApp.Model;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Component
{
  public class PersonaComponent : ComponentBase
  {
    [Inject]
    protected IPersonasService _personaService { get; set; }
    [Inject]
    public IJSRuntime JS { get; set; }
    public IEnumerable<Persona> personas { get; set; } = new List<Persona>();
    public string Nombre = "";
    public string Apellido = "";
    public string Codigo = "";
    public bool isEdit = false;
    public async Task Crear()
    {
      Persona persona = new Persona(Nombre, Apellido);
      var response = await _personaService.Create(persona);
      await response.Match(Error, Success);
    }
    public async Task Editar()
    {
      Persona persona = new Persona(Nombre, Apellido);
      var response = await _personaService.Edit(Codigo, persona);
      await response.Match(Error, Success);
    }
    public async Task GetFromEdit(string id)
    {
      var response = await _personaService.GetById(id);
      Codigo = response.Codigo;
      Nombre = response.Nombre;
      Apellido = response.Apellido;
      isEdit = true;

      StateHasChanged();
    }
    public async Task Eliminar(string id)
    {
      var respuesta = await _personaService.Delete(id);
      if (respuesta)
      {
        await ObtenerPersonas();
        await llamarJS("personas.successDelete", id);
      }
      else await llamarJS("personas.errorDelete");
    }
    public async Task Error(bool resultado)
    {
      await llamarJS("personas.error");
    }
    public async Task Success(Respuesta response)
    {
      await ObtenerPersonas();
      Limpiar();
      await llamarJS("personas.success", response.name);
    }
    public void Limpiar()
    {
      Codigo = "";
      Nombre = "";
      Apellido = "";
      isEdit = false;
    }
    public async Task llamarJS(string callBackName, string id = null)
    {
      await JS.InvokeAsync<string>(callBackName, id);
    }
    public async Task ObtenerPersonas()
    {
      personas = await _personaService.GetAll();
      StateHasChanged();
    }
    protected override async Task OnInitializedAsync()
    {
      await ObtenerPersonas();
    }
  }
}