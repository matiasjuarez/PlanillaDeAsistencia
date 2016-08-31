using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using PlanillaAsistencia;
using PlanillaAsistencia.Principal;

using Entidades;
using AccesoDatos;

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

            DAOAsistencias.obtenerAsistenciasDeFechas(new List<DateTime>{DateTime.Now});
            Docente docente = DAODocentes.obtenerDocentePorID(892);

            // yyyy-mm-dd
            DateTime inicio = DateTime.Parse("2016-01-01");
            DateTime fin = DateTime.Parse("2016-07-30");
            SincronizacionInterBase.ControladorSincronizacionInterBase.sincronizar(inicio, fin);
            
            Application.Run(pantalla);
        }
    }
}