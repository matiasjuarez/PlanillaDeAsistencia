using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;

using MySql.Data.MySqlClient;

using Utilidades;

namespace AccesoDatos
{

    public static class DAOEspecialidades
    {
        /*private static Configuracion.Config configuracion = Configuracion.Config.getInstance();

        public static List<Especialidad> obtenerTodasLasEspecialidades()
        {
            List<Especialidad> especialidades = new List<Especialidad>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, letra " +
                               "FROM especialidad "
                               );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Ejecutamos la consulta
            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Especialidad especialidad = new Especialidad();

                    especialidad.Id = ValidadorValoresNull.getInt(reader, "id", -1);
                    especialidad.Nombre = ValidadorValoresNull.getString(reader, "nombre");
                    especialidad.Letra = ValidadorValoresNull.getString(reader, "letra");

                    especialidades.Add(especialidad);
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

            return especialidades;
        }

        // Devuelve null si no se encuentra una especialidad con ese id
        public static Especialidad obtenerEspecialidadPorID(int id)
        {
            Especialidad especialidad = null;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, letra " +
                               "FROM especialidad " +
                               "WHERE id = @id"
                               );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter idParam = new MySqlParameter();
            idParam.ParameterName = "@id";
            idParam.Value = id;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(idParam);

            // Ejecutamos la consulta
            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    especialidad = new Especialidad();

                    especialidad.Id = ValidadorValoresNull.getInt(reader, "id");
                    especialidad.Nombre = ValidadorValoresNull.getString(reader, "nombre");
                    especialidad.Letra = ValidadorValoresNull.getString(reader, "letra");
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

            return especialidad;
        }*/
    }

}
