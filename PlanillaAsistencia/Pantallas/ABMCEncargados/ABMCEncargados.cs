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
        private CamaraWeb camara;
        private bool camaraFilmando = false;

        private int ESTADO_INICIAL = 0;
        private int ESTADO_ALTA = 1;
        private int ESTADO_MODIFICACION = 2;
        private int estadoActual = -1;

        private ControladorABMCEncargados controlador;
        public ControladorABMCEncargados Controlador
        {
            set { controlador = value; }
        }

        public ABMCEncargados()
        {
            InitializeComponent();
            camara = new CamaraWeb();

            inicializarEscalador();
        }

        public void tomarImagenEncargado(Image image)
        {
            pbFoto.Image = image;
        }

        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            detenerFilmacion();

            controlador.seleccionarImagen();            
        }

        private void iniciarFilmacion()
        {
            camaraFilmando = true;
            btnTomarFoto.Text = "Capturar";
            controlador.iniciarFilmacion();
        }

        private void detenerFilmacion()
        {
            camaraFilmando = false;
            btnTomarFoto.Text = "Camara";
            controlador.detenerFilmacion();
        }

        private void btnTomarFoto_Click(object sender, EventArgs e)
        {
            if (!camaraFilmando)
            {
                iniciarFilmacion();
            }
            else
            {
                detenerFilmacion();
            }
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
            estadoActual = ESTADO_INICIAL;

            habilitarCampos(false);
            limpiarCampos();

            habilitarBotones(false, false, false, true, false, false, false, false);

        }

        public void ponerEnEstadoNuevoEncargado()
        {
            estadoActual = ESTADO_ALTA;

            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(false, true, true, false, false, false, true, true);
        }

        public void ponerEnEstadoModificarEncargado()
        {
            estadoActual = ESTADO_MODIFICACION;

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
            estadoActual = ESTADO_MODIFICACION;

            limpiarCampos();
            habilitarCampos(true);

            habilitarBotones(true, true, true, false, false, false, true, true);
        }

        private void btnNuevoEncargado_Click(object sender, EventArgs e)
        {
            controlador.opcionNuevoEncargado();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            controlador.opcionCancelar();
        }
    }
}
