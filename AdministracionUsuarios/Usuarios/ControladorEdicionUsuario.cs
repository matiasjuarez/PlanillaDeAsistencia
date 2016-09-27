using AccesoDatos.DAO;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministracionPersonal.Usuarios
{
    public class ControladorEdicionUsuario
    {
        private Usuario usuario;
        private string nombreUsuarioOriginal;
        private RolUsuario rolUsuarioSeleccionado;

        private int estadoActual;
        private const int ESTADO_EDICION = 0;
        private const int ESTADO_ALTA = 1;

        private EdicionUsuario ventanaEdicionUsuario;
        public EdicionUsuario VentanaEdicionUsuario
        {
            get { return ventanaEdicionUsuario; }
            set { ventanaEdicionUsuario = value; }
        }

        public List<RolUsuario> obtenerRolesUsuario()
        {
            List<RolUsuario> roles = DAORoles.obtenerTodosLosRoles();
            return roles;
        }

        public void inicializarEdicionUsuario()
        {
            ventanaEdicionUsuario.cargarRoles(obtenerRolesUsuario());

            usuario = null;
            nombreUsuarioOriginal = string.Empty;
            estadoActual = -1;
        }

        public void editarUsuario(Usuario usuario)
        {
            this.estadoActual = ESTADO_EDICION;

            this.usuario = usuario;
            this.nombreUsuarioOriginal = usuario.Nombre;

            ventanaEdicionUsuario.mostrarUsuario(usuario.Nombre, usuario.Rol);

            if (usuario.Habilitado)
            {
                ventanaEdicionUsuario.setTextoBotonBloquear("BLOQUEAR");
                ventanaEdicionUsuario.setTextoLabelUsuarioHabilitado("SI");
            }
            else
            {
                ventanaEdicionUsuario.setTextoBotonBloquear("DESBLOQUEAR");
                ventanaEdicionUsuario.setTextoLabelUsuarioHabilitado("NO");
            }

            ventanaEdicionUsuario.habilitarBotonBloquearUsuario(true);
            ventanaEdicionUsuario.habilitarBotonReiniciarContrasena(true);
        }

        public void altaUsuario()
        {
            this.estadoActual = ESTADO_ALTA;

            this.usuario = new Usuario();

            ventanaEdicionUsuario.habilitarBotonBloquearUsuario(false);
            ventanaEdicionUsuario.habilitarBotonReiniciarContrasena(false);
            ventanaEdicionUsuario.setTextoLabelUsuarioHabilitado(string.Empty);
        }

        public void reiniciarContrasenaUsuario()
        {
            usuario.Password = "";
            DAOUsuario.reiniciarPassword(usuario);
        }

        public void manejarBloqueoUsuario()
        {
            if (usuario.Habilitado) DAOUsuario.bloquearUsuario(usuario, true);
            else DAOUsuario.bloquearUsuario(usuario, false);

            Usuario usuarioActualizado = DAOUsuario.buscarUsuario(usuario.Nombre);
            usuario.Habilitado = usuarioActualizado.Habilitado;

            if (usuario.Habilitado) { ventanaEdicionUsuario.setTextoBotonBloquear("BLOQUEAR"); }
            else { ventanaEdicionUsuario.setTextoBotonBloquear("DESBLOQUEAR"); }
        }

        private bool validarUsuario()
        {
            if (estadoActual == ESTADO_ALTA)
            {
                if (DAOUsuario.existeUsuario(usuario.Nombre))
                {
                    MessageBox.Show("Ya hay un usuario con ese nombre");
                    return false;
                }
            }
            else if (estadoActual == ESTADO_EDICION)
            {
                if (DAOUsuario.existeUsuario(usuario.Nombre) && usuario.Nombre != nombreUsuarioOriginal)
                {
                    MessageBox.Show("Ya hay un usuario con ese nombre");
                    return false;
                }
            }

            if (usuario.Nombre == null || usuario.Nombre.Length < 6)
            {
                MessageBox.Show("El nombre de usuario debe contener al menos 6 caracteres");
                return false;
            }

            // Vemos que lo que ingreso el usuario sean caracteres alfanumericos
            foreach (char aChar in usuario.Nombre)
            {
                if (!Char.IsLetterOrDigit(aChar))
                {
                    MessageBox.Show("Solo se permite letras y numeros en el nombre de usuario");
                    return false;
                }
            }

            return true;
        }


    }
}