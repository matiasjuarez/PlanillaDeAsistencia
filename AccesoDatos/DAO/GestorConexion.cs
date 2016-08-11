using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using MySql.Data;

using System.Windows.Forms;

using Utilidades;

namespace AccesoDatos
{
    public class GestorConexion
    {
        private MySqlConnection connection;

        // El servidor en el que esta la base de datos
        private string server = "";
        // El nombre de la base de datos
        private string database = "";
        // El nombre de usuario que usaremos para conectarnos
        private string uid = "";
        // La contraseña del usuario que se quiere conectar
        private string password = "";

        // Nos permitira especificar a cual base de datos queremos conectarnos
        // cuando creemos un nuevo objeto gestorConexion
        public static readonly int ConexionRapla = 0;
        public static readonly int ConexionPlanillaAsistencia = 1;


        //Constructor
        public GestorConexion(int baseDeDatosAConectar)
        {
            Initialize(baseDeDatosAConectar);
        }

        // Inicializa la coneccion contra la base de datos seleccionada
        private void Initialize(int baseDeDatosAConectar)
        {
            string connectionString = null;

            if (baseDeDatosAConectar == GestorConexion.ConexionRapla)
            {
                /*server = "localhost";
                database = "rapla2016";
                uid = "matias";
                password = "120491";*/

                server = "tricorder";
                database = "rapla_test";
                uid = "rapla_test";
                password = "rapla_test";
            }
            else if (baseDeDatosAConectar == GestorConexion.ConexionPlanillaAsistencia)
            {
                server = "localhost";
                database = "planilla_asistencia";
                uid = "matias";
                password = "120491";
            }

            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        // Devuelve un objeto MySqlConnecion ya abierto y listo para usar
        public MySqlConnection getConexionAbierta()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (MySqlException ex)
            {
                GestorExcepciones.mostrarExcepcion(ex, "Algo fue mal cuando se intento conectar con la base de datos: ");
                //Environment.Exit(1);
            }
            
            return connection;
        }

        // Cierra la coneccion
        public bool cerrarConexion()
        {
            bool valor = true;

            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open) {
                    connection.Close();
                }
            }
            catch(Exception e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }

            return valor;
        }

        
    }
}
