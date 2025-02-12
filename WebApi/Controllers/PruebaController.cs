using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PruebaController : ControllerBase
    {
        #region prueba
        //indica la url
        [HttpGet("prueba")]
        //metodo publico que se ejecutara si la url es llamada
        public string Get()
        {
            return "Hola Mundo";
        }
        #endregion
    }

}
