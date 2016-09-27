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

namespace AdministracionPersonal.Password
{
    public partial class CambioPassword : ResizableControl
    {
        private Usuario usuario;

        public CambioPassword()
        {
            InitializeComponent();
            inicializarEscalador();
        }

        public void mostrarUsuario(Usuario usuario)
        {
            this.usuario = usuario;
            lblUsuario.Text = usuario.Nombre;
            txtPasswordNuevo.ResetText();
            txtPasswordAnterior.ResetText();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string passAnterior = txtPasswordAnterior.Text;
            string passNuevo = txtPasswordNuevo.Text;

            string usuario = lblUsuario.Text;

            if(DAOUsuario.comprobarUsuarioPassword(usuario, PasswordEncriptacion.encriptarPassword(passAnterior)))
            {
                if (comprobarPasswordValido(passNuevo))
                {
                    DAOUsuario.cambiarPassword(usuario, PasswordEncriptacion.encriptarPassword(passNuevo));
                    mostrarUsuario(this.usuario);
                    MessageBox.Show("El password fue cambiado");
                }
                else
                {
                    MessageBox.Show("El password no cumple con las condiciones requeridas");
                }
            }
            else
            {
                MessageBox.Show("El password anterior ingresado no es correcto");
            }
        }

        private bool comprobarPasswordValido(string password)
        {
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            mostrarUsuario(usuario);
        }
    }
}
