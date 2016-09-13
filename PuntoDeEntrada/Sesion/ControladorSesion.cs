using AccesoDatos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace PuntoDeEntrada.Sesion
{
    public class ControladorSesion
    {
        private List<IObservadorCambioEstadoSesion> observadoresSesion = new List<IObservadorCambioEstadoSesion>();

        private Usuario usuarioLogeado;
        public Usuario UsuarioLogeado
        {
            get { return usuarioLogeado; }
        }

        private DateTime horaInicioSesion;
        public DateTime HoraInicioSesion
        {
            get { return horaInicioSesion; }
        }

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
            if (usuarioLogeado == null)
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
                usuarioLogeado = null;
                ventanaSesion.ponerEnEstadoSesionNoIniciada();
                notificarObservadoresCierreSesion();
            }
        }

        private bool iniciarSesion(string usuario, string password)
        {
            if (!DAOUsuario.comprobarUsuarioPassword(usuario, password))
            {
                MessageBox.Show("Por favor, compruebe el nombre de usuario y la contraseña");
                return false;
            }
            else
            {
                usuarioLogeado = DAOUsuario.buscarUsuario(usuario);
                horaInicioSesion = DateTime.Now;
                notificarObservadoresInicioSesion();
                return true;
            }
        }

        private void notificarObservadoresInicioSesion()
        {
            foreach (IObservadorCambioEstadoSesion observador in observadoresSesion)
            {
                observador.observarInicioSesion(usuarioLogeado);
            }
        }

        private void notificarObservadoresCierreSesion()
        {
            foreach (IObservadorCambioEstadoSesion observador in observadoresSesion)
            {
                observador.observarCierreSesion(usuarioLogeado);
            }
        }
    }

    public interface IObservadorCambioEstadoSesion
    {
        void observarCierreSesion(Usuario usuario);
        void observarInicioSesion(Usuario usuario);
    }
}
