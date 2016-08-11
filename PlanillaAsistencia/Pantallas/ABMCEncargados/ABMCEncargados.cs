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

namespace PlanillaAsistencia.Pantallas.ABMCEncargados
{
    public partial class ABMCEncargados : UserControl
    {
        private Escalador escalador;

        private CamaraWeb camara;
        private bool camaraFilmando = false;

        private ControladorABMCEncargados controlador;
        public ControladorABMCEncargados Controlador
        {
            set { controlador = value; }
        }

        public ABMCEncargados()
        {
            InitializeComponent();
            this.escalador = new Escalador(this);

            camara = new CamaraWeb();
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
            if (!camaraFilmando)
            {
                camaraFilmando = true;

                camara.iniciarCaptura(ref pbFoto);
                btnTomarFoto.Text = "Capturar";
            }
            else
            {
                camaraFilmando = false;

                camara.detenerCaptura();
                btnTomarFoto.Text = "Filmar";
            }
        }

        private void ABMCEncargados_SizeChanged(object sender, EventArgs e)
        {
            this.escalador.resize();
            this.Update();
        }

    }
}
