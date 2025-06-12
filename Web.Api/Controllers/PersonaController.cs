using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using Web.Api.Models;
using System.Linq.Expressions;
using System.Text.Json;
//DbAba2d0BdveterinariaContext

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {

        //Lista sin filtro
        [HttpGet]
        public List<PersonaCLS> listarPersona()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAba2d0BdveterinariaContext bd = new DbAba2d0BdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + 
                                 persona.Appaterno + " " + 
                                 persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" :
                                 persona.Fechanacimiento.Value.ToString("yyyy-MM-dd")
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


        //Lista con filtro
        [HttpGet("{nombrecompleto}")]
        public List<PersonaCLS> listarPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAba2d0BdveterinariaContext bd = new DbAba2d0BdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1 
                             && (persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno).Contains(nombrecompleto)
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" :
                                 persona.Fechanacimiento.Value.ToShortDateString()
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        //Lista por ID
        [HttpGet("recuperarPersona/{id}")]
        public PersonaCLS recuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();

            try
            {
                using (DbAba2d0BdveterinariaContext bd = new DbAba2d0BdveterinariaContext())
                {
                    oPersonaCLS = (from persona in bd.Personas
                             where persona.Bhabilitado == 1 && persona.Iidpersona == id
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombre = persona.Nombre, 
                                 appaterno = persona.Appaterno,
                                 apmaterno = persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimiento = (DateTime) persona.Fechanacimiento,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? " ":
                                 persona.Fechanacimiento.Value.ToString("yyyy-MM-dd"),
                                 iidsexo = (int)persona.Iidsexo
                             }).First();
                }
                return oPersonaCLS;
            }
            catch (Exception ex)
            {
                return oPersonaCLS;
            }
        }

    }
}

