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
using AdministracionPersonal.Administracion;
using AdministracionPersonal.InicioSesion;
using EstadisticasRapla;
using System.Windows.Forms.Integration;
using System.Windows.Interop;

namespace PuntoDeEntrada
{
    public partial class VentanaPuntoDeEntrada : Form
    {
        private PlanillaAsistencias planillaAsistencias;
        private PantallaAdministracionPersonal pantallaAdministracionUsuarios;

        private ControladorPuntoEntrada controladorPuntoDeEntrada;
        public ControladorPuntoEntrada ControladorPuntoDeEntrada
        {
            set { controladorPuntoDeEntrada = value; }
        }

        public VentanaPuntoDeEntrada()
        {
            InitializeComponent();
        }

        public void mostrarMenuAccesoPlanillaAsistencia(bool mostrar)
        {
            pLANILLAASISTENCIAToolStripMenuItem.Visible = mostrar;
        }

        public void mostrarMenuAccesoObjetosPerdidos(bool mostrar)
        {
            oBJETOSPERDIDOSToolStripMenuItem.Visible = mostrar;
        }

        public void mostrarMenuAccesoAdministracionUsuarios(bool mostrar)
        {
            aDMINISTRARToolStripMenuItem.Visible = mostrar;
        }

        public void mostrarMenuAccesoCambioPassword(bool mostrar)
        {
            cAMBIOPASSToolStripMenuItem.Visible = mostrar;
        }

        public void mostrarMenuAccesoEstadisticasRapla(bool mostrar)
        {
            eSTADISTICASRAPLAToolStripMenuItem.Visible = mostrar;
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
                pantallaAdministracionUsuarios = new PantallaAdministracionPersonal();
            }

            mostrarUserControl(pantallaAdministracionUsuarios);
        }

        private void sESIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VentanaSesion ventanaSesion = controladorPuntoDeEntrada.obtenerVentanaSesion();
            mostrarUserControl(ventanaSesion);
        }

        private void cAMBIOPASSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambioPassword cambioPassword = controladorPuntoDeEntrada.obtenerVentanaCambioPassword();

            if (cambioPassword != null) mostrarUserControl(cambioPassword);
        }

        private void eSTADISTICASRAPLAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Form form = new Form();
            WindowInteropHelper wih = new WindowInteropHelper(new MainWindow());
            wih.Owner = form.Handle;
            form.ShowDialog();*/
        }
    }
}
