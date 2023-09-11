using MySql.Data.MySqlClient;
using perfumesMVC;
using System;
using System.Collections.Generic;
using System.Data;

namespace perfumesMVC
{
    public class EmpleadoDaoImpl : IEmpleadoDao
    {
        private const string INSERT_QUERY = "INSERT INTO empleados (cedula, nombre_empleado, apellido_empleado, nombre, marca, precio, FechaRegistro) VALUES (@cedula, @nombre_empleado, @apellido_empleado, @nombre, @marca, @precio,  @FechaRegistro)";
        private const string SELECT_ALL_QUERY = "SELECT * FROM empleados ORDER BY ID";
        private const string UPDATE_QUERY = "UPDATE empleados SET cedula=@cedula, nombre_empleado=@nombre_empleado, apellido_empleado=@apellido_empleado, nombre=@nombre, marca=@marca, precio=@precio ,FechaRegistro =@FechaRegistro, WHERE ID=@id";
        private const string DELETE_QUERY = "DELETE FROM empleados WHERE ID=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM empleados WHERE id=@id";
        private const string SELECT_BY_PRECIO_QUERY = "SELECT * FROM empleados WHERE precio";
        private const string SELECT_ALL_EMPLEADOS_QUERY = "SELECT * FROM empleados";

        private readonly MySqlConnection _connection;

        public EmpleadoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool Registrar(Empleado empleado)
        {
            bool registrado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@cedula", empleado.Cedula);
                    cmd.Parameters.AddWithValue("@nombre_empleado", empleado.Nombre_empleado);
                    cmd.Parameters.AddWithValue("@apellido_empleado", empleado.Apellido_empleado);
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@marca", empleado.Marca);
                    cmd.Parameters.AddWithValue("@precio", empleado.Precio);
                    cmd.Parameters.AddWithValue("@FechaRegistro", empleado.FechaRegistro);

                    cmd.ExecuteNonQuery();

                    empleado.Id = (int)cmd.LastInsertedId;

                    registrado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al registrar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return registrado;
        }

        public List<Empleado> Obtener()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado empleado = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener los empleados", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaEmpleados;
        }

        public bool Actualizar(Empleado empleado)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@cedula", empleado.Cedula);
                    cmd.Parameters.AddWithValue("@nombre_empleado", empleado.Nombre_empleado);
                    cmd.Parameters.AddWithValue("@apellido_empleado", empleado.Apellido_empleado);
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@marca", empleado.Marca);
                    cmd.Parameters.AddWithValue("@precio", empleado.Precio);
                    cmd.Parameters.AddWithValue("@FechaRegistro", empleado.FechaRegistro);
                    cmd.Parameters.AddWithValue("@id", empleado.Id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al actualizar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool Eliminar(Empleado empleado)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", empleado.Id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al eliminar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            Empleado empleado = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = CrearEmpleadoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el empleado por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return empleado;
        }

        public List<Empleado> ObtenerTodosLosEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_EMPLEADOS_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado empleado = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener todos los empleados", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaEmpleados;
        }

        public bool SeleccionCalcularPromedio()
        {
            bool seleccionCalcular = false;
            try
            {

                _connection.Open(); // Abre la conexión
                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_PRECIO_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<double> precios = new List<double>();

                        while (reader.Read())
                        {
                            double precio = reader.GetDouble("Precio"); 
                            precios.Add(precio);
                        }

                        if (precios.Count > 0)
                        {
                            double promedio = precios.Average();

                            // Mostrar el resultado
                            Console.WriteLine($"Promedio de todas las ventas precios: ${promedio}");
                        }
                        else
                        {
                            Console.WriteLine("No hay precios para calcular el promedio.");
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el producto por precio", ex);
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close(); // Cierra la conexión si no es nula
                }
            }

            return seleccionCalcular;
        }

        


        private Empleado CrearEmpleadoDesdeDataReader(MySqlDataReader reader)
        {
            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
            string cedula = reader.GetString("cedula");
            string nombre_empleado = reader.GetString("nombre_empleado");
            string apellido_empleado = reader.GetString("apellido_empleado");
            string nombre = reader.GetString("nombre");
            string marca = reader.GetString("marca");
            double precio = reader.GetDouble("precio");
            DateTime FechaRegistro = reader.GetDateTime("FechaRegistro");

            return new Empleado(id, cedula, nombre_empleado, apellido_empleado, nombre, marca, precio, FechaRegistro);
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}
