using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccesoDatos.DAO
{
    public class DAORoles
    {
        public static List<RolUsuario> obtenerTodosLosRoles()
        {
            List<RolUsuario> roles = new List<RolUsuario>();

            string consulta = "Select nombre from rol_usuario";

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, conexion);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RolUsuario rol = new RolUsuario();
                    rol.Nombre = reader.GetString("nombre");

                    roles.Add(rol);
                }

                return roles;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return null;
            }
            finally
            {
                GestorConexion.cerrarConexion(conexion);
            }
        }

        public static bool existeRol(RolUsuario rol)
        {
            string consulta = "Select nombre from rol_usuario where nombre=@nombre";

            MySqlConnection connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", rol.Nombre);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                return reader.HasRows;
            }
            catch (MySqlException ex)
            {
                Debug.Write(ex.StackTrace);
                throw ex;
            }
            finally
            {
                GestorConexion.cerrarConexion(connection);
            }
        }

        public static bool insertar(RolUsuario rol)
        {
            string consulta = "INSERT INTO rol_usuario(nombre) VALUES(@nombre)";

            MySqlConnection connection = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", rol.Nombre);

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                Debug.Write(ex.StackTrace);
                throw ex;
            }
            finally
            {
                GestorConexion.cerrarConexion(connection);
            }
        }
    }
}