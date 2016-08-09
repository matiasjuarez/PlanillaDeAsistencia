using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Entidades;
using Utilidades;
using Configuracion;

namespace PlanillaAsistencia
{
    public partial class VistaGlobal : UserControl
    {
        private Config configuracion = Config.getInstance();

        private ControladorVistaGlobal controladorVistaGlobal;
        public ControladorVistaGlobal ControladorVistaGlobal
        {
            get { return controladorVistaGlobal; }
            set { controladorVistaGlobal = value; }
        }

        public VistaGlobal()
        {
            InitializeComponent();
        }

        public DateTime obtenerFechaDesde()
        {
            return dtpFiltroFechaDesde.Value.Date;
        }

        public DateTime obtenerFechaHasta()
        {
            return dtpFiltroFechaHasta.Value.Date;
        }

        public bool fechaDesdeEstaChequeada()
        {
            return this.chkUsarFechaDesde.Checked;
        }

        public bool fechaHastaEstaChequeada()
        {
            return this.chkUsarFechaHasta.Checked;
        }

        public bool asignaturaEstaChequeda()
        {
            return this.chkUsarAsignatura.Checked;
        }

        public bool docenteEstaChequeado()
        {
            return this.chkUsarDocente.Checked;
        }

        public void cargarComboAsignaturas(List<Asignatura> asignaturas)
        {
            Asignatura asignaturaNoAsignada = new Asignatura();
            asignaturaNoAsignada.Id = configuracion.IdAsignaturaNoAsignada;
            asignaturaNoAsignada.Nombre = configuracion.AsignaturaNoAsignada;

            asignaturas.Insert(0, asignaturaNoAsignada);

            CargadorCombo.cargar<Asignatura>(cmbFiltroAsignatura, asignaturas, "nombre", "id");
        }

        public void cargarComboDocentes(List<Docente> docentes)
        {
            Docente docenteNoAsignado = new Docente();
            docenteNoAsignado.Id = configuracion.IdDocenteNoAsignado;
            docenteNoAsignado.Nombre = configuracion.DocenteNoAsignado;

            docentes.Insert(0, docenteNoAsignado);

            CargadorCombo.cargar<Docente>(cmbFiltroDocente, docentes, "nombre", "id");
        }

        public Docente obtenerDocenteSeleccionado()
        {
            object docente = cmbFiltroDocente.SelectedItem;
            return (Docente)docente;
        }

        public Asignatura obtenerAsignaturaSeleccionada()
        {
            object asignatura = cmbFiltroAsignatura.SelectedItem;
            return (Asignatura)asignatura;
        }

        public void refrescarGrillas()
        {
            tripleGrillaVistaGlobal.refrescarGrillas();
        }

        public void informarFechaDesdeEsMayorQueFechaHasta()
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarNuevaBusqueda();
        }

        private void dtpFiltroFechaDesde_CloseUp(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void dtpFiltroFechaHasta_CloseUp(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void chkUsarFechaDesde_CheckedChanged(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void chkUsarFechaHasta_CheckedChanged(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void cmbFiltroAsignatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void cmbFiltroDocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void chkUsarAsignatura_CheckedChanged(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        private void chkUsarDocente_CheckedChanged(object sender, EventArgs e)
        {
            controladorVistaGlobal.manejarCambioFiltros();
        }

        public void cargarAsistenciasTurnoManana(List<AsistenciaTabla> asistencias)
        {
            tripleGrillaVistaGlobal.cargarAsistenciasTurnoManana(asistencias);
        }

        public void cargarAsistenciasTurnoTarde(List<AsistenciaTabla> asistencias)
        {
            tripleGrillaVistaGlobal.cargarAsistenciasTurnoTarde(asistencias);
        }

        public void cargarAsistenciasTurnoNoche(List<AsistenciaTabla> asistencias)
        {
            tripleGrillaVistaGlobal.cargarAsistenciasTurnoNoche(asistencias);
        }

       
    }
}
