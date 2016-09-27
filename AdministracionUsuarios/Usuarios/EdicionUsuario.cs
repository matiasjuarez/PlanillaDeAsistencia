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

namespace AdministracionPersonal.Usuarios
{
    public partial class EdicionUsuario : UserControl
    {
        private ControladorEdicionUsuario controladorEdicionUsuario;

        public EdicionUsuario(ControladorEdicionUsuario controlador)
        {
            InitializeComponent();

            this.controladorEdicionUsuario = controlador;

            this.controladorEdicionUsuario.inicializarEdicionUsuario();
        }

        public void habilitarBotonReiniciarContrasena(bool habilitar)
        {
            btnReiniciarContrasena.Enabled = habilitar;
        }

        public void habilitarBotonBloquearUsuario(bool habilitar)
        {
            btnBloquear.Enabled = habilitar;
        }

        public void setTextoBotonBloquear(string texto)
        {
            btnBloquear.Text = texto;
        }

        public void setTextoLabelUsuarioHabilitado(string texto)
        {
            lblHabilitado.Text = texto;
        }

        public void cargarRoles(List<RolUsuario> roles)
        {
            CargadorCombo.cargar<RolUsuario>(cmbRoles, roles, "Nombre", "Nombre");

            cmbRoles.SelectedIndex = -1;
        }

        public void mostrarUsuario(string nombreUsuario, RolUsuario rolUsuario)
        {
            if (nombreUsuario == null || nombreUsuario == string.Empty)
            {
                txtNombreUsuario.Text = "No especificado";
            }
            else
            {
                txtNombreUsuario.Text = nombreUsuario;
            }

            cmbRoles.SelectedIndex = -1;
            if (rolUsuario != null)
            {
                for (int i = 0; i < cmbRoles.Items.Count; i++)
                {
                    RolUsuario valor = (RolUsuario)cmbRoles.Items[i];
                    if (valor.Nombre == rolUsuario.Nombre)
                    {
                        cmbRoles.SelectedIndex = i;
                        break;
                    }
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
                cargarRoles(controladorEdicionUsuario.obtenerRolesUsuario());
            }
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            controladorEdicionUsuario.manejarBloqueoUsuario();
        }

        private void btnReiniciarContrasena_Click(object sender, EventArgs e)
        {
            controladorEdicionUsuario.reiniciarContrasenaUsuario();
        }
    }
}