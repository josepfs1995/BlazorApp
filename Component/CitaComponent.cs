using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorApp.Model;
using BlazorApp.Services;

namespace BlazorApp.Component
{   
    public class CitaComponent:ComponentBase
    {
        [Inject]
        protected ICitasService _citasService { get; set; }
        [Inject]
        protected IJSRuntime JS { get; set; }
        [Inject]
        protected NavigationManager NM { get; set; }
        public IEnumerable<Citas> Citas { get; set; } = new List<Citas>();
        public async Task ObtenerCitas (){
            Citas = await _citasService.GetAll(x=>x.nId_Citas < 2985);
            await llamarJS("citas.nocargando");
        }
        protected override async Task OnInitializedAsync() {
            await llamarJS("citas.cargando");
            await ObtenerCitas();
        }
        protected async Task llamarJS(string callBackName){
             await JS.InvokeAsync<string>(callBackName, null);
        } 
    }
}