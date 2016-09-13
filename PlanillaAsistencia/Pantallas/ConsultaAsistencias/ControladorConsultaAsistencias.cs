using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using Entidades;
using AccesoDatos;

namespace PlanillaAsistencia.Pantallas.VistaGlobalAsistencias
{
    public class ControladorConsultaAsistencias
    {
        private ConsultaAsistencias vistaGlobal;
        private List<AsistenciaTabla> asistencias;

        private RangoHorario rangoHorarioManana = new RangoHorario("00:00:00", "12:00:00");
        private RangoHorario rangoHorarioTarde = new RangoHorario("12:00:00", "18:00:00");
        private RangoHorario rangoHorarioNoche = new RangoHorario("18:00:00", "23:59:59");


        public ControladorConsultaAsistencias(ConsultaAsistencias vistaGlobal)
        {
            this.asistencias = new List<AsistenciaTabla>();

            this.vistaGlobal = vistaGlobal;

            inicializarVista();
        }

        private void inicializarVista()
        {
            this.vistaGlobal.ControladorVistaGlobal = this;

            List<Asignatura> asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
            List<Docente> docentes = DAODocentes.obtenerTodosLosDocentes();

            this.vistaGlobal.cargarComboAsignaturas(asignaturas);
            this.vistaGlobal.cargarComboDocentes(docentes);
        }

        public void manejarNuevaBusqueda()
        {
            if (!validarFiltros()) return;

            asistencias = obtenerAsistencias();

            // Esto no hace falta. Es un arreglo a un bug rapido que se debe revisar
            foreach (AsistenciaTabla asistencia in asistencias)
            {
                asistencia.obtenerAsistencia().guardarEstado();
            }

            filtrarAsistencias(asistencias);

            mostrarAsistencias(asistencias);

            vistaGlobal.refrescarGrillas();
        }

        private void mostrarAsistencias(List<AsistenciaTabla> asistencias)
        {
            List<AsistenciaTabla> asistenciasManana = new List<AsistenciaTabla>();
            List<AsistenciaTabla> asistenciasTarde = new List<AsistenciaTabla>();
            List<AsistenciaTabla> asistenciasNoche = new List<AsistenciaTabla>();

            if (asistencias != null)
            {
                foreach (AsistenciaTabla asistenciaTabla in asistencias)
                {
                    Asistencia asistencia = asistenciaTabla.obtenerAsistencia();

                    TimeSpan horaClase = asistencia.HoraEntradaEsperada;

                    if (rangoHorarioManana.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasManana.Add(asistenciaTabla);
                    }
                    else if (rangoHorarioTarde.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasTarde.Add(asistenciaTabla);
                    }
                    else
                    {
                        asistenciasNoche.Add(asistenciaTabla);
                    }
                }

                OrdenadorAsistencias sorter = new OrdenadorAsistencias(ordenadorAsistenciaPorHoraEntradaEsperada);

                asistenciasManana.Sort((a1, a2) => sorter(a1, a2));
                asistenciasTarde.Sort((a1, a2) => sorter(a1, a2));
                asistenciasNoche.Sort((a1, a2) => sorter(a1, a2));
            }

            vistaGlobal.cargarAsistenciasTurnoManana(asistenciasManana);
            vistaGlobal.cargarAsistenciasTurnoTarde(asistenciasTarde);
            vistaGlobal.cargarAsistenciasTurnoNoche(asistenciasNoche);

            setearColorDeAsistenciaTabla(asistenciasManana);
            setearColorDeAsistenciaTabla(asistenciasTarde);
            setearColorDeAsistenciaTabla(asistenciasNoche);
        }

        private void setearColorDeAsistenciaTabla(List<AsistenciaTabla> asistencias)
        {
            Color colorImpar = Color.SkyBlue;
            Color colorPar = Color.Gray;
            bool colorImparSeleccionado = true;

            DateTime fechaActual = new DateTime(1, 1, 1);
            bool fechaInicializada = false;

            foreach (AsistenciaTabla asistencia in asistencias)
            {
                Asistencia asistenciaCorrespondiente = asistencia.obtenerAsistencia();

                if (!fechaInicializada)
                {
                    fechaActual = asistenciaCorrespondiente.Fecha;
                    fechaInicializada = true;
                }


                if (!fechaActual.Equals(asistenciaCorrespondiente.Fecha))
                {
                    fechaActual = asistenciaCorrespondiente.Fecha;
                    colorImparSeleccionado = !colorImparSeleccionado;
                }

                if (colorImparSeleccionado) asistencia.ColorBackground = colorImpar;
                else asistencia.ColorBackground = colorPar;
            }
        }

