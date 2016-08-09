using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using PlanillaAsistencia;
using PlanillaAsistencia.ABMCEncargados;

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

            planillaAsistencia planilla = new planillaAsistencia();
            Modelo modelo = new Modelo();
            Controlador controlador = new Controlador(planilla, modelo);

            modelo.Controlador = controlador;
            planilla.Controlador = controlador;

            crearTabEncargados(planilla);

            /*DateTime inicio = DateTime.Parse("2016-01-01");
            DateTime fin = DateTime.Parse("2016-12-12");
            SincronizacionInterBase.ControladorSincronizacionInterBase.sincronizar(inicio, fin);
            */

            Application.Run(planilla);
        }

        private static void crearTabEncargados(planillaAsistencia planilla){
            ABMCEncargados.ABMCEncargados vista = new ABMCEncargados.ABMCEncargados();
            ControladorABMCEncargados controlador = new ControladorABMCEncargados(vista);

            planilla.agregarPestanaABMCEncargados(vista);
        }
    }
}
