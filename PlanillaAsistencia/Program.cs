using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using PlanillaAsistencia.Sincronizacion;

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

            modelo.setControlador(controlador);
            planilla.setControlador(controlador);

            //modelo.agregarObservador(controlador);

            TimerActualizacion actualizadorModelo = new TimerActualizacion(22, controlador);

            /*ControladorSincronizacionBaseRapla con = new ControladorSincronizacionBaseRapla();
            con.sincronizarAsistencias("2016-01-01", "2016-12-12");*/

            Application.Run(planilla);
        }
    }
}
