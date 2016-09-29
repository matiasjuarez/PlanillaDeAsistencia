using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Entidades;

using Utilidades;

using System.Drawing;

using System.IO;
using AccesoDatos.DAO;
using System.Diagnostics;

namespace AccesoDatos
{
    public static class DAOPersonal
    {
        // Devuelve null si no se encuentra un curso con ese id
        public static Personal obtenerPersonalPorID(int id)
        {
            StringBuilder consulta = new StringBuilder(obtenerSelectBasico());
            consulta.Append(" WHERE id = @id");

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta.ToString();
            comando.Connection = conexion;

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
                GestorConexion.cerrarConexion(conexion);
            }

            return null;
        }

        public static void darDeBaja(Personal personal)
        {
            String consulta = "UPDATE personal SET estado = 'B' WHERE id=@id";

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand command = new MySqlCommand(consulta, conexion);
            command.Parameters.AddWithValue("@id", personal.Id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                GestorExcepciones.mostrarExcepcion(ex);
                throw ex;
            }
            finally
            {
                GestorConexion.cerrarConexion(conexion);
            }
        }

        public static List<Personal> obtenerTodoElPersonal()
        {
            List<Personal> encargados = new List<Personal>();

            string consulta = obtenerSelectBasico();
            consulta += " WHERE estado = 'A'";

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand comando = new MySqlCommand(consulta, conexion);

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
                GestorConexion.cerrarConexion(conexion);
            }
        }

        public static bool insertarPersonal(Personal personal)
        {
            if (personal == null) return false;

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlCommand comandoPersonal = new MySqlCommand();
            comandoPersonal.CommandText = obtenerInsert(personal);
            comandoPersonal.Connection = conexion;

            comandoPersonal.Parameters.AddWithValue("@Nombre", personal.Nombre);
            comandoPersonal.Parameters.AddWithValue("@Apellido", personal.Apellido);
            comandoPersonal.Parameters.AddWithValue("@Telefono", personal.Telefono);
            comandoPersonal.Parameters.AddWithValue("@Dni", personal.Dni);
            comandoPersonal.Parameters.AddWithValue("@FechaNacimiento", personal.FechaNacimiento);
            comandoPersonal.Parameters.AddWithValue("@Legajo", personal.Legajo);
            comandoPersonal.Parameters.AddWithValue("@MailGeneral", personal.MailGeneral);
            comandoPersonal.Parameters.AddWithValue("@MailBBS", personal.MailBBS);

            byte[] arregloFoto = null;
            if (personal.Foto != null)
            {
                arregloFoto = Imagenes.convertirImagenEnArregloDeBytes(personal.Foto);
            }

            comandoPersonal.Parameters.AddWithValue("@Usuario", personal.Usuario.Nombre);
            comandoPersonal.Parameters.AddWithValue("@Foto", arregloFoto);

            try
            {
                comandoPersonal.ExecuteNonQuery();
                DAOUsuario.insertar(personal.Usuario);

                return true;
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return false;
            }
            finally
            {
                GestorConexion.cerrarConexion(conexion);
            }
        }

        public static bool modificarPersonal(Personal personal)
        {
            if (personal == null) return false;
            
            StringBuilder consulta = new StringBuilder(obtenerUpdateBasico());
            consulta.Append(" WHERE id = @Id");

            MySqlConnection conexion = GestorConexion.getInstance().getConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta.ToString();
            comando.Connection = conexion;

            comando.Parameters.AddWithValue("@Nombre", personal.Nombre);
            comando.Parameters.AddWithValue("@Apellido", personal.Apellido);
            comando.Parameters.AddWithValue("@Telefono", personal.Telefono);
            comando.Parameters.AddWithValue("@Dni", personal.Dni);
            comando.Parameters.AddWithValue("@FechaNacimiento", personal.FechaNacimiento);
            comando.Parameters.AddWithValue("@Legajo", personal.Legajo);
            comando.Parameters.AddWithValue("@MailGeneral", personal.MailGeneral);
            comando.Parameters.AddWithValue("@MailBBS", personal.MailBBS);
            comando.Parameters.AddWithValue("@Id", personal.Id);
            comando.Parameters.AddWithValue("@Usuario", personal.Usuario.Nombre);

            byte[] arregloFoto = null;
            if (personal.Foto != null)
            {
                arregloFoto = Imagenes.convertirImagenEnArregloDeBytes(personal.Foto);
            }

            comando.Parameters.AddWithValue("@Foto", arregloFoto);

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
                GestorConexion.cerrarConexion(conexion);
            }
        }

        private static Personal armarEncargado(MySqlDataReader reader)
        {
            Configuracion.Config config = Configuracion.Config.getInstance();

            Personal personal = new Personal();

            personal.Id = reader.GetInt32("id");
            personal.Nombre = ValidadorValoresNull.getString(reader,"nombrePersonal", "");
            personal.Apellido = ValidadorValoresNull.getString(reader, "apellido", "");
            personal.Telefono = ValidadorValoresNull.getString(reader, "telefono", "");
            personal.Dni = ValidadorValoresNull.getString(reader,"DNI", "");
            personal.FechaNacimiento = ValidadorValoresNull.getDateTime(reader, "fechaNacimiento");
            personal.Legajo = ValidadorValoresNull.getString(reader,"legajo", "");
            personal.MailGeneral = ValidadorValoresNull.getString(reader,"mailGeneral", "");
            personal.MailBBS = ValidadorValoresNull.getString(reader,"mailBBS","");

            byte[] fotoData = ValidadorValoresNull.getBinaryData(reader, "foto");
            personal.Foto = Imagenes.obtenerImagenDesdeArregloDeBytes(fotoData);

            string nombreUsuario = ValidadorValoresNull.getString(reader, "nombreUsuario", null);
            Usuario usuario = DAOUsuario.buscarUsuario(nombreUsuario);
            personal.Usuario = usuario;

            return personal;
        }

        private static string obtenerUpdateBasico()
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("UPDATE personal SET ");
            consulta.Append("nombre = @Nombre, ");
            consulta.Append("apellido = @Apellido, ");
            consulta.Append("telefono = @Telefono, ");
            consulta.Append("DNI = @Dni, ");
            consulta.Append("fechaNacimiento = @FechaNacimiento, ");
            consulta.Append("legajo = @Legajo, ");
            consulta.Append("mailGeneral = @MailGeneral, ");
            consulta.Append("mailBBS = @MailBBS, ");
            consulta.Append("foto = @Foto, ");
            consulta.Append("usuario = @usuario");

            return consulta.ToString();
        }

        private static string obtenerSelectBasico()
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("SELECT id, p.nombre as nombrePersonal, apellido, telefono, DNI, ");
            consulta.Append("fechaNacimiento, legajo, mailGeneral, mailBBS, foto, u.nombre as nombreUsuario, u.rol ");
            consulta.Append("FROM personal as p ");
            consulta.Append("LEFT JOIN usuario as u on u.nombre = p.usuario");

            return consulta.ToString();
        }

        private static string obtenerInsert(Personal personal)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("INSERT INTO personal(nombre, apellido, telefono, DNI, ");
            consulta.Append("fechaNacimiento, legajo, mailGeneral, mailBBS, foto, usuario) ");
            consulta.Append("VALUES(");
            consulta.Append("@Nombre, ");
            consulta.Append("@Apellido, ");
            consulta.Append("@Telefono, ");
            consulta.Append("@Dni, ");
            consulta.Append("@FechaNacimiento, ");
            consulta.Append("@Legajo, ");
            consulta.Append("@MailGeneral, ");
            consulta.Append("@MailBBS, ");
            consulta.Append("@Foto, ");
            consulta.Append("@Usuario)");

            return consulta.ToString();
        }
    }
}