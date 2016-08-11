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
        private PictureBox pictureBox;

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
                            pictureBox.Image = webcam.GetBitmap();
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
                this.pictureBox = null;
            };
        }

        public void iniciarCaptura(ref PictureBox destino)
        {
            this.pictureBox = destino;

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