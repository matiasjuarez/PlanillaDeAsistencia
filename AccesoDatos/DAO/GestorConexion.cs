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
            connectionStringPlanillaAsistencia = ConfigurationManager.ConnectionStrings["planilla_asistencias"].ConnectionString;
            connectionStringRapla = ConfigurationManager.ConnectionStrings["rapla"].ConnectionString;
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
