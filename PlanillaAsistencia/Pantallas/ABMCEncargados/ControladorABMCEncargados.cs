using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace PlanillaAsistencia.Pantallas.ABMCEncargados
{
    public class ControladorABMCEncargados
    {
        private ABMCEncargados vista;

        public ControladorABMCEncargados(ABMCEncargados vista)
        {
            this.vista = vista;
            vista.Controlador = this;
        }

    }
}
