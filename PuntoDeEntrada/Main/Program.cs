using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanillaAsistencia.Principal;

namespace PuntoDeEntrada.Main
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ControladorPuntoEntrada controladorPuntoDeEntrada = new ControladorPuntoEntrada();
            VentanaPuntoDeEntrada puntoDeEntrada = new VentanaPuntoDeEntrada();

            controladorPuntoDeEntrada.PuntoDeEntrada = puntoDeEntrada;
            puntoDeEntrada.ControladorPuntoDeEntrada = controladorPuntoDeEntrada;
            controladorPuntoDeEntrada.configurarVistaSegunRolDeUsuario(null);

            Application.Run(puntoDeEntrada);
        }
    }
}
