using perfumesMVC;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace perfumesMVC
{
    class Program
    {
        private static IEmpleadoDao dao = EmpleadoDAOFactory.CrearEmpleadoDAO();

        public static void Main(string[] args)
        {
            string action;

            while (true)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("[L]istar | [R]egistrar | [A]ctualizar | [E]liminar | [C]alcular Promedio | [S]alir:  ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                ListarEmpleados();
                                break;
                            case "R":
                                RegistrarEmpleado();
                                break;
                            case "A":
                                ActualizarEmpleado();
                                break;
                            case "E":
                                EliminarEmpleado();
                                break;
                            case "C":
                                CalcularPromedio();
                                break;
                            case "S":
                                return;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                                break;
                        }
                    }
                    catch (DAOException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        // Calcular el promedio
        private static void CalcularPromedio()
        {
            try
            {
                dao.SeleccionCalcularPromedio();
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al calcular el promedio: " + e.Message);
            }
        }


        private static void RegistrarEmpleado()
        {
            try
            {
                Empleado empleado = InputEmpleado();
                empleado.FechaRegistro = DateTime.Now; // Agregar la fecha de registro actual
                if (dao.Registrar(empleado))
                {
                    Console.WriteLine("Registro exitoso: " + empleado.Id);
                    Console.WriteLine("\n\nCreado: " + empleado);
                }
                else
                {
                    Console.WriteLine("Error al registrar el empleado.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el empleado: " + e.Message);
            }
        }

        private static void ActualizarEmpleado()
        {
            int id = InputId();
            Empleado empleado = dao.ObtenerEmpleadoPorId(id);
            Console.WriteLine("------------Datos originales------------");
            Console.WriteLine(empleado);
            Console.WriteLine("Ingrese los nuevos datos");

            string cedula = InputCedula();
            string nombre_empleado = InputNombre_empleado();
            string apellido_empleado = InputApellido_empleado();
            string nombre = InputNombre();
            string marca = InputMarca();
            double precio = InputPrecio("Ingrese el precio: ");
            DateTime FechaRegistro = InputFechaRegistro("digite la fecha dd/MM/yyyy");


            empleado = new Empleado(id, cedula, nombre_empleado, apellido_empleado, nombre, marca, precio, FechaRegistro);
            try
            {
                if (dao.Actualizar(empleado))
                {
                    Console.WriteLine("Actualización exitosa");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el empleado.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el empleado: " + e.Message);
            }
        }

        private static void EliminarEmpleado()
        {
            int id = InputId();
            Empleado empleado = null;

            try
            {
                empleado = dao.ObtenerEmpleadoPorId(id);
            }
            catch (DAOException daoe)
            {
                Console.WriteLine("Error: " + daoe.Message);
            }

            if (empleado != null && dao.Eliminar(empleado))
            {
                Console.WriteLine("Empleado eliminado: " + empleado.Id);
            }
            else
            {
                Console.WriteLine("Error al eliminar el empleado.");
            }
        }

        private static void ListarEmpleados()
        {
            try
            {
                List<Empleado> todosLosEmpleados = dao.ObtenerTodosLosEmpleados();
                foreach (Empleado empleado in todosLosEmpleados)
                {
                    Console.WriteLine(empleado.ToString() + "\n");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener todos los empleados: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }

        private static Empleado InputEmpleado()
        {
            string cedula = InputCedula();
            string nombre_empleado = InputNombre_empleado();
            string apellido_empleado = InputApellido_empleado();
            string nombre = InputNombre();
            string marca = InputMarca();
            double precio = InputPrecio("Ingrese el precio: ");
            DateTime FechaRegistro = InputFechaRegistro("ingrese la fecha");

            return new Empleado(cedula, nombre_empleado, apellido_empleado, nombre, marca, precio, FechaRegistro);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese un valor entero para el ID del empleado: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return id;
        }

        private static string InputCedula()
        {
            return InputString("Ingrese el número de cédula del empleado: ");
        }

        private static string InputNombre_empleado()
        {
            return InputString("Ingrese el nombre del empleado: ");
        }

        private static string InputApellido_empleado()
        {
            return InputString("Ingrese el apellido del empleado: ");
        }

        private static string InputNombre()
        {
            return InputString("Ingrese el nombre del perfume: ");
        }

        private static string InputMarca()
        {
            return InputString("Ingrese la marca del perfume: ");
        }



        private static double InputPrecio(string message)
        {
            double precio;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (double.TryParse(Console.ReadLine(), out precio))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return precio;
        }


        private static DateTime InputFechaRegistro(string message)
        {
            DateTime FechaRegistro;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FechaRegistro))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de fecha. Por favor, use dd/mm/yyyy.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de fecha. Por favor, use dd/mm/yyyy.");
                }
            }
            return FechaRegistro;
        }


        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                Console.WriteLine(message);
                s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && s.Length >= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("La longitud de la cadena debe ser >= 2");
                }
            }
            return s;
        }
    }
}
