using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Repository
{
    public class ProfesorDao
    {

        public RegistroAlumnoContext context = new RegistroAlumnoContext();


        #region GetById
        //creamos un metodo que recibe 2 parametros usuario y contraseña
        //creamos una exprecion lamda que recibe 
        //Usuario -> Usuario ingresado en el body
        //Pass    -> contraseña descrita en el body
        public Profesor  login (string usuario , string pass)
        {
            //Creamo una funcion de flecha where definimos una condicion logia de comparacion
            //p es un objecto temporal que representa al modelo profesor
            //FirstOrDefaul() llama al primero que cumpla con la condicion de lo contrario da null

            var prof = context.Profesors.Where(
                p => p.Usuario == usuario
               && p.Pass == pass).FirstOrDefault();
            //retornamos el resultado de la consulta Lamda
            return prof;


        }
        #endregion

    }
}
