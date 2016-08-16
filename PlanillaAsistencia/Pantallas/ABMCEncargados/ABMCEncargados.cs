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

namespace PlanillaAsistencia.Pantallas.ABMCEncargados
{
    public partial class ABMCEncargados : ResizableControl
    {
        private ControladorABMCEncargados controlador;
        public ControladorABMCEncargados Controlador
        {
            set { controlador = value; }
        }

        public ABMCEncargados()
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
            BindingList<Encargado> bindingEncargados = new BindingList<Encargado>(encargados);
            listEncargados.DataSource = bindingEncargados;
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
            dtpNacimiento.ResetText();
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
            dtpNacimiento.Enabled = habilitar;
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
            habilitarCampos(false);
            limpiarCampos();

            habilitarBotones(false, false, false, true, false, false, false, false);
        }

        public void ponerEnEstadoNuevoEncargado()
        {
            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(false, true, true, false, false, false, true, true);
        }

        public void ponerEnEstadoModificarEncargado()
        {
            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(false, true, true, false, false, false, true, true);

            Encargado encargado = (Encargado)this.listEncargados.SelectedItem;

            txtNombre.Text = encargado.Nombre;
            txtApellido.Text = encargado.Apellido;
            txtDocumento.Text = encargado.Dni;
            txtTelefono.Text = encargado.Telefono;
            txtMailBBS.Text = encargado.MailBBS;
            txtMailPersonal.Text = encargado.MailGeneral;
            txtLegajo.Text = encargado.Legajo;
            dtpNacimiento.Value = encargado.FechaNacimiento;

            controlador.opcionModificarEncargado(encargado);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarNombreEncargado(obtenerTextoDeTextBox(sender));
        }

        private string obtenerTextoDeTextBox(object sender)
        {
            TextBox textbox = (TextBox)sender;
            return textbox.Text;
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarApellidoEncargado(obtenerTextoDeTextBox(sender));
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarDocumentoEncargado(obtenerTextoDeTextBox(sender));
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarTelefonoEncargado(obtenerTextoDeTextBox(sender));
        }

        private void txtMailPersonal_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarMailPersonalEncargado(obtenerTextoDeTextBox(sender));
        }

        private void txtMailBBS_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarMailBBSencargado(obtenerTextoDeTextBox(sender));
        }

        private void txtLegajo_TextChanged(object sender, EventArgs e)
        {
            controlador.tomarLegajoEncargado(obtenerTextoDeTextBox(sender));
        }

        private void dtpNacimiento_CloseUp(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            controlador.tomarFechaNacimientoEncargado(dtp.Value);
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
    }
}
