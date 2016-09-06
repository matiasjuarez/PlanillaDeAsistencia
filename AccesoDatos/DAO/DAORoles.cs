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

            GestorConexion gestor = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            string consulta = "Select nombre from rol_usuario";

            MySqlCommand command = new MySqlCommand(consulta, gestor.getConexionAbierta());

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
                gestor.cerrarConexion();
            }
        }

        public static bool existeRol(RolUsuario rol)
        {
            GestorConexion gestor = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            string consulta = "Select nombre from rol_usuario where nombre=@nombre";

            MySqlCommand command = new MySqlCommand(consulta, gestor.getConexionAbierta());
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
                gestor.cerrarConexion();
            }
        }

        public static bool insertar(RolUsuario rol)
        {
            GestorConexion gestor = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            string consulta = "INSERT INTO rol_usuario(nombre) VALUES(@nombre)";

            MySqlCommand command = new MySqlCommand(consulta, gestor.getConexionAbierta());
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
                gestor.cerrarConexion();
            }
        }
    }
}