using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanillaAsistencia.Principal;
using PlanillaAsistencia.Pantallas;
using PlanillaAsistencia.Pantallas.VistaGlobalAsistencias;
using PlanillaAsistencia.Pantallas.EditorAsistencias;

namespace PlanillaAsistencia.Principal
{
    public partial class PlanillaAsistencias : UserControl
    {
        private ControladorPrincipal controlador;
        public ControladorPrincipal Controlador
        {
            set { controlador = value; }
        }

        public PlanillaAsistencias()
        {
            InitializeComponent();

            controlador = new ControladorPrincipal(this);

            EditorAsistencias modificacion = controlador.crearModificacionAsistencias();
            crearTab(modificacion, "Modificacion");

            ConsultaAsistencias consultaAsistencia = controlador.crearConsultaAsistencias();
            crearTab(consultaAsistencia, "Consulta");
        }

        private TabPage crearTab(Control control, string nombreTab)
        {
            TabPage tab = new TabPage(nombreTab);
            tab.Controls.Add(control);
            tabs.TabPages.Add(tab);
            control.Dock = DockStyle.Fill;
            return tab;
        }
    }
}