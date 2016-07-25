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
            Encargado encargado = null;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder(
                               "SELECT id, nombre, apellido, telefono, DNI, " +
                               "fechaNacimiento, legajo, mailGeneral, mailBBS, foto " +
                               "FROM encargado " +
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
                    encargado = new Encargado();

                    encargado.Id = reader.GetInt32("id");
                    encargado.Nombre = reader.GetString("nombre");
                    encargado.Apellido = reader.GetString("apellido");
                    encargado.Dni = reader.GetString("DNI");
                    encargado.FechaNacimiento = reader.GetDateTime("fechaNacimiento");
                    encargado.Legajo = reader.GetString("legajo");
                    encargado.MailGeneral = reader.GetString("mailGeneral");
                    encargado.MailBBS = reader.GetString("mailBBS");
                    encargado.Foto = obtenerFotoDeBaseDeDatos(reader.GetByte("foto"));
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

            return encargado;
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
