using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetriCam;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace Utilidades
{
    public class CamaraWeb
    {
        private WebCam webcam;
        private BackgroundWorker bkgWorker;
        private List<IObservadorCamara> observadores = new List<IObservadorCamara>();

        public CamaraWeb()
        {
            webcam = new WebCam();
            inicializarWorker();
        }

        public bool estaGrabando()
        {
            if (webcam == null) return false;

            if (webcam.IsConnected()) return true;
            return false;
        }

        private void inicializarWorker()
        {
            bkgWorker = new BackgroundWorker();

            bkgWorker.WorkerSupportsCancellation = true;

            bkgWorker.DoWork += (o, i) =>
            {
                if (webcam.IsConnected())
                {
                    while (!this.bkgWorker.CancellationPending)
                    {
                        webcam.Update();
                        try
                        {
                            notificarCapturaImagen( webcam.GetBitmap() );
                        }
                        catch (Exception e)
                        {
                            GestorExcepciones.mostrarMensajeDeError("Hubo un problema con la camara. Vuelva a iniciar la grabacion");
                            System.Console.Write(e.StackTrace);
                            detenerCaptura();
                        }
                    }
                }
            };

            bkgWorker.RunWorkerCompleted += (o, i) =>
            {
                webcam.Disconnect();
            };
        }

        public void iniciarCaptura()
        {
            if (!webcam.IsConnected())
            {
                try
                {
                    webcam.Connect();
                }
                catch (Exception e)
                {
                    GestorExcepciones.mostrarMensajeDeError("Compruebe que tiene una camara instalada");
                    System.Console.Write(e.StackTrace);
                }
            }

            bkgWorker.RunWorkerAsync();
        }

        public void detenerCaptura()
        {
            bkgWorker.CancelAsync();
        }

        public void agregarObservador(IObservadorCamara observador)
        {
            if (!this.observadores.Contains(observador))
            {
                this.observadores.Add(observador);
            }
        }

        public bool quitarObservador(IObservadorCamara observador)
        {
            return this.observadores.Remove(observador);
        }

        private void notificarCapturaImagen(Image imagen)
        {
            foreach (IObservadorCamara observador in observadores)
            {
                observador.observarCapturaImagen(imagen);
            }
        }
    }

    public interface IObservadorCamara
    {
        void observarCapturaImagen(Image imagen);
    }
}