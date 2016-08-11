using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using PlanillaAsistencia;
using PlanillaAsistencia.Principal;

namespace PlanillaAsistencia
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

            PantallaPrincipal pantalla = new PantallaPrincipal();
            ControladorPrincipal controlador = new ControladorPrincipal(pantalla);

            // yyyy-mm-dd
            DateTime inicio = DateTime.Parse("2016-01-01");
            DateTime fin = DateTime.Parse("2016-12-30");
            SincronizacionInterBase.ControladorSincronizacionInterBase.sincronizar(inicio, fin);
            

            Application.Run(pantalla);
        }
    }
}
