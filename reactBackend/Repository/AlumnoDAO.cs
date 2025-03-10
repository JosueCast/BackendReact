﻿using reactBackend.Context;
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



        public Alumno? GetByIdAlumno(int id)
        {
            var alumno = context.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }


        #region update alumno 

        public bool updateAlumno(int id, Alumno actualizar)
        {
            try
            {
                var alumnoUpdate = GetById(id);

                if (alumnoUpdate == null)
                {
                    Console.WriteLine("Alumno es null");
                    return false;
                }
                //AGREGAR CAMPO QUE SE PUEDAN ACTUALIZAR O VALIDAR GG FUTURA UPDATE
                alumnoUpdate.Direccion = actualizar.Direccion;
                alumnoUpdate.Dni = actualizar.Dni;
                alumnoUpdate.Nombre = actualizar.Nombre;
                alumnoUpdate.Email = actualizar.Email;
                alumnoUpdate.Edad = actualizar.Edad;

                context.Alumnos.Update(alumnoUpdate);
                context.SaveChanges();
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al actualizar el alumno: {e.Message}");
                if (e.InnerException != null)
                {
                    Console.WriteLine($"Detalles: {e.InnerException.Message}");
                }
                return false;
            }
        }


        #endregion



        #region SelccionarPorDni
        /// <summary>
        /// Este metodo devolvera null si no exiate el DNI indicado, recibe un alumno y apartir de el se extrae el DNI se devuelve el estudiandiante en caso de exito
        /// </summary>
        /// <param name="alumno"> es de tipo Alumno </param>
        /// <returns> Alumno </returns>

        public Alumno DNIAlumno(Alumno alumno)
        {
            var alumnos = context.Alumnos.Where(x => x.Dni == alumno.Dni).FirstOrDefault();
            return alumnos == null ? null : alumnos;
        }
        #endregion
        #region AlumnoMatricula
        public bool InsartarMatricula(Alumno alumno, int idAsing)
        {
            // se utiliza  un bloque con el cual  detectaremos las exepciones que nos pueda dar la inserccion 
            try
            {

                //comprobar si existe el DNI en los alumnos
                var alumnoDNI = DNIAlumno(alumno);
                //si existe solo lo añadimos pero si no lo debemos de insertar
                if (alumnoDNI == null)
                {
                    insertarAlumno(alumno);
                    // si en null creamos el alumno pero ahora debemos de matricular el alumno con el Dni que corresponda
                    var alumnoInsertado = DNIAlumno(alumno);
                    // ahora debemos crear un objeto matricula para poder hacer la insercion de ambas llaves
                    var unirAlumnoMatricula = matriculaAsignaturaALumno(alumno, idAsing);
                    if (unirAlumnoMatricula == false)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    matriculaAsignaturaALumno(alumnoDNI, idAsing);
                    return true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Matriucla
        /// <summary>
        /// Relaciona el Id del alumno con el ID de la matricula 
        /// se definel el id de la asignatura
        /// Para ello el metodo crea una instancia de Matricula he inicializa los campos idAlumno e id Asignatura
        /// si el registro se guarda  devuelve true de lo contrario False
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="idAsignatura"></param>
        /// <returns>  bool</returns>
        public bool matriculaAsignaturaALumno(Alumno alumno, int idAsignatura)
        {
            try
            {
                Matricula matricula = new Matricula();
                //usaremos los campos AlumnoId y asignaturaId
                matricula.AlumnoId = alumno.Id;
                matricula.AsignaturaId = idAsignatura;
                // Guardamos el cambio que se realizo al momento de insertar.
                context.Matriculas.Add(matricula);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion


        public bool eliminarAlumno(int id)
        {
            try
            {
                // Buscar al alumno
                var alumno = context.Alumnos.FirstOrDefault(x => x.Id == id);
                if (alumno != null)
                {
                    // Cargar matrículas en memoria
                    var matriculaA = context.Matriculas.Where(x => x.AlumnoId == id).ToList(); // ✅ Ejecuta la consulta aquí

                    // Eliminar calificaciones asociadas
                    foreach (var m in matriculaA)
                    {
                        var calificaciones = context.Calificacions.Where(x => x.MatriculaId == m.Id).ToList(); // ✅ Convertir en lista
                        context.Calificacions.RemoveRange(calificaciones);
                    }

                    // Eliminar matrículas
                    context.Matriculas.RemoveRange(matriculaA);
                    context.Alumnos.Remove(alumno);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("Alumno no encontrado");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }

        }



    }
}
