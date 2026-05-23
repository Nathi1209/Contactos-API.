using Microsoft.AspNetCore.Mvc;
using ContactosApi.Models; // Asegúrate de que este sea el namespace de tu carpeta Models

namespace ContactosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        // Lista estática temporal para simular la base de datos
        private static List<Persona> contactos = new List<Persona>
        {
            new Persona { Id = 1, Nombre = "Mel", Telefono = "8888-1234" },
            new Persona { Id = 2, Nombre = "Estudiante UCR", Telefono = "2222-5678" }
        };

        // GET: api/Personas
        [HttpGet]
        public IEnumerable<Persona> Get()
        {
            return contactos;
        }

        // GET api/Personas/1
        [HttpGet("{id}")]
        public ActionResult<Persona> Get(int id)
        {
            var persona = contactos.FirstOrDefault(p => p.Id == id);
            if (persona == null) return NotFound();
            return persona;
        }

        // POST api/Personas
        [HttpPost]
        public ActionResult Post([FromBody] Persona nuevaPersona)
        {
            nuevaPersona.Id = contactos.Max(p => p.Id) + 1;
            contactos.Add(nuevaPersona);
            return Ok(nuevaPersona);
        }

        // DELETE api/Personas/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var persona = contactos.FirstOrDefault(p => p.Id == id);
            if (persona == null) return NotFound();

            contactos.Remove(persona);
            return NoContent();
        }
    }
}