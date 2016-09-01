using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilidades;
using Entidades;

namespace AdministracionUsuarios
{
    public partial class PantallaAdministracionUsuarios : ResizableControl
    {
        private bool escucharEventos = true;

        private ControladorABMCEncargados controlador;
        public ControladorABMCEncargados Controlador
        {
            set { controlador = value; }
        }

        public PantallaAdministracionUsuarios()
        {
            InitializeComponent();
            inicializarEscalador();
        }

        public void inicializar()
        {
            List<Encargado> encargados = controlador.obtenerEncargados();
            cargarListaEncargados(encargados);

            ponerEnEstadoInicial();

            tomarImagenEncargado(controlador.obtenerImagenInicial());
        }

        public void cargarListaEncargados(List<Encargado> encargados)
        {
            escucharEventos = false;

            BindingList<Encargado> bindingEncargados = new BindingList<Encargado>(encargados);
            listEncargados.DataSource = bindingEncargados;
            listEncargados.DisplayMember = "NombreCompleto";

            escucharEventos = true;
        }

        public void tomarImagenEncargado(Image image)
        {
            pbFoto.Image = image;
        }

        public void tomarTextoBotonCamara(string texto)
        {
            this.btnTomarFoto.Text = texto;
        }

        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            controlador.opcionSeleccionarImagen();            
        }

        private void btnTomarFoto_Click(object sender, EventArgs e)
        {
            controlador.opcionTomarFotoDesdeCamara();
        }

        private void limpiarCampos()
        {
            txtNombre.ResetText();
            txtApellido.ResetText();
            txtDocumento.ResetText();
            txtDocumento.ResetText();
            txtLegajo.ResetText();
            txtTelefono.ResetText();
            txtMailBBS.ResetText();
            txtMailPersonal.ResetText();
            mkFechaNacimiento.ResetText();

            pbFoto.Image = controlador.obtenerImagenInicial();
        }

        private void habilitarCampos(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtDocumento.Enabled = habilitar;
            txtDocumento.Enabled = habilitar;
            txtLegajo.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;
            txtMailBBS.Enabled = habilitar;
            txtMailPersonal.Enabled = habilitar;
            mkFechaNacimiento.Enabled = habilitar;
        }

        private void habilitarBotones(bool btnEliminarFoto, bool btnSeleccionarFoto, bool btnCamara,
            bool btnAgregarEncargado, bool btnModificarEncargado, bool btnBajaEncargado, 
            bool btnGuardar, bool btnCancelar)
        {
            this.btnEliminarFoto.Enabled = btnEliminarFoto;
            this.btnSeleccionarFoto.Enabled = btnSeleccionarFoto;
            this.btnTomarFoto.Enabled = btnCamara;
            this.btnNuevoEncargado.Enabled = btnAgregarEncargado;
            this.btnModificarEncargado.Enabled = btnModificarEncargado;
            this.btnBajaEncargado.Enabled = btnBajaEncargado;
            this.btnGuardarCambios.Enabled = btnGuardar;
            this.btnCancelar.Enabled = btnCancelar;
        }

        public void ponerEnEstadoInicial()
        {
            escucharEventos = false;

            habilitarCampos(false);
            limpiarCampos();

            habilitarBotones(false, false, false, true, false, false, false, false);

            escucharEventos = true;
        }

        public void ponerEnEstadoNuevoEncargado()
        {
            escucharEventos = false;

            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(false, true, true, false, false, false, true, true);

            escucharEventos = true;
        }

        public void ponerEnEstadoModificarEncargado(Encargado encargado)
        {
            escucharEventos = false;

            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(false, true, true, false, false, false, true, true);

            txtNombre.Text = encargado.Nombre;
            txtApellido.Text = encargado.Apellido;
            txtDocumento.Text = encargado.Dni;
            txtTelefono.Text = encargado.Telefono;
            txtMailBBS.Text = encargado.MailBBS;
            txtMailPersonal.Text = encargado.MailGeneral;
            txtLegajo.Text = encargado.Legajo;

            string nacimiento = "";
            try { nacimiento = encargado.FechaNacimiento.ToString("dd/MM/yyyy"); }
            catch { nacimiento = DateTime.Now.ToString("dd/MM/yyyy"); }
            mkFechaNacimiento.Text = nacimiento;

            pbFoto.Image = encargado.Foto;

            escucharEventos = true;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarNombreEncargado(obtenerTextoDeTextBox(sender));
            }
            
        }

        private string obtenerTextoDeTextBox(object sender)
        {
            TextBox textbox = (TextBox)sender;
            return textbox.Text;
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarApellidoEncargado(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarDocumentoEncargado(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarTelefonoEncargado(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtMailPersonal_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarMailPersonalEncargado(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtMailBBS_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarMailBBSencargado(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtLegajo_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarLegajoEncargado(obtenerTextoDeTextBox(sender));
            }
        }

        private void btnModificarEncargado_Click(object sender, EventArgs e)
        {
            Encargado encargadoSeleccionado = (Encargado)this.listEncargados.SelectedValue;

            if (encargadoSeleccionado != null) controlador.opcionModificarEncargado(encargadoSeleccionado);
        }

        private void btnNuevoEncargado_Click(object sender, EventArgs e)
        {
            controlador.opcionNuevoEncargado();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            controlador.opcionCancelar();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            controlador.opcionGuardar();
        }

        public Image obtenerImagenSeleccionada()
        {
            return pbFoto.Image;
        }

        private void listEncargados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!escucharEventos) return;

            ListBox list = (ListBox)sender;

            Encargado encargado = (Encargado)list.SelectedValue;

            controlador.opcionModificarEncargado(encargado);
        }

        private void mkFechaNacimiento_TextChanged(object sender, EventArgs e)
        {
            if (!escucharEventos) return;

            MaskedTextBox mk = (MaskedTextBox)sender;
            controlador.tomarFechaNacimientoEncargado(mk.Text);
        }
    }
}
