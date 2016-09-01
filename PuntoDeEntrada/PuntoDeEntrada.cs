using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilidades;
using PlanillaAsistencia.Principal;
using AdministracionUsuarios;
using AdministracionUsuarios.Sesion;

namespace PuntoDeEntrada
{
    public partial class PuntoDeEntrada : Form
    {
        private PlanillaAsistencias planillaAsistencias;
        private PantallaAdministracionUsuarios pantallaAdministracionUsuarios;

        public PuntoDeEntrada()
        {
            InitializeComponent();
        }

        private void pLANILLAASISTENCIAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (planillaAsistencias == null)
            {
                planillaAsistencias = new PlanillaAsistencias();
            }

            mostrarUserControl(planillaAsistencias);
        }

        private void mostrarUserControl(UserControl control)
        {
            this.panelPrincipal.Controls.Clear();
            this.panelPrincipal.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }

        private void aDMINISTRARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pantallaAdministracionUsuarios == null)
            {
                pantallaAdministracionUsuarios = new PantallaAdministracionUsuarios();
            }

            mostrarUserControl(pantallaAdministracionUsuarios);
        }

        private void sESIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VentanaSesion ventanaSesion = new VentanaSesion();
            mostrarUserControl(ventanaSesion);
        }
    }
}
