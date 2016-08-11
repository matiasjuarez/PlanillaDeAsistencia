using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PlanillaAsistencia.Pantallas.VistaGlobalAsistencias;
using PlanillaAsistencia.Pantallas.ModificacionAsistencias;
using PlanillaAsistencia.Pantallas.ABMCEncargados;

using Utilidades;

namespace PlanillaAsistencia.Principal
{
    public partial class PantallaPrincipal : Form
    {
        private TabPage tabModificacionAsistencias;
        private TabPage tabConsultaAsistencia;
        private TabPage tabABMCencargados;

        private ControladorPrincipal controlador;
        public ControladorPrincipal Controlador
        {
            set { controlador = value; }
        }

        public PantallaPrincipal()
        {
            InitializeComponent();
        }

        public void mostrarPantallaConsulta()
        {
            if (tabConsultaAsistencia == null)
            {
                ConsultaAsistencias vista = controlador.crearConsultaAsistencias();
                tabConsultaAsistencia = crearTab(vista, "Consulta");
            }
        }

        public void mostrarPantallaModificacion()
        {
            if (tabModificacionAsistencias == null)
            {
                ModificacionAsistencias vista = controlador.crearModificacionAsistencias();
                tabModificacionAsistencias = crearTab(vista, "Modificacion");
            }
        }

        public void mostrarPantallaEncargados()
        {
            if (tabABMCencargados == null)
            {
                ABMCEncargados vista = controlador.crearABMCencargados();
                tabABMCencargados = crearTab(vista, "Encargados");
            }
        }

        private TabPage crearTab(Control control, string nombreTab)
        {
            TabPage tab = new TabPage(nombreTab);
            tab.Controls.Add(control);
            tabs.TabPages.Add(tab);
            control.Dock = DockStyle.Fill;
            return tab;
        }

        private void PantallaPrincipal_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
