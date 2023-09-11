using System;
using System.Collections.Generic;
using System.Text;

namespace perfumesMVC
{
    public interface IEmpleadoDao
    {
        bool Registrar(Empleado empleado);
        List<Empleado> Obtener();
        bool Actualizar(Empleado empleado);
        bool Eliminar(Empleado empleado);
        Empleado ObtenerEmpleadoPorId(int id);
        List<Empleado> ObtenerTodosLosEmpleados();
        //
        bool SeleccionCalcularPromedio();
        //List<DescProducto> CalcularDescuentoVentas();
    }
}
