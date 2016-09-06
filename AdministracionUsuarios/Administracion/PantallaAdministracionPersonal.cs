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
using AdministracionUsuarios.Administracion;

namespace AdministracionUsuarios
{
    public partial class PantallaAdministracionUsuarios : ResizableControl
    {
        private int estadoActual;
        private const int ESTADO_INICIAL = 0;
        private const int ESTADO_MODIFICACION = 1;
        private const int ESTADO_ALTA = 2;

        private bool escucharEventos = true;

        private ControladorAdministracionPersonal controlador;
        public ControladorAdministracionPersonal Controlador
        {
            set { controlador = value; }
        }

        public PantallaAdministracionUsuarios()
        {
            InitializeComponent();
            inicializarEscalador();

            this.controlador = new ControladorAdministracionPersonal(this);
        }

        public void inicializar()
        {
            List<Personal> personal = controlador.obtenerPersonal();
            cargarListaDePersonal(personal);

            ponerEnEstadoInicial();

            tomarImagenPersonal(controlador.obtenerImagenInicial());
        }

        public void cargarListaDePersonal(List<Personal> encargados)
        {
            escucharEventos = false;

            BindingList<Personal> bindingEncargados = new BindingList<Personal>(encargados);
            listEncargados.DataSource = bindingEncargados;
            listEncargados.DisplayMember = "NombreCompleto";

            escucharEventos = true;
        }

        public void tomarImagenPersonal(Image image)
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

            estadoActual = ESTADO_INICIAL;
            panelUsuario.Enabled = false;

            escucharEventos = true;
        }

        public void ponerEnEstadoNuevoPersonal()
        {
            escucharEventos = false;

            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(false, true, true, false, false, false, true, true);

            estadoActual = ESTADO_ALTA;

            panelUsuario.Enabled = true;

            escucharEventos = true;
        }

        public void ponerEnEstadoModificarPersonal(Personal personal)
        {
            escucharEventos = false;

            habilitarCampos(true);
            limpiarCampos();

            habilitarBotones(true, true, true, false, false, false, true, true);

            mostrarDatosDePersonal(personal);

            estadoActual = ESTADO_MODIFICACION;
            panelUsuario.Enabled = true;

            escucharEventos = true;
        }

        private void mostrarDatosDePersonal(Personal personal)
        {
            escucharEventos = false;

            txtNombre.Text = personal.Nombre;
            txtApellido.Text = personal.Apellido;
            txtDocumento.Text = personal.Dni;
            txtTelefono.Text = personal.Telefono;
            txtMailBBS.Text = personal.MailBBS;
            txtMailPersonal.Text = personal.MailGeneral;
            txtLegajo.Text = personal.Legajo;

            string nacimiento = "";
            try { nacimiento = personal.FechaNacimiento.ToString("dd/MM/yyyy"); }
            catch { nacimiento = DateTime.Now.ToString("dd/MM/yyyy"); }
            mkFechaNacimiento.Text = nacimiento;

            pbFoto.Image = personal.Foto;

            if (personal.Usuario == null || personal.Usuario.Nombre == null || personal.Usuario.Nombre == string.Empty)
            {
                lblNombreUsuario.Text = "No especificado";
                lblRolUsuario.Text = "No especificado";
            }
            else
            {
                lblNombreUsuario.Text = personal.Usuario.Nombre;
                lblRolUsuario.Text = personal.Usuario.Rol.Nombre;
            }

            escucharEventos = true;
        }

        public void mostrarDatosUsuario(string nombre, string rol)
        {
            lblNombreUsuario.Text = nombre;
            lblRolUsuario.Text = rol;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarNombre(obtenerTextoDeTextBox(sender));
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
                controlador.tomarApellido(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarDocumento(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarTelefono(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtMailPersonal_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarMailPersonal(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtMailBBS_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarMailBBS(obtenerTextoDeTextBox(sender));
            }
        }

        private void txtLegajo_TextChanged(object sender, EventArgs e)
        {
            if (escucharEventos)
            {
                controlador.tomarLegajo(obtenerTextoDeTextBox(sender));
            }
        }

        private void btnModificarEncargado_Click(object sender, EventArgs e)
        {
            Personal personalSeleccionado = (Personal)this.listEncargados.SelectedValue;

            if (personalSeleccionado != null) controlador.opcionModificarPersonal(personalSeleccionado);
        }

        private void btnNuevoEncargado_Click(object sender, EventArgs e)
        {
            controlador.opcionNuevoPersonal();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            controlador.opcionCancelar();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Nombre = lblNombreUsuario.Text;
            controlador.opcionGuardar();
        }

        public Image obtenerImagenSeleccionada()
        {
            return pbFoto.Image;
        }


        private void mkFechaNacimiento_TextChanged(object sender, EventArgs e)
        {
            if (!escucharEventos) return;

            MaskedTextBox mk = (MaskedTextBox)sender;
            controlador.tomarFechaNacimiento(mk.Text);
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            controlador.mostrarVentanaEditarUsuario();
        }

        private void listEncargados_Click(object sender, EventArgs e)
        {
            if (!escucharEventos) return;

            ListBox list = (ListBox)sender;

            Personal personal = (Personal)list.SelectedValue;

            mostrarDatosDePersonal(personal);

            btnModificarEncargado.Enabled = true;
            btnBajaEncargado.Enabled = true;
        }

        private void btnBajaEncargado_Click(object sender, EventArgs e)
        {
            Personal personalSeleccionado = (Personal)this.listEncargados.SelectedValue;

            if (personalSeleccionado != null) controlador.opcionBajaPersonal(personalSeleccionado);
        }

        private void btnEliminarFoto_Click(object sender, EventArgs e)
        {
            controlador.eliminarFotoPersonal();
        }
    }
}
