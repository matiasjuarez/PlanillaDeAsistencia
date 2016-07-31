using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;

using MySql.Data.MySqlClient;

using Utilidades;

namespace AccesoDatos
{
    public static class DAOAsignaturas
    {

        public static List<Asignatura> obtenerTodasLasAsignaturas()
        {
            List<Asignatura> asignaturas = new List<Asignatura>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, idJefeCatedra " +
                               "FROM asignatura " +
                               "ORDER BY nombre"
                               );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            try
            {
                // Ejecutamos la consulta
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Asignatura asignatura = new Asignatura();

                    asignatura.Id = reader.GetInt32("id");
                    asignatura.Nombre = reader.GetString("nombre");
                    asignatura.JefeCatedra = DAODocentes.obtenerDocentePorID(reader.GetInt32("idJefeCatedra"));

                    asignaturas.Add(asignatura);
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

            return asignaturas;
        }

        // Devuelve null si no se encuentra una asignatura con ese id
        public static Asignatura obtenerAsignaturaPorID(int id)
        {
            Asignatura asignatura = null;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, idJefeCatedra " +
                               "FROM asignatura " +
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

            try
            {
                // Ejecutamos la consulta
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    asignatura = new Asignatura();

                    asignatura.Id = reader.GetInt32("id");
                    asignatura.Nombre = reader.GetString("nombre");
                    asignatura.JefeCatedra = DAODocentes.obtenerDocentePorID(reader.GetInt32("idJefeCatedra"));
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

            return asignatura;
        }



        // Comprueba si existe la asignatura en la base de datos de la planilla de asistencias.
        // Basamos la comprobacion en el nombre de la asignatura guardado en la base de datos del rapla 
        // y en el id del jefe de catedra

        public static bool existeAsignatura(Asignatura asignatura)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT nombre, idJefeCatedra " +
                               "FROM asignatura " +
                               "WHERE nombre = @nombre and idJefeCatedra = @idJefeCatedra"
                               );

            string consulta = consultaBuilder.ToString();


            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter nombreParam = new MySqlParameter();
            nombreParam.ParameterName = "@nombre";
            nombreParam.Value = asignatura.Nombre;

            MySqlParameter idJefeCatedraParam = new MySqlParameter();
            idJefeCatedraParam.ParameterName = "@idJefeCatedra";
            idJefeCatedraParam.Value = asignatura.JefeCatedra.Id;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(nombreParam);
            comando.Parameters.Add(idJefeCatedraParam);


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


        // Inserta la asignatura pasada como parametro en la base de datos
        public static void insertarAsignatura(Asignatura asignatura)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                              "insert into asignatura(nombre, idJefeCatedra) " +
                              "values(@nombre, @idJefeCatedra)"
                              );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter nombreParam = new MySqlParameter();
            nombreParam.ParameterName = "@nombre";
            nombreParam.Value = asignatura.Nombre;

            MySqlParameter idJefeCatedraParam = new MySqlParameter();
            idJefeCatedraParam.ParameterName = "@idJefeCatedra";
            idJefeCatedraParam.Value = asignatura.JefeCatedra.Id;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(nombreParam);
            comando.Parameters.Add(idJefeCatedraParam);

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

        public static void insertarAsignaturas(List<Asignatura> asignaturas)
        {
            foreach (Asignatura asignatura in asignaturas)
            {
                insertarAsignatura(asignatura);
            }
        }

        public static void actualizarAsignatura(Asignatura asignatura)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                              "UPDATE asignatura set nombre=@nombre, idJefeCatedra=@idJefeCatedra) " +
                              "where id = @id"
                              );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter nombreParam = new MySqlParameter();
            nombreParam.ParameterName = "@nombre";
            nombreParam.Value = asignatura.Nombre;

            MySqlParameter idJefeCatedraParam = new MySqlParameter();
            idJefeCatedraParam.ParameterName = "@idJefeCatedra";
            idJefeCatedraParam.Value = asignatura.JefeCatedra.Id;

            MySqlParameter idAsignatura = new MySqlParameter();
            idAsignatura.ParameterName = "@id";
            idAsignatura.Value = asignatura.Id;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(nombreParam);
            comando.Parameters.Add(idJefeCatedraParam);
            comando.Parameters.Add(idAsignatura);

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

        public static void actualizarAsignaturas(List<Asignatura> asignaturas)
        {
            foreach (Asignatura asignatura in asignaturas)
            {
                actualizarAsignatura(asignatura);
            }
        }
    }
}
