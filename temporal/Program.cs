using reactBackend.Models;
using reactBackend.Repository;

//abstraccion de un objecto DAO
AlumnoDAO alumnoDAO = new AlumnoDAO();
//llamamos el metodo que creamos en el DAO
var alumno = alumnoDAO.SelectAll();
//recorremos la lista
foreach ( var item in alumno)
{
    Console.WriteLine(item.Nombre);
}
Console.WriteLine("");
#region selectById
//probamos el metodo que creamos en el DAO SelelctById
var selectById = alumnoDAO.GetById(1);
Console.WriteLine(selectById?.Nombre);
#endregion

#region addAlumno
var nuevoAlumno = new Alumno {
    Direccion = "Dulce Nombre de Maria, Barrio Concepcion",
    Dni = "12345",
    Edad = 30,
    Email = "12345@email.com",
    Nombre = "Josue "
};
var resultado = alumnoDAO.insertarAlumno(nuevoAlumno);
Console.WriteLine(resultado);

#endregion

#region updateAlumno
var nuevoAlumno2 = new Alumno {

    Direccion = "Dulce Nombre de Maria, Barrio Concepcion",
    Dni = "12345",
    Edad = 32,
    Email = "12345@email.com",
    Nombre = "Josue Aaron Castillo"

};
var resultado2 = alumnoDAO.update(2, nuevoAlumno2);
Console.WriteLine(resultado2);

#endregion

#region borrar 

var result = alumnoDAO.delete(18);
Console.WriteLine("se eliminio el usuario " +result);

#endregion

#region alumnoAsignatura desde JOIN

var alumAsig = alumnoDAO.SelectAlumAsig();
foreach (AlumnoAsignatura alumAsig2 in alumAsig)
{
    Console.WriteLine(alumAsig2.nombreAlumno+" Asignatura que cursa "+alumAsig2.nombreAsigantura);
}

#endregion
