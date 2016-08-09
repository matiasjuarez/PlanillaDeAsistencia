using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetriCam;

namespace PlanillaAsistencia.ABMCEncargados
{
    public partial class ABMCEncargados : UserControl
    {
        private WebCam webCam;

        private ControladorABMCEncargados controlador;
        public ControladorABMCEncargados Controlador
        {
            set { controlador = value; }
        }

        public ABMCEncargados()
        {
            InitializeComponent();
            webCam = new WebCam();
        }

        public PictureBox obtenerContenedorPhoto()
        {
            return this.pbFoto;
        }

        public void cambiarTextoBotonCapturaCamara(string texto)
        {
            btnTomarFoto.Text = texto;
        }

        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK){
                System.IO.StreamReader streamReader = new System.IO.StreamReader(fileDialog.FileName);

                Image image = Image.FromStream(streamReader.BaseStream);

                pbFoto.Image = image;
            }
            
        }

        private void btnTomarFoto_Click(object sender, EventArgs e)
        {
            if (!webCam.IsConnected())
            {
                webCam.Connect();
                btnTomarFoto.Text = "Capturar";
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                backgroundWorker1.CancelAsync();

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                webCam.Update();
                this.pbFoto.Image = webCam.GetBitmap();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webCam.Disconnect();
            btnTomarFoto.Text = "Filmar";
        }
    }
}
