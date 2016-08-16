using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Entidades;

using Utilidades;

using System.Drawing;

using System.IO;

namespace AccesoDatos
{
    public static class DAOEncargados
    {
        // Devuelve null si no se encuentra un curso con ese id
        public static Encargado obtenerEncargadoPorID(int id)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consulta = new StringBuilder(obtenerSelectBasico());
            consulta.Append(" WHERE id = @id");

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta.ToString();
            comando.Connection = gestorConexion.getConexionAbierta();

            comando.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    return armarEncargado(reader);
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

        public static List<Encargado> obtenerTodosLosEncargados()
        {
            List<Encargado> encargados = new List<Encargado>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = obtenerSelectBasico();
            comando.Connection = gestorConexion.getConexionAbierta();

            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    encargados.Add( armarEncargado(reader) );
                }

                return encargados;
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return null;
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }

        public static bool insertarEncargado(Encargado encargado)
        {
            if (encargado == null) return false;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = obtenerInsert(encargado);
            comando.Connection = gestorConexion.getConexionAbierta();
            comando.Parameters.AddWithValue("@Nombre", encargado.Nombre);
            comando.Parameters.AddWithValue("@Apellido", encargado.Apellido);
            comando.Parameters.AddWithValue("@Telefono", encargado.Telefono);
            comando.Parameters.AddWithValue("@Dni", encargado.Dni);
            comando.Parameters.AddWithValue("@FechaNacimiento", encargado.FechaNacimiento);
            comando.Parameters.AddWithValue("@Legajo", encargado.Legajo);
            comando.Parameters.AddWithValue("@MailGeneral", encargado.MailGeneral);
            comando.Parameters.AddWithValue("@MailBBS", encargado.MailBBS);
            comando.Parameters.AddWithValue("@Foto", encargado.Foto);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return false;
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }

        private static Encargado armarEncargado(MySqlDataReader reader)
        {
            Encargado encargado = new Encargado();

            encargado.Id = reader.GetInt32("id");
            encargado.Nombre = reader.GetString("nombre");
            encargado.Apellido = reader.GetString("apellido");
            encargado.Dni = reader.GetString("DNI");
            encargado.FechaNacimiento = reader.GetDateTime("fechaNacimiento");
            encargado.Legajo = reader.GetString("legajo");
            encargado.MailGeneral = reader.GetString("mailGeneral");
            encargado.MailBBS = reader.GetString("mailBBS");
            encargado.Foto = obtenerFotoDeBaseDeDatos(reader.GetByte("foto"));

            return encargado;
        }

        private static string obtenerSelectBasico()
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("SELECT id, nombre, apellido, telefono, DNI, ");
            consulta.Append("fechaNacimiento, legajo, mailGeneral, mailBBS, foto ");
            consulta.Append("FROM encargado");

            return consulta.ToString();
        }

        private static string obtenerInsert(Encargado encargado)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("INSERT INTO encargado(nombre, apellido, telefono, DNI, ");
            consulta.Append("fechaNacimiento, legajo, mailGeneral, mailBBS, foto) ");
            consulta.Append("VALUES(");
            consulta.Append("@Nombre, ");
            consulta.Append("@Apellido, ");
            consulta.Append("@Telefono, ");
            consulta.Append("@Dni, ");
            consulta.Append("@FechaNacimiento, ");
            consulta.Append("@Legajo, ");
            consulta.Append("@MailGeneral, ");
            consulta.Append("@MailBBS, ");
            consulta.Append("@Foto)");

            return consulta.ToString();
        }

        /*
         * Recibe como parametro el campo de un dataReader que trae una imagen desde la base de datos,
         * trabaja la informacion recibida y la convierte en una imagen
         */
        private static Image obtenerFotoDeBaseDeDatos(Object campo){

            byte[] img = (byte[])campo;
            MemoryStream memoryStream = new MemoryStream(img);
            Image imagen = Image.FromStream(memoryStream);

            return imagen;
        }
    }
}