using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        //creamoos una instancia de elemento calificaionesDAO
        private CalificacionDAO _cdao = new CalificacionDAO();

        #region Lista de calificaiones 
        [HttpGet("calificaiones")]
        public List<Calificacion> get(int idMatricula)
        {
            //invicamo al metodo calificaionDAO
            return _cdao.seleccion(idMatricula);


        }
        #endregion
    }
}
