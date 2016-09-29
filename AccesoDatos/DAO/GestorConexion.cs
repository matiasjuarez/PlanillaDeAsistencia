using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using MySql.Data.MySqlClient;
using MySql.Data;

using System.Windows.Forms;

using Utilidades;

namespace AccesoDatos
{
    public class GestorConexion
    {
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

        private string connectionStringPlanillaAsistencia = "";
        private string connectionStringRapla = "";

        private static GestorConexion instanciaGestorConexion;

        public static GestorConexion getInstance()
        {
            if (instanciaGestorConexion == null)
            {
                instanciaGestorConexion = new GestorConexion();
            }

            return instanciaGestorConexion;
        }

        //Constructor
        private GestorConexion()
        {
            Initialize();
        }

        // Inicializa la coneccion contra la base de datos seleccionada
        private void Initialize()
        {
            string connectionString = null;
                /*server = "localhost";
                database = "rapla2016";
                uid = "matias";
                password = "120491";*/

                /*server = "localhost";
                database = "rapla_test";
                uid = "matias";
                password = "120491";*/

                /*server = "tricorder";
                database = "rapla_test";
                uid = "rapla_test";
                password = "rapla_test";*/

                connectionStringPlanillaAsistencia = ConfigurationManager.ConnectionStrings["planilla_asistencias"].ConnectionString;
                connectionStringRapla = ConfigurationManager.ConnectionStrings["rapla"].ConnectionString;
                /*server = "localhost";
                database = "planilla_asistencia";
                uid = "matias";
                password = "120491";*/
            

            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            //connection = new MySqlConnection(connectionString);
        }


        // Devuelve un objeto MySqlConnecion ya abierto y listo para usar
        public MySqlConnection getConexion(int baseDeDatosAConectar)
        {
            MySqlConnection conexion = null;

            try
            {
                if(baseDeDatosAConectar == GestorConexion.ConexionPlanillaAsistencia)
                {
                    conexion = new MySqlConnection(connectionStringPlanillaAsistencia);
                }
                else if (baseDeDatosAConectar == GestorConexion.ConexionRapla)
                {
                    conexion = new MySqlConnection(connectionStringRapla);
                }
                else
                {
                    throw new ArgumentException("El valor: " + baseDeDatosAConectar + " no es valido");
                }

                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                GestorExcepciones.mostrarExcepcion(ex, "Algo fue mal cuando se intento conectar con la base de datos: ");
                return null;
                //Environment.Exit(1);
            }
        }

        // Cierra la coneccion
        public static bool cerrarConexion(MySqlConnection conexion)
        {
            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }

                return true;
            }
            catch(Exception e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return false;
            }
        }

        
    }
}
