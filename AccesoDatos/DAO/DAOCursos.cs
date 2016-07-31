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
        // Devuelve null si no se encuentra un curso con ese id
        public static List<Curso> obtenerTodosLosCursos()
        {
            List<Curso> cursos = new List<Curso>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                        "SELECT curso.id as IdCurso, curso.nombre as Nombre " +
                        "FROM curso "
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
                    Curso curso = new Curso();

                    curso.Id = ValidadorValoresNull.getInt(reader, "IdCurso");
                    curso.Nombre = ValidadorValoresNull.getString(reader, "Nombre");

                    cursos.Add(curso);
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

            return cursos;
        }

        // Devuelve null si no se encuentra un curso con ese id
        public static Curso obtenerCursoPorID(int id)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                        "SELECT curso.id as IdCurso, curso.nombre as Nombre " +
                        "FROM curso " +
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
                if (reader.Read())
                {
                    Curso curso = new Curso();

                    curso.Id = ValidadorValoresNull.getInt(reader, "IdCurso");
                    curso.Nombre = ValidadorValoresNull.getString(reader, "Nombre");

                    return curso;
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

            return null;
        }

        public static void insertarCurso(Curso curso)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                        "INSERT INTO curso(Nombre) values(@nombre)"
                        );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter nombreParam = new MySqlParameter();
            nombreParam.ParameterName = "@nombre";
            nombreParam.Value = curso.Nombre;

            comando.Parameters.Add(nombreParam);

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
                gestorConexion.cerrarConexion();
            }
        }

        public static void insertarCursos(List<Curso> cursos)
        {
            foreach (Curso curso in cursos)
            {
                insertarCurso(curso);
            }
        }

    }



}
