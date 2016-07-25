using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;

namespace PlanillaAsistencia
{
    public class ControladorEdicionAsistencia : IObservadorCamposPlanilla
    {
        private planillaAsistencia vista;
        private Modelo modelo;

        private Asistencia asistenciaOriginal;

        private Asistencia asistenciaEnEdicion;
        public Asistencia AsistenciaEnEdicion
        {
            get { return asistenciaEnEdicion; }
            set { asistenciaEnEdicion = value; }
        }

        public ControladorEdicionAsistencia(planillaAsistencia vista, Modelo modelo)
        {
            this.vista = vista;
            this.modelo = modelo;
        }

        public void manejarSeleccionDeAsistenciaDesdeGrilla()
        {
            int idAsistencia = vista.obtenerIdAsistenciaSeleccionada();

            asistenciaEnEdicion = modelo.getAsistencia(idAsistencia).Clone();
            asistenciaOriginal = asistenciaEnEdicion.Clone();

            vista.habilitarManejadoresDeEventos(false);
            vista.cargarDatosDeAsistencia(asistenciaEnEdicion);
            vista.habilitarManejadoresDeEventos(true);

            vista.habilitarCampos(true);
        }

        public bool seModificoAsistencia()
        {
            if (!asistenciaEnEdicion.poseeLosMismosDatosQueEstaAsistencia(asistenciaOriginal))
            {
                return true;
            }
            return false;
        }

        public bool guardarAsistenciasModificadas()
        {
            List<Asistencia> asistencias = modelo.getAsistenciasModificadas();

            bool resultado = DAOAsistencias.updateAsistencias(asistencias);

            if (resultado)
            {
                modelo.combinarAsistenciasModificadasEnDiccionarios();

                DateTime fechaSeleccionada = vista.obtenerFechaSeleccionada();
                List<Asistencia> asistenciasParaFechaSeleccionada = modelo.getAsistenciasParaFecha(fechaSeleccionada, true);
                vista.cargarAsistenciasEnGrillas(asistenciasParaFechaSeleccionada);
            }

            return resultado;
        }

        // ****************************************************************************
        // ******************IMPLEMENTACION IObservadorCamposPlanilla******************
        // ****************************************************************************
        public void observarCambioDocente(Docente docente)
        {
            asistenciaEnEdicion.Docente = docente;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioAsignatura(Asignatura Asignatura)
        {
            asistenciaEnEdicion.Asignatura = Asignatura;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioHoraRealDeSalida(DateTime horaSalida)
        {
            asistenciaEnEdicion.FinClaseReal = horaSalida;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioHoraRealDeEntrada(DateTime horaEntrada)
        {
            asistenciaEnEdicion.ComienzoClaseReal = horaEntrada;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioEstadoAsistencia(EstadoAsistencia estadoAsistencia)
        {
            asistenciaEnEdicion.EstadoAsistencia = estadoAsistencia;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioObservaciones(string observaciones)
        {
            asistenciaEnEdicion.Observaciones = observaciones;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioCantidadAlumnos(int cantidadAlumnos)
        {
            asistenciaEnEdicion.CantidadAlumnos = cantidadAlumnos;
            manejarCambioDatosAsistencia();
        }

        private void manejarCambioDatosAsistencia()
        {
            modelo.agregarAsistenciaModificada(this.asistenciaEnEdicion);
            vista.marcarAsistenciaComoModificada(this.asistenciaEnEdicion);
            vista.habilitarBotonGuardado(true);
        }
        // ****************************************************************************
        // ****************************************************************************
    }
}