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

        public static void darDeBaja(Personal personal)
        {
            GestorConexion gestor = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            String consulta = "UPDATE personal SET estado = 'B' WHERE id=@id";

            MySqlCommand command = new MySqlCommand(consulta, gestor.getConexionAbierta());
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
                gestor.cerrarConexion();
            }
        }

        public static List<Personal> obtenerTodoElPersonal()
        {
            List<Personal> encargados = new List<Personal>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            string consulta = obtenerSelectBasico();
            consulta += " WHERE estado = 'A'";

            MySqlCommand comando = new MySqlCommand(consulta, gestorConexion.getConexionAbierta());

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

        public static bool insertarPersonal(Personal personal)
        {
            if (personal == null) return false;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();

            MySqlCommand comandoPersonal = new MySqlCommand();
            comandoPersonal.CommandText = obtenerInsert(personal);
            comandoPersonal.Connection = gestorConexion.getConexionAbierta();

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
                gestorConexion.cerrarConexion();
            }
        }

        public static bool modificarPersonal(Personal personal)
        {
            if (personal == null) return false;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consulta = new StringBuilder(obtenerUpdateBasico());
            consulta.Append(" WHERE id = @Id");

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta.ToString();
            comando.Connection = gestorConexion.getConexionAbierta();

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
                gestorConexion.cerrarConexion();
            }
        }

        private static Personal armarEncargado(MySqlDataReader reader)
        {
            Configuracion.Config config = Configuracion.Config.getInstance();

            Personal personal = new Personal();

            personal.Id = reader.GetInt32("id");
            personal.Nombre = ValidadorValoresNull.getString(reader,"nombre", "");
            personal.Apellido = ValidadorValoresNull.getString(reader, "apellido", "");
            personal.Dni = ValidadorValoresNull.getString(reader,"DNI", "");
            personal.FechaNacimiento = ValidadorValoresNull.getDateTime(reader, "fechaNacimiento");
            personal.Legajo = ValidadorValoresNull.getString(reader,"legajo", "");
            personal.MailGeneral = ValidadorValoresNull.getString(reader,"mailGeneral", "");
            personal.MailBBS = ValidadorValoresNull.getString(reader,"mailBBS","");

            byte[] fotoData = ValidadorValoresNull.getBinaryData(reader, "foto");
            personal.Foto = Imagenes.obtenerImagenDesdeArregloDeBytes(fotoData);

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
            consulta.Append("SELECT id, p.nombre, apellido, telefono, DNI, ");
            consulta.Append("fechaNacimiento, legajo, mailGeneral, mailBBS, foto, u.nombre, u.rol ");
            consulta.Append("FROM personal as p ");
            consulta.Append("LEFT JOIN usuario as u on u.nombre = p.usuario");

            return consulta.ToString();
        }

        private static string obtenerInsert(Personal personal)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("INSERT INTO personal(nombre, apellido, telefono, DNI, ");
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
    }
}