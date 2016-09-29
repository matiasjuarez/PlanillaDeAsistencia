using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Entidades;

using Utilidades;

namespace AccesoDatos
{
    public static class DAOCursos
    {
        private static Configuracion.Config configuracion = Configuracion.Config.getInstance();
        // Devuelve null si no se encuentra un curso con ese id
        public static List<Curso> obtenerTodosLosCursos()
        {
            List<Curso> cursos = new List<Curso>();

            string consulta = "SELECT curso.id as IdCurso, curso.nombre as Nombre FROM curso";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            comando.Connection = conexion;

            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Curso curso = new Curso();
                    curso.Id = ValidadorValoresNull.getInt(reader, "IdCurso", -1);
                    curso.Nombre = ValidadorValoresNull.getString(reader, "Nombre", configuracion.CursoNoAsignado);
                    cursos.Add(curso);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(conexion); }

            return cursos;
        }

        // Devuelve null si no se encuentra un curso con ese id
        public static Curso obtenerCursoPorID(int id)
        {
            string consulta = "SELECT curso.id as IdCurso, curso.nombre as Nombre FROM curso WHERE id = @id";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            comando.Connection = conexion;
            comando.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    Curso curso = new Curso();
                    curso.Id = ValidadorValoresNull.getInt(reader, "IdCurso", -1);
                    curso.Nombre = ValidadorValoresNull.getString(reader, "Nombre", configuracion.CursoNoAsignado);
                    return curso;
                }
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                GestorConexion.cerrarConexion(conexion);
            }

            return null;
        }

        public static void insertarCurso(Curso curso)
        {
            insertarCursos( new List<Curso> { curso } );
        }

        public static void insertarCursos(List<Curso> cursos)
        {
            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion;

            string consulta = "INSERT INTO curso(Nombre) VALUES";

            for (int i = 0; i < cursos.Count; i++)
            {
                string parametroNombre = "@nombre" + i;

                consulta += "(" + parametroNombre + "),";

                Curso curso = cursos.ElementAt(i);
                comando.Parameters.AddWithValue(parametroNombre, curso.Nombre);
            }

            consulta = consulta.Substring(0, consulta.Length - 1);
            comando.CommandText = consulta;

            try
            {
                comando.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                GestorConexion.cerrarConexion(conexion);
            }
        }
    }
}