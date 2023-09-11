using MySql.Data.MySqlClient;
using perfumesMVC;

namespace perfumesMVC
{
    public class EmpleadoDAOFactory
    {
        public static IEmpleadoDao CrearEmpleadoDAO()
        {
            MySqlConnection connection = Conexion.Instance.AbrirConexion();
            return new EmpleadoDaoImpl(connection);
        }
    }
}
