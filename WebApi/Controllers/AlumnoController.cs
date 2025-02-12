using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AlumnoController : Controller
    {
        //este elemento representa el modelo de datos de alumno para poder instanciarlo es privado
        private AlumnoDAO _dao = new AlumnoDAO();


        [HttpGet("alumnoProfesor")]

        public List<AlumnoProfesor> GetAlumnoProfesor(string usuario)
        {
            return _dao.alumnoProfesors(usuario);
        }


    }
}
