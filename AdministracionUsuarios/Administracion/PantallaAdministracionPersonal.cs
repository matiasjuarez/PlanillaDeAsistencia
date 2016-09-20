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
using System.Diagnostics;
using AccesoDatos.DAO;

namespace AdministracionPersonal.Administracion
{
    public partial class PantallaAdministracionPersonal : ResizableControl
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

        public PantallaAdministracionPersonal()
        {
            InitializeComponent();
            inicializarEscalador();
            cargarRoles();
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
            dtpFechaNacimiento.ResetText();

            pbFoto.Image = controlador.obtenerImagenInicial();

            txtNombreUsuario.ResetText();
            lblHabilitado.ResetText();
            cmbRoles.SelectedIndex = -1;
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
            dtpFechaNacimiento.Enabled = habilitar;
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
            btnBloquear.Enabled = false;
            btnReiniciarContrasena.Enabled = false;
            txtNombreUsuario.ResetText();
            cmbRoles.SelectedIndex = -1;
            lblHabilitado.ResetText();

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
            btnBloquear.Enabled = true;
            btnReiniciarContrasena.Enabled = true;

            escucharEventos = true;
        }

        public void mostrarDatosDePersonal(Personal personal)
        {
            escucharEventos = false;

            txtNombre.Text = personal.Nombre;
            txtApellido.Text = personal.Apellido;
            txtDocumento.Text = personal.Dni;
            txtTelefono.Text = personal.Telefono;
            txtMailBBS.Text = personal.MailBBS;
            txtMailPersonal.Text = personal.MailGeneral;
            txtLegajo.Text = personal.Legajo;

            try
            {
                dtpFechaNacimiento.Value = personal.FechaNacimiento;
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
                dtpFechaNacimiento.Value = dtpFechaNacimiento.MinDate;
            }

            if (personal.Foto != null)
            {
                pbFoto.Image = personal.Foto;
            }
            else
            {
                pbFoto.Image = controlador.obtenerImagenInicial();
            }

            mostrarUsuario(personal.Usuario);

            escucharEventos = true;
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

        private void listEncargados_Click(object sender, EventArgs e)
        {
            if (!escucharEventos) return;

            ListBox list = (ListBox)sender;

            Personal personal = (Personal)list.SelectedValue;
            controlador.seSeleccionoPersonal(personal);

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


        //*************************************************************************
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

        public void habilitarBotonesModificacionPersonal(bool habilitar)
        {
            btnModificarEncargado.Enabled = habilitar;
            btnBajaEncargado.Enabled = habilitar;
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

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            controlador.cambiarBloqueoUsuario();
        }

        private void btnReiniciarContrasena_Click(object sender, EventArgs e)
        {
            controlador.reiniciarPassword();
        }

        public string obtenerNombreUsuario()
        {
            return txtNombreUsuario.Text;
        }

        public RolUsuario obtenerRolUsuario()
        {
            return (RolUsuario)cmbRoles.SelectedItem;
        }
    }
}