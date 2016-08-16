using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlanillaAsistencia;
using Entidades;
//using System.Windows.Forms;
using System.Drawing;

using System.Windows.Forms;

using PlanillaAsistencia.Pantallas.VistaGlobalAsistencias;
using PlanillaAsistencia.Pantallas.EditorAsistencias;
using PlanillaAsistencia.Pantallas.ABMCEncargados;

namespace PlanillaAsistencia.Principal
{
    public class ControladorPrincipal
    {
        private PantallaPrincipal pantalla;

        public ControladorPrincipal(PantallaPrincipal pantalla)
        {
            this.pantalla = pantalla;
            pantalla.Controlador = this;

            pantalla.mostrarPantallaModificacion();
            pantalla.mostrarPantallaConsulta();
            pantalla.mostrarPantallaEncargados();
        }

        public ConsultaAsistencias crearConsultaAsistencias()
        {
            ConsultaAsistencias vista = new ConsultaAsistencias();
            ControladorConsultaAsistencias controlador = new ControladorConsultaAsistencias(vista);

            return vista;
        }

        public EditorAsistencias crearModificacionAsistencias()
        {
            EditorAsistencias vista = new EditorAsistencias();
            ModeloEditorAsistencias modelo = new ModeloEditorAsistencias();
            ControladorEditorAsistencias controlador = new ControladorEditorAsistencias(vista, modelo);

            return vista;
        }

        public ABMCEncargados crearABMCencargados()
        {
            ABMCEncargados vista = new ABMCEncargados();
            ControladorABMCEncargados controlador = new ControladorABMCEncargados(vista);

            return vista;
        }
    }
}
