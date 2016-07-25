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
        
        public static List<Aula> obtenerTodasLasAulas()
        {
            List<Aula> aulas = new List<Aula>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, descripcion " +
                               "FROM aula "
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
                    Aula aula = new Aula();

                    aula.Id = ValidadorValoresNull.getInt(reader, "id");
                    aula.Nombre = ValidadorValoresNull.getString(reader, "nombre");
                    aula.Descripcion = ValidadorValoresNull.getString(reader, "descripcion");

                    aulas.Add(aula);
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

            return aulas;
        }

        // Devuelve null si no se encuentra un aula con ese id
        public static Aula obtenerAulaPorID(int id)
        {
            Aula aula = null;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, descripcion " +
                               "FROM aula " +
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
                    aula = new Aula();

                    aula.Id = ValidadorValoresNull.getInt(reader, "id");
                    aula.Nombre = ValidadorValoresNull.getString(reader, "nombre");
                    aula.Descripcion = ValidadorValoresNull.getString(reader, "descripcion");
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

            return aula;
        }


        public static Boolean existeAula(Aula aula)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT nombre " +
                               "FROM aula " +
                               "WHERE nombre = @nombre"
                               );

            string consulta = consultaBuilder.ToString();


            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter nombreParam = new MySqlParameter();
            nombreParam.ParameterName = "@nombre";
            nombreParam.Value = aula.Nombre;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(nombreParam);


            try
            {
                // Ejecutamos la consulta
                MySqlDataReader reader = comando.ExecuteReader();

                bool tieneFilas = reader.HasRows;
                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch (MySqlException e)
            {
                
                GestorExcepciones.mostrarExcepcion(e);
                return true;
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }


            return false;
         
        }



        public static void insertarNuevaAula(Aula aula)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                              "insert into aula(nombre, descripcion) " +
                              "values(@nombre, @descripcion)"
                              );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter nombreParam = new MySqlParameter();
            nombreParam.ParameterName = "@nombre";
            nombreParam.Value = aula.Nombre;

            MySqlParameter descripcionParam = new MySqlParameter();
            descripcionParam.ParameterName = "@descripcion";
            descripcionParam.Value = aula.Descripcion;


            // Agregamos los parametros a la consulta
            comando.Parameters.Add(nombreParam);
            comando.Parameters.Add(descripcionParam);
            

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
        

    }


}
