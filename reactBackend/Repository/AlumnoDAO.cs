using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Repository
{
    public class AlumnoDAO
    {
        #region Contex
        //para hacer cualquier opracio con de datos debemos llamar al contexto
        // -> la peticion llama al contexto
        // -> contexto verifica el dataset
        // -> el data set mediante su datatable se actualiza
        // -> el contexto mediante su metodo save guarda las actualizaciones , delete o insert
        // -> devuelve el tipo de correspondiente de error o peticion.

        public RegistroAlumnoContext context = new RegistroAlumnoContext();

        #endregion

        #region Select All
        //se utiliza para selecionar un elemento alumno de la base de datos
        //</sumary>
        //<param name="T"> t es un modelo de sql 
        //<returns>Lista de elemntos del modelo que se ingrese</returns>
        public List<Alumno> SelectAll()
        {
            //->Creamos una variable var que es generica
            //->el contexto tiene referencia todo los modelos
            //-> dentro de el tenemos el metodo modelo.ToList<Modelo>
            var alumno = context.Alumnos.ToList<Alumno>();
            return alumno;

        }
        #endregion

        #region SelectById
        //<sumary>
        //este metodo nos devolvera el objecto que contenga el primer ID que encuentre y
        //coincida con el que se pase como parametro modelo indica que aceptar nulo

        //</sumary>
        //param name id entero que sera eol ID del elemento a retorno
        //return =un obejcto

        public Alumno GetById(int id)
        {
            var alumno = context.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion

        #region Insert
        public bool insertarAlumno(Alumno alumno)
        {

            try
            {
                var alum = new Alumno
                {
                    Direccion = alumno.Direccion,
                    Edad = alumno.Edad,
                    Email = alumno.Email,
                    Dni = alumno.Dni,
                    Nombre = alumno.Nombre
                };
                //añaddimos al contexto de dataset que representa la base de datos el metodo add
                context.Alumnos.Add(alum);
                //este elemnto en si no nos guardara los datos para ello debemos utilizar el metodo save
                context.SaveChanges();
                return true;

            }catch (Exception ex)
            {
                return false;
            }


        }

        #endregion

        #region update alumno
        public bool update(int id, Alumno actualizar)
        {
            try
            {
                var alumnoUpdate = GetById(id);

                if (alumnoUpdate == null)
                {
                    Console.WriteLine("Alumno es null");
                    return false;
                }

                alumnoUpdate.Direccion = actualizar.Direccion;
                alumnoUpdate.Dni = actualizar.Dni;
                alumnoUpdate.Nombre = actualizar.Nombre;
                alumnoUpdate.Email = actualizar.Email;

                context.Alumnos.Update(alumnoUpdate);
                context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        #endregion

        #region Delete

        public bool delete(int id)
        {
            var borrar = GetById(id);
            try
            {
                if (borrar == null)
                {
                    return false;

                }
                else
                {
                    context.Alumnos.Remove(borrar);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        #endregion


        # region LeftJoin

        public List<AlumnoAsignatura> SelectAlumAsig()
        {
            var consulta = from a in context.Alumnos
                           join m in context.Matriculas on a.Id equals m.AlumnoId
                           join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                           select new AlumnoAsignatura {
                           
                                nombreAlumno = a.Nombre,
                                nombreAsigantura = asig.Nombre
                           
                           };

            return consulta.ToList();
            
        }

        #endregion

        #region leftJoinAlumnoMatreiculaMateria
        public List<AlumnoProfesor> alumnoProfesors(string nombreProfesor)
        {
            var listadoALumno = from a in context.Alumnos
                                join m in context.Matriculas on a.Id equals m.AlumnoId
                                join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                                where asig.Profesor == nombreProfesor
                                select new AlumnoProfesor
                                {
                                    Id = a.Id,
                                    Dni = a.Dni,
                                    Nombre = a.Nombre,
                                    Direccion = a.Direccion,
                                    Edad = a.Edad,
                                    Email = a.Email,
                                    asignatura = asig.Nombre
                                };

            return listadoALumno.ToList();
        }
        #endregion


    }
}
