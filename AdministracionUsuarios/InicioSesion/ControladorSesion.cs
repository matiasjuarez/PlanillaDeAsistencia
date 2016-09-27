using AccesoDatos.DAO;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdministracionPersonal.Password;

namespace AdministracionPersonal.InicioSesion
{
    public class ControladorSesion
    {
        private List<IObservadorCambioEstadoSesion> observadoresSesion = new List<IObservadorCambioEstadoSesion>();

        private VentanaSesion ventanaSesion;
        public VentanaSesion VentanaSesion
        {
            set { ventanaSesion = value; }
        }

        public void agregarObservadorSesion(IObservadorCambioEstadoSesion observador)
        {
            observadoresSesion.Add(observador);
        }

        public bool quitarObservadorSesion(IObservadorCambioEstadoSesion observador)
        {
            return observadoresSesion.Remove(observador);
        }

        public void botonSesionPresionado()
        {
            if (Sesion.obtenerSesionActual() == null || Sesion.obtenerSesionActual().Usuario == null)
            {
                string usuario = ventanaSesion.obtenerUsuario();
                string password = ventanaSesion.obtenerPassword();

                if (iniciarSesion(usuario, password))
                {
                    ventanaSesion.ponerEnEstadoSesionIniciada();
                }
            }
            else
            {
                Sesion.cerrarSesion();
                ventanaSesion.ponerEnEstadoSesionNoIniciada();
            }
        }

        public DateTime obtenerHoraInicioSesion()
        {
            return Sesion.obtenerSesionActual().HoraInicioSesion;
        }

        private bool iniciarSesion(string usuario, string password)
        {
            if(usuario.ToLower() != "admin")
            {
                password = PasswordEncriptacion.encriptarPassword(password);
            }

            if (!DAOUsuario.comprobarUsuarioPassword(usuario, password))
            {
                MessageBox.Show("Por favor, compruebe el nombre de usuario y la contraseña");
                return false;
            }
            else
            {
                Sesion.iniciarSesion(DAOUsuario.buscarUsuario(usuario));
                return true;
            }
        }
    }
}