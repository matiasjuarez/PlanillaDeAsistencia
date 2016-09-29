using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Entidades;
using Utilidades;

namespace AccesoDatos
{
    public static class DAOAulas
    {
        private static Configuracion.Config configuracion = Configuracion.Config.getInstance();

        public static List<Aula> obtenerTodasLasAulas()
        {
            List<Aula> aulas = new List<Aula>();

            string consulta = "SELECT id, nombre, descripcion FROM aula";

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);

            // Ejecutamos la consulta
            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Aula aula = armarAulaDesdeReader(reader);

                    aulas.Add(aula);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(comando.Connection); }

            return aulas;
        }

        // Devuelve null si no se encuentra un aula con ese id
        public static Aula obtenerAulaPorID(int id)
        {
            Aula aula = null;

            string consulta = "SELECT id, nombre, descripcion FROM aula WHERE id = @id";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            comando.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    aula = armarAulaDesdeReader(reader);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(comando.Connection); }

            return aula;
        }

        private static Aula armarAulaDesdeReader(MySqlDataReader reader)
        {
            Aula aula = new Aula();
            aula.Id = ValidadorValoresNull.getInt(reader, "id", -1);
            aula.Nombre = ValidadorValoresNull.getString(reader, "nombre", configuracion.AulaNoAsignada);
            aula.Descripcion = ValidadorValoresNull.getString(reader, "descripcion", "");

            return aula;
        }

        public static Boolean existeAula(Aula aula)
        {
            string consulta = "SELECT nombre FROM aula WHERE nombre = @nombre";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            comando.Parameters.AddWithValue("@nombre", aula.Nombre);

            try
            {
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows) { return true; }
                else return false;
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return true;
            }
            finally
            {
                GestorConexion.cerrarConexion(comando.Connection);
            }
        }

        public static void insertarAula(Aula aula)
        {
            insertarAulas( new List<Aula> { aula } );
        }

        public static void insertarAulas(List<Aula> aulas)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder builder = new StringBuilder("INSERT INTO aula(nombre, descripcion) VALUES");

            for (int i = 0; i < aulas.Count; i++)
            {
                string parametroNombre = "@nombre" + i;
                string parametroDescripcion = "@descripcion" + i;

                builder.Append("(" + parametroNombre + "," + parametroDescripcion + "),");

                Aula aula = aulas.ElementAt(i);
                command.Parameters.AddWithValue(parametroNombre, aula.Nombre);
                command.Parameters.AddWithValue(parametroDescripcion, aula.Descripcion);
            }

            string consulta = builder.ToString();
            consulta = consulta.Substring(0, consulta.Length - 1);

            command.CommandText = consulta;

            try { command.ExecuteNonQuery(); }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(command.Connection); }
        }
    }
}