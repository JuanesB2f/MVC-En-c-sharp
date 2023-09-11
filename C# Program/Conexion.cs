// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace perfumesMVC
{
    public sealed  class Conexion
    {
        private static readonly string ConnectionString = "server=localhost;user=root;password=;database=tiendaperfumedb";
        private MySqlConnection _connection;

        private static Conexion _instance = null;
        public static Conexion Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Conexion();
                }
                return _instance;
            }
        }
        public Conexion() 
        {
            _connection = new MySqlConnection(ConnectionString);
        }
        public MySqlConnection AbrirConexion()
        {
            try
            {
                if(_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                    Console.WriteLine("Conectado");

                }          
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            return _connection;
        }
        public void CerrarConexion()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Closed)
                {
                    _connection.Close();
                    Console.WriteLine("Conexión cerrada");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

    }
}
