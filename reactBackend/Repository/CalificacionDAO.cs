using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Repository
{
    public class CalificacionDAO
    {
        private RegistroAlumnoContext _contexto = new RegistroAlumnoContext();


        #region Seleccionar_lista_caificaciones 
        public List<Calificacion> seleccion(int matriculaid)
        {

            var matricula = _contexto.Matriculas.Where(x => x.Id == matriculaid);
            Console.WriteLine("matricula encontrada");
            try
            {
                if (matricula != null)
                {

                    var calificacion = _contexto.Calificacions.Where(x => x.Id == matriculaid).ToList();
                    return calificacion;
                }
                else
                {

                    return null;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }


        }
        #endregion
    }
}
