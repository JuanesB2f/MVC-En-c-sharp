using System;
using System.Collections.Generic;
using System.Text;

namespace perfumesMVC
{
    public class ViewEmpleado
    {
        public void VerEmpleado(Empleado empleado)
        {
            Console.WriteLine("Datos del Empleado:\n" + empleado.ToString());
        }

        public void VerEmpleados(List<Empleado> empleados)
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados para mostrar.");
                return;
            }

            Console.WriteLine("Lista de Empleados:");
            foreach (Empleado empleado in empleados)
            {
                Console.WriteLine("------------");
                Console.WriteLine(empleado.ToString());
            }
        }
    }
}