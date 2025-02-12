using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]

    public class ProfesorController : Controller
    {
        private ProfesorDao _proDao = new ProfesorDao();
        //se crea un endpoint con el metodo httpost
        //api/autenticacion
        [HttpPost("autenticacion")]
        public string loginProfesor([FromBody] Profesor prof)
        {
            var prof1 = _proDao.login(prof.Usuario, prof.Pass);
            if (prof1 != null)
            {
                return prof1.Usuario;
            }
            return "Elemento no encontrado\n !!USUARIO o CONTRASEÑA INCORRECTOS!!";

        }
    }

   


}
