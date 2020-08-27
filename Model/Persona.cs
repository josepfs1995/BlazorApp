namespace BlazorApp.Model
{
  public class Persona
  {
    public Persona()
    {

    }
    public Persona(string nombre, string apellido)
    {
      Nombre = nombre;
      Apellido = apellido;
    }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
  }
}