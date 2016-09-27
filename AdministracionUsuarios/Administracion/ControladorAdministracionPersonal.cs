using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using Utilidades;
using Entidades;
using AccesoDatos;
using System.Diagnostics;
using AccesoDatos.DAO;
using AdministracionPersonal.Password;

namespace AdministracionPersonal.Administracion
{
    public class ControladorAdministracionPersonal : IObservadorCamara
    {
        private const int ESTADO_INICIAL = 0;
        private const int ESTADO_ALTA = 1;
        private const int ESTADO_MODIFICACION = 2;
        private int estadoActual = -1;

        private PantallaAdministracionPersonal vista;
        private Image imagenInicial;
        private CamaraWeb camara;

        //private List<Personal> personal;
        private Personal personalSeleccionado;
        private String nombreUsuarioOriginal;

        public ControladorAdministracionPersonal(PantallaAdministracionPersonal vista)
        {
            this.vista = vista;
            vista.Controlador = this;

            camara = new CamaraWeb();
            camara.agregarObservador(this);

            vista.inicializar();
        }

        public Image obtenerImagenInicial()
        {
            if (imagenInicial == null)
            {
                imagenInicial = new Bitmap(AdministracionPersonal.Properties.Resources.mystery);
            }

            return imagenInicial;
        }

        public List<Personal> obtenerPersonal()
        {
            return DAOPersonal.obtenerTodoElPersonal();
        }

        public void opcionSeleccionarImagen()
        {
            if (camara.estaGrabando()) detenerFilmacion();

            OpenFileDialog fileDialog = new OpenFileDialog();

            Image imagen = null;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(fileDialog.FileName);

                imagen = Image.FromStream(streamReader.BaseStream);
            }

            if (imagen != null)
            {
                vista.tomarImagenPersonal(imagen);
            }
            else
            {
                vista.tomarImagenPersonal(imagenInicial);
            }
        }

        public void opcionTomarFotoDesdeCamara()
        {
            if (camara.estaGrabando()) detenerFilmacion();
            else iniciarFilmacion();
        }

        private void iniciarFilmacion()
        {
            camara.iniciarCaptura();

            vista.tomarTextoBotonCamara("Capturar");
        }

        private void detenerFilmacion()
        {
            camara.detenerCaptura();

            vista.tomarTextoBotonCamara("Camara");
        }

        public void opcionNuevoPersonal()
        {
            this.estadoActual = ESTADO_ALTA;

            vista.ponerEnEstadoNuevoPersonal();
            this.personalSeleccionado = new Personal();
        }

        public void opcionModificarPersonal(Personal personal)
        {
            this.estadoActual = ESTADO_MODIFICACION;

            vista.ponerEnEstadoModificarPersonal(personal);
            this.personalSeleccionado = personal;
            this.nombreUsuarioOriginal = personal.Usuario.Nombre;
        }

        public void seSeleccionoPersonal(Personal personal)
        {
            this.ponerEnEstadoInicial();
            this.personalSeleccionado = personal;

            vista.ponerEnEstadoInicial();
            vista.mostrarDatosDePersonal(personal);
            vista.habilitarBotonesModificacionPersonal(true);
        }

        public void opcionBajaPersonal(Personal personal)
        {
            var confirmacion = MessageBox.Show("¿Confirma la baja?", "CONFIRMACION", MessageBoxButtons.YesNo);

            if (confirmacion == DialogResult.Yes)
            {
                DAOPersonal.darDeBaja(personal);
                DAOUsuario.bloquearUsuario(personal.Usuario, true);

                vista.ponerEnEstadoInicial();
                vista.cargarListaDePersonal(DAOPersonal.obtenerTodoElPersonal());

                opcionCancelar();
            }
        }

        public void opcionCancelar()
        {
            this.ponerEnEstadoInicial();

            vista.ponerEnEstadoInicial();
            vista.tomarImagenPersonal(this.imagenInicial);
            vista.cargarListaDePersonal(obtenerPersonal());
        }

        private void ponerEnEstadoInicial()
        {
            this.estadoActual = ESTADO_INICIAL;
            this.personalSeleccionado = null;

            camara.detenerCaptura();
        }

        public void opcionGuardar()
        {
            personalSeleccionado.Usuario.Nombre = vista.obtenerNombreUsuario();
            personalSeleccionado.Usuario.Rol = vista.obtenerRolUsuario();

            if (!validarDatos()) return;

            if (this.estadoActual == ESTADO_ALTA)
            {
                personalSeleccionado.Usuario.Password = PasswordEncriptacion.encriptarPassword(personalSeleccionado.Usuario.Nombre.ToLower());
                if (DAOPersonal.insertarPersonal(personalSeleccionado))
                {
                    //this.personal.Add(personalSeleccionado);
                    this.ponerEnEstadoInicial();
                    vista.ponerEnEstadoInicial();
                    vista.tomarImagenPersonal(this.imagenInicial);
                }

            }
            else if (this.estadoActual == ESTADO_MODIFICACION)
            {
                Usuario user = personalSeleccionado.Usuario;

                DAOUsuario.modificar(this.nombreUsuarioOriginal, user.Nombre, user.Rol.Nombre);

                if (DAOPersonal.modificarPersonal(personalSeleccionado))
                {
                    this.ponerEnEstadoInicial();
                    vista.ponerEnEstadoInicial();
                }
            }

            vista.cargarListaDePersonal(this.obtenerPersonal());

            this.personalSeleccionado = null;
        }

