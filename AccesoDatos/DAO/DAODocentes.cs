using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using MySql.Data.MySqlClient;
using Utilidades;

namespace AccesoDatos
{
    public static class DAODocentes
    {
        public static List<Docente> obtenerTodosLosDocentes()
        {
            List<Docente> docentes = new List<Docente>();

            string consulta = "SELECT id, nombre FROM docente ORDER BY nombre";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    docentes.Add(armarDocenteDesdeReader(reader));
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(comando.Connection); }

            return docentes;
        }

        private static Docente armarDocenteDesdeReader(MySqlDataReader reader)
        {
            Docente docente = new Docente();
            docente.Id = reader.GetInt32("id");
            docente.Nombre = ValidadorValoresNull.getString(reader, "nombre", "");
            return docente;
        }

        // Devuelve null si no se encuentra un docente con ese id
        public static Docente obtenerDocentePorID(int id)
        {

            string consulta = "SELECT id, nombre FROM docente WHERE id=@id ORDER BY nombre";

            MySqlConnection connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return armarDocenteDesdeReader(reader);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(connection); }

            return null;
        }

        public static Docente buscarDocentePorNombre(string nombre)
        {
            string consulta = "SELECT id, nombre FROM docente ORDER BY nombre WHERE nombre=@nombre";

            MySqlConnection connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", nombre);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return armarDocenteDesdeReader(reader);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(connection); }

            return null;
        }

        // Comprueba si existe el docente en la base de datos de la planilla de asistencias.
        // Basamos la comprobacion en el nombre del docente guardado en la base de datos del rapla
        public static bool existeDocente(Docente docente)
        {
            string consulta = "SELECT nombre FROM docente where nombre=@nombre";


            MySqlConnection connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", docente.Nombre);

            try
            {
                // Ejecutamos la consulta
                MySqlDataReader reader = command.ExecuteReader();

                return reader.HasRows;
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return false;
            }
            finally
            {
                GestorConexion.cerrarConexion(connection);
            }
        }

        public static void insertarDocente(Docente docente)
        {
            insertarDocentes( new List<Docente> { docente } );
        }

        public static void insertarDocentes(List<Docente> docentes)
        {
            MySqlCommand command = new MySqlCommand();

            MySqlConnection connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            command.Connection = connection;

            string consulta = "INSERT INTO docente(nombre) VALUES";
            for (int i = 0; i < docentes.Count; i++)
            {
                string parametroNombre = "@nombre" + i;

                consulta += "(" + parametroNombre + "),";

                Docente docente = docentes.ElementAt(i);
                command.Parameters.AddWithValue(parametroNombre, docente.Nombre);
            }

            consulta = consulta.Substring(0, consulta.Length - 1);
            command.CommandText = consulta;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                GestorExcepciones.mostrarExcepcion(ex);
            }
            finally
            {
                GestorConexion.cerrarConexion(connection);
            }
        }
    }
}