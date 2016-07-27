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

        // La asistencia sobre la que vamos a hacer cambios
        private AsistenciaDual asistenciaEnEdicion;
        // La asistencia sobre la que vamos a hacer cambios tal cual estaba antes de empezar a modificarla
        public AsistenciaDual AsistenciaEnEdicion
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

            asistenciaEnEdicion = modelo.getAsistencia(idAsistencia);

            vista.habilitarManejadoresDeEventos(false);
            vista.cargarDatosDeAsistencia(asistenciaEnEdicion.Clonada);
            vista.habilitarManejadoresDeEventos(true);

            vista.habilitarCampos(true);
        }

        public bool seModificoAsistencia()
        {
            return asistenciaEnEdicion.esModificada();
        }

        public bool guardarAsistenciasModificadas()
        {
            List<AsistenciaDual> asistenciasD = modelo.getAsistenciasModificadas();
            List<Asistencia> asistencias = new List<Asistencia>();
            foreach (AsistenciaDual asistenciaD in asistenciasD)
            {
                asistencias.Add(asistenciaD.Clonada);
            }

            bool resultado = DAOAsistencias.updateAsistencias(asistencias);

            if (resultado)
            {
                modelo.setearAsistenciasClonadasComoOriginales();

                DateTime fechaSeleccionada = vista.obtenerFechaSeleccionada();
                List<AsistenciaDual> asistenciasParaFechaSeleccionada = modelo.getAsistenciasParaFecha(fechaSeleccionada, true);
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

        public void observarCambioHoraRealDeSalida(TimeSpan horaSalida)
        {
            asistenciaEnEdicion.FinClaseReal = horaSalida;
            manejarCambioDatosAsistencia();
        }

        public void observarCambioHoraRealDeEntrada(TimeSpan horaEntrada)
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
            //modelo.agregarAsistenciaModificada(this.asistenciaEnEdicion);
            //vista.marcarAsistenciaComoModificada(this.asistenciaEnEdicion);
            vista.habilitarBotonGuardado(true);
        }
        // ****************************************************************************
        // ****************************************************************************
    }
}