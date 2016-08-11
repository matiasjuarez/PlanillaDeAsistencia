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
        private CamaraContenedorImagen destino;

        public CamaraWeb()
        {
            webcam = new WebCam();
            inicializarWorker();
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
                            destino.Imagen = webcam.GetBitmap();
                        }
                        catch (Exception e)
                        {
                            GestorExcepciones.mostrarMensajeDeError("Hubo un problema con la camara. Vuelva a iniciar la grabacion");
                            detenerCaptura();
                        }
                    }
                }
            };

            bkgWorker.RunWorkerCompleted += (o, i) =>
            {
                webcam.Disconnect();
                this.destino = null;
            };
        }

        public void iniciarCaptura(ref CamaraContenedorImagen destino)
        {
            this.destino = destino;

            if (!webcam.IsConnected())
            {
                try
                {
                    webcam.Connect();
                }
                catch (Exception e)
                {
                    GestorExcepciones.mostrarMensajeDeError("Compruebe que tiene una camara instalada");
                }
            }

            bkgWorker.RunWorkerAsync();
        }

        public void detenerCaptura()
        {
            bkgWorker.CancelAsync();
        }
    }
}