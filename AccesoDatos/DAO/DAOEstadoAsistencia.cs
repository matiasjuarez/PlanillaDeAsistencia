using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;

using MySql.Data.MySqlClient;

using Utilidades;

namespace AccesoDatos
{
    public static class DAOEstadoAsistencia
    {
        public static List<EstadoAsistencia> obtenerTodosLosEstadosAsistencia()
        {
            List<EstadoAsistencia> estadosAsistencia = new List<EstadoAsistencia>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            string consulta = "SELECT id, nombre FROM estadoasistencia";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            try
            {
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    EstadoAsistencia estadoAsistencia = new EstadoAsistencia();
                    estadoAsistencia.Id = reader.GetInt32("id");
                    estadoAsistencia.Nombre = reader.GetString("nombre");
                    estadosAsistencia.Add(estadoAsistencia);
                }
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }

            return estadosAsistencia;
        }
    }
}