        /*public void mostrarVentanaEditarUsuario()
        {
            EdicionUsuario edicionUsuario;
            using (var form = crearVentanaEdicionUsuario(out edicionUsuario))
            {
                form.ShowDialog();

                Usuario usuario = this.personalSeleccionado.Usuario;
                usuario.Nombre = edicionUsuario.NombreUsuario;
                usuario.Rol = edicionUsuario.RolUsuario;

                this.vista.mostrarDatosUsuario(usuario);
            }
        }

        private Form crearVentanaEdicionUsuario(out EdicionUsuario edicionUsuarioOut)
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

            EdicionUsuario edicionUsuario = new EdicionUsuario();
            edicionUsuarioOut = edicionUsuario;

            if (this.estadoActual == ESTADO_MODIFICACION) edicionUsuario.ponerEnEdicion(this.personalSeleccionado.Usuario);
            if (this.estadoActual == ESTADO_ALTA) edicionUsuario.ponerEnAlta(this.personalSeleccionado.Usuario);

            edicionUsuario.Dock = DockStyle.Fill;

            form.Controls.Add(edicionUsuario);

            return form;
        }*/

        private bool validarDatos()
        {
            if (personalSeleccionado == null) return false;

            if (personalSeleccionado.Apellido == null || personalSeleccionado.Apellido == "")
            {
                MessageBox.Show("El apellido no puede estar vacio");
                return false;
            }
            if (personalSeleccionado.Nombre == null || personalSeleccionado.Nombre == "")
            {
                MessageBox.Show("El nombre no puede estar vacio");
                return false;
            }

            if (!validarUsuario(personalSeleccionado.Usuario)) return false;

            Image imagenSeleccionada = vista.obtenerImagenSeleccionada();
            if (imagenSeleccionada != this.imagenInicial)
            {
                personalSeleccionado.Foto = imagenSeleccionada;
            }
            else
            {
                personalSeleccionado.Foto = null;
            }

            return true;
        }

        private bool validarUsuario(Usuario usuario)
        {
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

            if (usuario.Rol == null)
            {
                MessageBox.Show("El usuario debe tener un rol asignado");
                return false;
            }

            if (DAOUsuario.existeUsuario(usuario.Nombre))
            {
                if (estadoActual == ESTADO_MODIFICACION)
                {
                    if (usuario.Nombre != nombreUsuarioOriginal)
                    {
                        MessageBox.Show("Ya hay un usuario con ese nombre");
                        return false;
                    }
                }
                else if(estadoActual == ESTADO_ALTA)
                {
                    MessageBox.Show("Ya hay un usuario con ese nombre");
                    return false;
                }
            }

            return true;
        }

        public void tomarNombre(string nombre)
        {
            personalSeleccionado.Nombre = nombre;
        }

        public void tomarApellido(string apellido)
        {
            personalSeleccionado.Apellido = apellido;
        }

        public void tomarDocumento(string documento)
        {
            personalSeleccionado.Dni = documento;
        }

        public void tomarFechaNacimiento(string nacimiento)
        {
            try
            {
                DateTime fecha = DateTime.Parse(nacimiento);
                personalSeleccionado.FechaNacimiento = fecha;
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
            
        }

        public void tomarTelefono(string telefono)
        {
            personalSeleccionado.Telefono = telefono;
        }

        public void tomarMailBBS(string mail)
        {
            personalSeleccionado.MailBBS = mail;
        }

        public void tomarMailPersonal(string mail)
        {
            personalSeleccionado.MailGeneral = mail;
        }

        public void tomarLegajo(string legajo)
        {
            personalSeleccionado.Legajo = legajo;
        }

        public void tomarUsuario(string usuario)
        {
            if (personalSeleccionado.Usuario == null) personalSeleccionado.Usuario = new Usuario();

            personalSeleccionado.Usuario.Nombre = usuario;
        }

        public void tomarRolUsuario(RolUsuario rol)
        {
            if (personalSeleccionado.Usuario == null) personalSeleccionado.Usuario = new Usuario();

            if (rol != null)
            {
                personalSeleccionado.Usuario.Rol = rol;
            }
        }

        public void observarCapturaImagen(Image imagen)
        {
            this.vista.tomarImagenPersonal(imagen);
        }

        public void eliminarFotoPersonal()
        {
            this.personalSeleccionado.Foto = this.imagenInicial;
            vista.tomarImagenPersonal(this.imagenInicial);
        }

        public void cambiarBloqueoUsuario()
        {
            if (personalSeleccionado.Usuario.Habilitado) DAOUsuario.bloquearUsuario(personalSeleccionado.Usuario, true);
            else DAOUsuario.bloquearUsuario(personalSeleccionado.Usuario, false);

            personalSeleccionado.Usuario = DAOUsuario.buscarUsuario(personalSeleccionado.Usuario.Nombre);

            vista.mostrarUsuario(personalSeleccionado.Usuario);
        }

        public void reiniciarPassword()
        {
            personalSeleccionado.Usuario.Password = PasswordEncriptacion.encriptarPassword("");
            DAOUsuario.reiniciarPassword(personalSeleccionado.Usuario);
            MessageBox.Show("La contraseña del usuario ha sido eliminada");
        }
    }
}