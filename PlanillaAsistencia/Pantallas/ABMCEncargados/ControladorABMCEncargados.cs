using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Utilidades;
using Entidades;

namespace PlanillaAsistencia.Pantallas.ABMCEncargados
{
    public class ControladorABMCEncargados : IObservadorContenedorImagenCamara
    {
        private ABMCEncargados vista;
        private Image imagenInicial;
        private Image imagenSeleccionada;
        private CamaraWeb camara;
        private CamaraContenedorImagen contenedorImagen;

        private Encargado encargado;

        public ControladorABMCEncargados(ABMCEncargados vista)
        {
            this.vista = vista;
            vista.Controlador = this;

            camara = new CamaraWeb();

            imagenInicial = new Bitmap(PlanillaAsistencia.Properties.Resources.mystery);

            vista.ponerEnEstadoInicial();
        }

        public void seleccionarImagen()
        {
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

        public void iniciarFilmacion()
        {
            this.contenedorImagen = new CamaraContenedorImagen();
            contenedorImagen.agregarObservador(this);

            camara.iniciarCaptura(ref contenedorImagen);
        }

        public void detenerFilmacion()
        {
            this.contenedorImagen.quitarObservador(this);
            this.imagenSeleccionada = contenedorImagen.Imagen;
            camara.detenerCaptura();

            this.contenedorImagen = null;
        }

        void IObservadorContenedorImagenCamara.observarCambioImagenContenedor(Image imagen)
        {
            this.vista.tomarImagenEncargado(imagen);
        }

        public void opcionNuevoEncargado()
        {
            vista.ponerEnEstadoNuevoEncargado();
            this.encargado = new Encargado();
        }

        public void opcionModificarEncargado(Encargado encargado)
        {
            vista.ponerEnEstadoModificarEncargado();
            this.encargado = encargado;
        }

        public void opcionCancelar()
        {
            camara.detenerCaptura();
            vista.ponerEnEstadoInicial();
            vista.tomarImagenEncargado(this.imagenInicial);
        }

        public void tomarNombreEncargado(string nombre)
        {
            encargado.Nombre = nombre;
        }

        public void tomarApellidoEncargado(string apellido)
        {
            encargado.Apellido = apellido;
        }

        public void tomarDocumentoEncargado(string documento)
        {
            encargado.Dni = documento;
        }

        public void tomarFechaNacimientoEncargado(DateTime nacimiento)
        {
            encargado.FechaNacimiento = nacimiento;
        }

        public void tomarTelefonoEncargado(string telefono)
        {
            encargado.Telefono = telefono;
        }

        public void tomarMailBBSencargado(string mail)
        {
            encargado.MailBBS = mail;
        }

        public void tomarMailPersonalEncargado(string mail)
        {
            encargado.MailGeneral = mail;
        }

        public void tomarLegajoEncargado(string legajo)
        {
            encargado.Legajo = legajo;
        }
    }
}
