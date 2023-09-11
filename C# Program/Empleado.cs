using System;
using System.Text.RegularExpressions;

namespace perfumesMVC
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Apellido_empleado { get; set; }
        public string Nombre_empleado { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public double Precio { get; set; }
        public DateTime FechaRegistro { get; set; } // Nueva propiedad para la fecha de registro

        public Empleado()
        {
        }

        public Empleado(int id, string cedula, string nombre_empleado, string apellido_empleado, string nombre, string marca,  double precio, DateTime fechaRegistro)
        {
            Id = id;
            Cedula = cedula;
            Apellido_empleado = apellido_empleado;
            Nombre_empleado = nombre_empleado;
            Nombre = nombre;
            Marca = marca;
            
            Precio = precio;
            FechaRegistro = fechaRegistro; // Asigna la fecha de registro
        }

        public Empleado(string cedula, string nombre_empleado, string apellido_empleado, string nombre, string marca, double precio,  DateTime fechaRegistro)
        {
            Cedula = cedula;
            Apellido_empleado = apellido_empleado;
            Nombre_empleado = nombre_empleado;
            Nombre = nombre;
            Marca = marca;
           ;
            Precio = precio;
            FechaRegistro = fechaRegistro; // Asigna la fecha de registro
        }

        public override string ToString()
        {
            return $"ID: {Id}\nCédula: {Cedula}\nNombre_empleado: {Nombre_empleado}Apellido_empleado: {Apellido_empleado}\nNombre: {Nombre}\nMarca: {Marca}\nSueldo: {Precio}\nFecha de Registro: {FechaRegistro}";
        }
    }
}
