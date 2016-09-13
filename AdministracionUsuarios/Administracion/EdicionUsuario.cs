using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using AccesoDatos.DAO;
using Utilidades;

namespace AdministracionUsuarios.Administracion
{
    public partial class EdicionUsuario : UserControl
    {
        private Usuario usuario;
        private string nombreUsuarioOriginal;

        private int estadoActual;
        private const int ESTADO_EDICION = 0;
        private const int ESTADO_ALTA = 1;

        public EdicionUsuario()
        {
            InitializeComponent();
            cargarRoles();
        }

        public string NombreUsuario
        {
            get { return usuario.Nombre; }
        }

        public RolUsuario RolUsuario
        {
            get { return usuario.Rol; }
        }

        public void ponerEnEdicion(Usuario usuario)
        {
            this.usuario = usuario;
            this.estadoActual = ESTADO_EDICION;
            this.nombreUsuarioOriginal = usuario.Nombre;

            mostrarUsuario(usuario);

            btnBloquear.Enabled = true;
            btnReiniciarContrasena.Enabled = true;
        }

        public void ponerEnAlta(Usuario usuario)
        {
            this.usuario = usuario;
            this.estadoActual = ESTADO_ALTA;

            btnBloquear.Enabled = false;
            btnReiniciarContrasena.Enabled = false;
            lblHabilitado.ResetText();
        }

        public void cargarRoles()
        {
            List<RolUsuario> roles = DAORoles.obtenerTodosLosRoles();

            CargadorCombo.cargar<RolUsuario>(cmbRoles, roles, "Nombre", "Nombre");

            cmbRoles.SelectedIndex = -1;
        }

        public void mostrarUsuario(Usuario usuario)
        {
            if (usuario == null || usuario.Nombre == null || usuario.Nombre == string.Empty)
            {
                txtNombreUsuario.Text = "No especificado";
                cmbRoles.SelectedIndex = -1;
            }
            else
            {
                txtNombreUsuario.Text = usuario.Nombre;

                for (int i = 0; i < cmbRoles.Items.Count; i++)
                {
                    RolUsuario valor = (RolUsuario)cmbRoles.Items[i];
                    if (valor.Nombre == usuario.Rol.Nombre)
                    {
                        cmbRoles.SelectedIndex = i;
                        break;
                    }
                }

                if (usuario.Habilitado)
                {
                    btnBloquear.Text = "BLOQUEAR";
                    lblHabilitado.Text = "SI";
                }
                else
                {
                    lblHabilitado.Text = "NO";
                    btnBloquear.Text = "DESBLOQUEAR";
                }
            }
        }

        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Size = new Size(0, 0);
            form.AutoSize = true;

            // Define the border style of the form to a dialog box.
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            form.MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            form.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            form.StartPosition = FormStartPosition.CenterScreen; 

            AltaRol altaRol = new AltaRol();
            altaRol.Dock = DockStyle.Fill;

            form.Controls.Add(altaRol);

            form.ShowDialog();

            if (altaRol.ResultadoOK)
            {
                cargarRoles();
            }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Parent.Dispose();
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            if (usuario.Habilitado) DAOUsuario.bloquearUsuario(usuario, true);
            else DAOUsuario.bloquearUsuario(usuario, false);

            Usuario usuarioActualizado = DAOUsuario.buscarUsuario(usuario.Nombre);
            usuario.Habilitado = usuarioActualizado.Habilitado;

            mostrarUsuario(usuario);
        }

        private void btnReiniciarContrasena_Click(object sender, EventArgs e)
        {
            usuario.Password = "";
            DAOUsuario.reiniciarPassword(usuario);
        }
    }
}
