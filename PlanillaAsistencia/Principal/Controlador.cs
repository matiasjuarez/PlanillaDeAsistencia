using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlanillaAsistencia;
using Entidades;
//using System.Windows.Forms;
using System.Drawing;

using System.Windows.Forms;

using AccesoDatos;
using ContenedoresDeDatos;
using Utilidades;

namespace PlanillaAsistencia
{
    public class Controlador : IObservadorCamposPlanilla, Temporizador.ITemporizable
    {
        private planillaAsistencia vista;
        private Modelo modelo;

        // La asistencia sobre la que el usuario hizo click en la grilla
        private Asistencia asistenciaSeleccionada;
        // Las asistencias que se han ido cargando en las grillas luego de apretado el boton de guardado
        private ContenedorAsistencias asistenciasTrabajadas;

        private RangoHorario rangoHorarioManana = new RangoHorario("00:00:00", "12:00:00");
        private RangoHorario rangoHorarioTarde = new RangoHorario("12:00:00", "18:00:00");
        private RangoHorario rangoHorarioNoche = new RangoHorario("18:00:00", "23:59:59");

        private Temporizador temporizador;

        public Controlador(planillaAsistencia planilla, Modelo modelo)
        {
            this.vista = planilla;
            this.modelo = modelo;

            this.asistenciasTrabajadas = new ContenedorAsistencias();

            vista.agregarObservadorCamposEditables(this);

            vista.Controlador = this;
            modelo.Controlador = this;

            modelo.inicializar();
            vista.inicializar();

            temporizador = new Temporizador(5000);
            temporizador.agregarObjetoTemporizable(this);
            temporizador.habilitar(true);
        }

        public void manejarGuardarCambios()
        {
            guardarAsistenciasModificadas();
            vista.ponerEnEstado(planillaAsistencia.ESTADO_NOCAMBIO_NOSELECCION);
            this.vista.mostrarMensaje("Los datos fueron guardados", Color.Green, 3000);
        }

        private void guardarAsistenciasModificadas()
        {
            List<Asistencia> asistenciasModificadas = new List<Asistencia>();

            foreach (Asistencia asistencia in asistenciasTrabajadas.obtenerDatos())
            {
                if (asistencia.estaModificada())
                {
                    asistenciasModificadas.Add(asistencia);
                }
            }

            DAOAsistencias.updateAsistencias(asistenciasModificadas);

            foreach (Asistencia asistencia in asistenciasModificadas)
            {
                asistencia.guardarEstado();
            }
        }

        public List<Asignatura> obtenerAsignaturas()
        {
            return modelo.obtenerAsignaturas();
        }

        public List<Docente> obtenerDocentes()
        {
            return modelo.obtenerDocentes();
        }

        public List<EstadoAsistencia> obtenerEstadosDeAsistencia()
        {
            return modelo.obtenerEstadosAsistencia();
        }

        // Se encarga de cargar los datos de la asistencia seleccionada por el usuario tras haber hecho click en
        // alguna fila de alguna grilla. 
        public void manejarSeleccionDeAsistenciaDesdeGrilla(AsistenciaTabla asistencia)
        {
            this.asistenciaSeleccionada = asistencia.Asistencia;
            vista.ponerEnEstado(planillaAsistencia.ESTADO_NOCAMBIO_SISELECCION);
            vista.mostrarDatosDeAsistencia(this.asistenciaSeleccionada);
        }

        // Cuando el usuario cambia la fecha seleccionada debe llamar a esta funcion.
        public void manejarCambioFechaSeleccionada(DateTime fechaSeleccionada)
        {
            if (hayAsistenciasModificadas())
            {
                vista.ponerEnEstado(planillaAsistencia.ESTADO_SICAMBIO_NOSELECCION);
            }

            mostrarAsistenciasDeFecha(fechaSeleccionada);
        }

        private bool hayAsistenciasModificadas()
        {
            foreach (Asistencia asistencia in asistenciasTrabajadas.obtenerDatos())
            {
                if (asistencia.estaModificada()) return true;
            }
            return false;
        }

        // La fecha de las asistencias que deberian ser recargadas
        private void mostrarAsistenciasDeFecha(DateTime fecha)
        {
            List<Asistencia> asistenciasDeFecha = modelo.obtenerAsistenciasParaFecha(fecha);

            List<Asistencia> asistenciasManana = new List<Asistencia>();
            List<Asistencia> asistenciasTarde = new List<Asistencia>();
            List<Asistencia> asistenciasNoche = new List<Asistencia>();

            if (asistenciasDeFecha != null)
            {
                foreach (Asistencia asistencia in asistenciasDeFecha)
                {
                    TimeSpan horaClase = asistencia.ComienzoClaseEsperado;
                    if (rangoHorarioManana.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasManana.Add(asistencia);
                    }
                    else if (rangoHorarioTarde.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasTarde.Add(asistencia);
                    }
                    else
                    {
                        asistenciasNoche.Add(asistencia);
                    }
                }

                asistenciasManana.Sort();
                asistenciasTarde.Sort();
                asistenciasNoche.Sort();
            }

            vista.cargarAsistenciasTurnoManana(asistenciasManana);
            vista.cargarAsistenciasTurnoTarde(asistenciasTarde);
            vista.cargarAsistenciasTurnoNoche(asistenciasNoche);
        }        

        private void actualizarModelo()
        {
            modelo.refrescarDatosSoporte();
            modelo.refrescarAsistencias();
        }

        public void IObservadorCamposPlanilla.observarCambioDocente(Docente docente)
        {
            throw new NotImplementedException();
        }

        public void IObservadorCamposPlanilla.observarCambioAsignatura(Asignatura Asignatura)
        {
            throw new NotImplementedException();
        }

        public void IObservadorCamposPlanilla.observarCambioHoraRealDeSalida(TimeSpan horaSalida)
        {
            throw new NotImplementedException();
        }

        public void IObservadorCamposPlanilla.observarCambioHoraRealDeEntrada(TimeSpan horaEntrada)
        {
            throw new NotImplementedException();
        }

        public void IObservadorCamposPlanilla.observarCambioEstadoAsistencia(EstadoAsistencia estadoAsistencia)
        {
            throw new NotImplementedException();
        }

        public void IObservadorCamposPlanilla.observarCambioObservaciones(string observaciones)
        {
            throw new NotImplementedException();
        }

        public void IObservadorCamposPlanilla.observarCambioCantidadAlumnos(int cantidadAlumnos)
        {
            throw new NotImplementedException();
        }

        private void Temporizador.ITemporizable.procesarTick()
        {
            actualizarModelo();
        }
    }
}
