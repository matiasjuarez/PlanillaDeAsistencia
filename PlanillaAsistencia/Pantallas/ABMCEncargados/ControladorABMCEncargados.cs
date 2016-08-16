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

namespace PlanillaAsistencia.Pantallas.ABMCEncargados
{
    public class ControladorABMCEncargados : IObservadorCamara
    {
        private const int ESTADO_INICIAL = 0;
        private const int ESTADO_ALTA = 1;
        private const int ESTADO_MODIFICACION = 2;
        private int estadoActual = -1;

        private ABMCEncargados vista;
        private Image imagenInicial;
        private Image imagenSeleccionada;
        private CamaraWeb camara;

        private List<Encargado> encargados;
        private Encargado encargadoSeleccionado;

        public ControladorABMCEncargados(ABMCEncargados vista)
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
                imagenInicial = new Bitmap(PlanillaAsistencia.Properties.Resources.mystery);
            }

            return imagenInicial;
        }

        public List<Encargado> obtenerEncargados()
        {
            if (encargados == null)
            {
                encargados = DAOEncargados.obtenerTodosLosEncargados();
            }

            return encargados;
        }

        public void opcionSeleccionarImagen()
        {
            if (camara.estaGrabando()) detenerFilmacion();

            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(fileDialog.FileName);

                imagenSeleccionada = Image.FromStream(streamReader.BaseStream);
            }

            if (imagenSeleccionada != null)
            {
                vista.tomarImagenEncargado(imagenSeleccionada);
            }
            else
            {
                vista.tomarImagenEncargado(imagenInicial);
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

        public void opcionNuevoEncargado()
        {
            this.estadoActual = ESTADO_ALTA;

            vista.ponerEnEstadoNuevoEncargado();
            this.encargadoSeleccionado = new Encargado();
        }

        public void opcionModificarEncargado(Encargado encargado)
        {
            this.estadoActual = ESTADO_MODIFICACION;

            vista.ponerEnEstadoModificarEncargado();
            this.encargadoSeleccionado = encargado;
        }

        public void opcionCancelar()
        {
            this.ponerEnEstadoInicial();

            vista.ponerEnEstadoInicial();
            vista.tomarImagenEncargado(this.imagenInicial);
        }

        private void ponerEnEstadoInicial()
        {
            this.estadoActual = ESTADO_INICIAL;
            this.encargadoSeleccionado = null;

            camara.detenerCaptura();
        }

        public void opcionGuardar()
        {
            if (this.estadoActual == ESTADO_ALTA)
            {
                if (validarDatosEncargado())
                {
                    if (DAOEncargados.insertarEncargado(encargadoSeleccionado))
                    {
                        this.encargados.Add(encargadoSeleccionado);
                        this.ponerEnEstadoInicial();
                        vista.ponerEnEstadoInicial();
                        vista.tomarImagenEncargado(this.imagenInicial);
                        vista.cargarListaEncargados(this.obtenerEncargados());
                    }
                }
            }
            else if (this.estadoActual == ESTADO_MODIFICACION)
            {

            }
        }

        private bool validarDatosEncargado()
        {
            if (encargadoSeleccionado == null) return false;

            if (encargadoSeleccionado.Apellido == "") return false;
            if (encargadoSeleccionado.Nombre == "") return false;

            if (encargadoSeleccionado.FechaNacimiento == null)
            {
                encargadoSeleccionado.FechaNacimiento = Configuracion.Config.getInstance().ValorParaFechaNula;
            }

            return true;
        }

        public void tomarNombreEncargado(string nombre)
        {
            encargadoSeleccionado.Nombre = nombre;
        }

        public void tomarApellidoEncargado(string apellido)
        {
            encargadoSeleccionado.Apellido = apellido;
        }

        public void tomarDocumentoEncargado(string documento)
        {
            encargadoSeleccionado.Dni = documento;
        }

        public void tomarFechaNacimientoEncargado(DateTime nacimiento)
        {
            encargadoSeleccionado.FechaNacimiento = nacimiento;
        }

        public void tomarTelefonoEncargado(string telefono)
        {
            encargadoSeleccionado.Telefono = telefono;
        }

        public void tomarMailBBSencargado(string mail)
        {
            encargadoSeleccionado.MailBBS = mail;
        }

        public void tomarMailPersonalEncargado(string mail)
        {
            encargadoSeleccionado.MailGeneral = mail;
        }

        public void tomarLegajoEncargado(string legajo)
        {
            encargadoSeleccionado.Legajo = legajo;
        }

        public void observarCapturaImagen(Image imagen)
        {
            this.vista.tomarImagenEncargado(imagen);
        }
    }
}