        private delegate int OrdenadorAsistencias(AsistenciaTabla a1, AsistenciaTabla a2);
        private int ordenadorAsistenciaPorHoraEntradaEsperada(AsistenciaTabla a1, AsistenciaTabla a2)
        {
            return a1.obtenerAsistencia().obtenerEntradaEsperada().CompareTo(
                a2.obtenerAsistencia().obtenerEntradaEsperada());
        }

        public void manejarCambioFiltros()
        {
            filtrarAsistencias(this.asistencias);
            vistaGlobal.refrescarGrillas();
        }

        private void filtrarAsistencias(List<AsistenciaTabla> asistencias)
        {
            Asignatura asignaturaSeleccionada = vistaGlobal.obtenerAsignaturaSeleccionada();
            Docente docenteSeleccionado = vistaGlobal.obtenerDocenteSeleccionado();
            DateTime fechaDesde = vistaGlobal.obtenerFechaDesde();
            DateTime fechaHasta = vistaGlobal.obtenerFechaHasta();

            foreach (AsistenciaTabla asistencia in asistencias)
            {

                if(validarAsistenciaContraDocente(asistencia, docenteSeleccionado) &&
                    validarAsistenciaContraAsignatura(asistencia, asignaturaSeleccionada) && 
                    validarAsistenciaContraFechaDesde(asistencia, fechaDesde) && 
                    validarAsistenciaContraFechaHasta(asistencia, fechaHasta))
                {
                    asistencia.Visible = true;
                }
                else
                {
                    asistencia.Visible = false;
                }
            }
        }

        private bool validarAsistenciaContraDocente(AsistenciaTabla asistencia, Docente docente)
        {
            if (!vistaGlobal.docenteEstaChequeado()) return true;

            if (asistencia.NombreProfesor == docente.Nombre)
            {
                return true;
            }

            return false;
        }

        private bool validarAsistenciaContraAsignatura(AsistenciaTabla asistencia, Asignatura asignatura)
        {
            if (!vistaGlobal.asignaturaEstaChequeda()) return true;

            if (asistencia.NombreAsignatura == asignatura.Nombre)
            {
                return true;
            }

            return false;
        }

        private bool validarAsistenciaContraFechaDesde(AsistenciaTabla asistenciaTabla, DateTime fechaDesde)
        {
            if (!vistaGlobal.fechaDesdeEstaChequeada()) return true;

            Asistencia asistencia = asistenciaTabla.obtenerAsistencia();
            if (asistencia.Fecha >= fechaDesde) return true;
            return false;
        }

        private bool validarAsistenciaContraFechaHasta(AsistenciaTabla asistenciaTabla, DateTime fechaHasta)
        {
            if (!vistaGlobal.fechaHastaEstaChequeada()) return true;

            Asistencia asistencia = asistenciaTabla.obtenerAsistencia();
            if (asistencia.Fecha <= fechaHasta) return true;
            return false;
        }

        private List<AsistenciaTabla> obtenerAsistencias()
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            DateTime fechaDesde = vistaGlobal.obtenerFechaDesde();
            DateTime fechaHasta = vistaGlobal.obtenerFechaHasta();

            asistencias = DAOAsistencias.obtenerAsistenciasEntreFechas(fechaDesde, fechaHasta);

            List<AsistenciaTabla> asistenciasTabla = new List<AsistenciaTabla>();
            foreach (Asistencia asistencia in asistencias)
            {
                asistenciasTabla.Add(new AsistenciaTabla(asistencia));
            }

            return asistenciasTabla;
        }

        private bool validarFiltros()
        {
            DateTime fechaDesde = vistaGlobal.obtenerFechaDesde();
            DateTime fechaHasta = vistaGlobal.obtenerFechaHasta();

            if (fechaDesde > fechaHasta)
            {
                vistaGlobal.informarFechaDesdeEsMayorQueFechaHasta();
                return false;
            }

            return true;
        }
    }
}