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

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "select id, nombre " +
                               "from docente " +
                               "order by nombre"
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
                    Docente docente = new Docente();

                    docente.Id = reader.GetInt32("id");
                    docente.Nombre = reader.GetString("nombre");

                    docentes.Add(docente);
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

            return docentes;
        }

        // Devuelve null si no se encuentra un docente con ese id
        public static Docente obtenerDocentePorID(int id)
        {
            Docente docente = null;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "select id, nombre " +
                               "from docente " +
                               "where id = @id"
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
                    docente = new Docente();

                    docente.Id = reader.GetInt32("id");
                    docente.Nombre = reader.GetString("nombre");
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

            return docente;
        }


        public static Docente buscarDocentePorNombre(Docente docente)
        {

            Docente docenteRecuperado = null;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre " +
                               "FROM docente " +
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
            nombreParam.Value = docente.Nombre;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(nombreParam);


            try
            {
                // Ejecutamos la consulta
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    docenteRecuperado = new Docente(reader.GetString("nombre")) ;
                    docenteRecuperado.Id = reader.GetInt32("id");
                    
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


            return docenteRecuperado;
        }

        // Comprueba si existe el docente en la base de datos de la planilla de asistencias.
        // Basamos la comprobacion en el nombre del docente guardado en la base de datos del rapla

        public static bool existeDocente(Docente docente)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT nombre " +
                               "FROM docente " +
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
            nombreParam.Value = docente.Nombre;

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


        // Inserta el docente pasado como parametro en la base de datos
        public static void insertarDocente(Docente docente)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            Insert insert = crearInsert(docente);

            insert.ejecutar(gestorConexion.getConexionAbierta());

            gestorConexion.cerrarConexion();
        }

        public static void insertarDocentes(List<Docente> docentes)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            InsertMultiple insert = new InsertMultiple();

            foreach (Docente docente in docentes)
            {
                insert.agregarInsert(crearInsert(docente));
            }

            insert.ejecutar(gestorConexion.getConexionAbierta());

            gestorConexion.cerrarConexion();
        }

        private static Insert crearInsert(Docente docente)
        {
            Insert insert = new Insert();
            insert.setNombreTabla("docente");
            insert.agregarCampo("nombre");
            insert.agregarParametro("@nombre", docente.Nombre);

            return insert;
        }
    }
}
