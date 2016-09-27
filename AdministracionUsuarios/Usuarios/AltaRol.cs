using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatos.DAO;
using Entidades;

namespace AdministracionPersonal.Usuarios
{
    public partial class AltaRol : UserControl
    {
        private bool resultadoOK = false;
        public bool ResultadoOK
        {
            get { return resultadoOK; }
        }

        public AltaRol()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreRol = txtNombreRol.Text;
            if (nombreRol == null || nombreRol == string.Empty)
            {
                MessageBox.Show("El nombre del rol no puede estar vacio");
                return;
            }

            RolUsuario rol = new RolUsuario(nombreRol);

            if (DAORoles.existeRol(rol))
            {
                MessageBox.Show("Ya hay un rol con ese nombre");
            }
            else
            {
                DAORoles.insertar(new RolUsuario(nombreRol));
                resultadoOK = true;
                this.Parent.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Parent.Dispose();
        }
    }
}