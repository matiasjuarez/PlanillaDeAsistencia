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
            
            string consulta = "SELECT id, nombre, idJefeCatedra FROM asignatura ORDER BY nombre";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);

            try
            {
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Asignatura asignatura = armarAsignaturaDesdeReader(reader);
                    asignaturas.Add(asignatura);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(comando.Connection); }

            return asignaturas;
        }

        private static Asignatura armarAsignaturaDesdeReader(MySqlDataReader reader)
        {
            Asignatura asignatura = new Asignatura();
            asignatura.Id = reader.GetInt32("id");
            asignatura.Nombre = reader.GetString("nombre");
            asignatura.JefeCatedra = DAODocentes.obtenerDocentePorID(ValidadorValoresNull.getInt(reader, "idJefeCatedra", -1));

            return asignatura;
        }

        // Devuelve null si no se encuentra una asignatura con ese id
        public static Asignatura obtenerAsignaturaPorID(int id)
        {
            Asignatura asignatura = null;
            
            string consulta = "SELECT id, nombre, idJefeCatedra FROM asignatura WHERE id = @id";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            comando.Parameters.AddWithValue("@id", id);

            try
            {
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    asignatura = armarAsignaturaDesdeReader(reader);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(comando.Connection); }

            return asignatura;
        }

        // Comprueba si existe la asignatura en la base de datos de la planilla de asistencias.
        // Basamos la comprobacion en el nombre de la asignatura guardado en la base de datos del rapla 
        // y en el id del jefe de catedra
        public static bool existeAsignatura(Asignatura asignatura)
        {
            string consulta = "SELECT nombre, idJefeCatedra FROM asignatura " + 
                "WHERE nombre = @nombre and idJefeCatedra = @idJefeCatedra";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            comando.Parameters.AddWithValue("@nombre", asignatura.Nombre);
            comando.Parameters.AddWithValue("@idJefeCatedra", asignatura.JefeCatedra.Id);

            try
            {
                MySqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
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

        public static void insertarAsignatura(Asignatura asignatura)
        {
            insertarAsignaturas( new List<Asignatura> { asignatura } );
        }

        public static void insertarAsignaturas(List<Asignatura> asignaturas)
        {
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);

            string consulta = "INSERT INTO asignatura(nombre, idJefeCatedra) VALUES";

            for (int i = 0; i < asignaturas.Count; i++)
            {
                string parametroNombre = "@nombre" + i;
                string parametroIdJefeCatedra = "@idJefeCatedra" + i;

                consulta += "(" + parametroNombre + "," + parametroIdJefeCatedra + "),";

                Asignatura asignatura = asignaturas.ElementAt(i);
                comando.Parameters.AddWithValue(parametroNombre, asignatura.Nombre);
                comando.Parameters.AddWithValue(parametroIdJefeCatedra, asignatura.JefeCatedra.Id);
            }

            consulta = consulta.Substring(0, consulta.Length - 1);
            comando.CommandText = consulta;

            try { comando.ExecuteNonQuery(); }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { GestorConexion.cerrarConexion(comando.Connection); }
        }

        public static void actualizarAsignatura(Asignatura asignatura)
        {
            actualizarAsignaturas( new List<Asignatura> { asignatura } );
        }

        public static void actualizarAsignaturas(List<Asignatura> asignaturas)
        {
            using (var connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia))
            {
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    for (int i = 0; i < asignaturas.Count; i++)
                    {
                        string paramJefeCatedra = "@idJefeCatedra" + i;
                        string paramNombre = "@nombre" + i;
                        string paramId = "@id" + i;

                        string consulta = "UPDATE asignatura SET " +
                            "nombre=" + paramNombre + "," +
                            "idJefeCatedra=" + paramJefeCatedra +
                            " where id = " + paramId;

                        MySqlCommand comando = new MySqlCommand();
                        comando.Connection = connection;
                        comando.CommandText = consulta;
                        comando.Transaction = transaction;

                        Asignatura asignatura = asignaturas.ElementAt(i);
                        comando.Parameters.AddWithValue(paramJefeCatedra, asignatura.JefeCatedra.Id);
                        comando.Parameters.AddWithValue(paramNombre, asignatura.Nombre);
                        comando.Parameters.AddWithValue(paramId, asignatura.Id);

                        comando.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (MySqlException e)
                {
                    transaction.Rollback();
                    GestorExcepciones.mostrarExcepcion(e);
                }
            }
        }
    }
}