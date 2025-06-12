using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Cliente.Clases;

namespace Web.Cliente.Controllers
{
    public class PersonaController : Controller
    {

        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonaController (IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];
            _httpClientFactory = httpClientFactory;

        }

        public IActionResult Index()
        {
            return View();
        }
        //--------------------
        //Traer la data como String
        //Medotodo para listar personas sin filtro
        public async Task<List<PersonaCLS>> listarPersonas()
        {
            return await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona");
        }
        //Medotodo para listar personas con filtro
        public async Task<List<PersonaCLS>> filtrarPersonas(string nombrecompleto)
        {
            return await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/" + nombrecompleto);
        }

        public async Task<PersonaCLS> RecuperarPersonaPorId(int id)
        {
            return await ClientHttp.Get<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/recuperarPersonaPorId/" + id);
        }

        //--------------------
    }
}
