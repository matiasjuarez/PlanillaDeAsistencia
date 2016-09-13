using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanillaAsistencia.Principal;

namespace PuntoDeEntrada
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
            PuntoDeEntrada puntoDeEntrada = new PuntoDeEntrada();

            controladorPuntoDeEntrada.PuntoDeEntrada = puntoDeEntrada;
            puntoDeEntrada.ControladorPuntoDeEntrada = controladorPuntoDeEntrada;
            controladorPuntoDeEntrada.configurarVistaSegunRolDeUsuario(null);

            Application.Run(puntoDeEntrada);
        }
    }
}
