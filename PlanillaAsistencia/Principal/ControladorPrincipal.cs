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

namespace PlanillaAsistencia.Principal
{
    public class ControladorPrincipal
    {
        private PlanillaAsistencias pantalla;

        public ControladorPrincipal(PlanillaAsistencias pantalla)
        {
            this.pantalla = pantalla;
            pantalla.Controlador = this;
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
    }
}
