using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using AccesoDatos;
using ContenedoresDeDatos;
using Utilidades;
using Entidades;

namespace PlanillaAsistencia.Pantallas.ModificacionAsistencias
{
    public class ControladorModificacionAsistencias : IObservadorCamposPlanilla, Temporizador.ITemporizable
    {
        private ModificacionAsistencias vista;
        private ModeloModificacionAsistencias modelo;

        // La asistencia sobre la que el usuario hizo click en la grilla
        private AsistenciaTabla asistenciaSeleccionada;
        // Las asistencias que se han ido cargando en las grillas luego de apretado el boton de guardado
        private ContenedorAsistencias asistenciasTrabajadas;

        private RangoHorario rangoHorarioManana = new RangoHorario("00:00:00", "12:00:00");
        private RangoHorario rangoHorarioTarde = new RangoHorario("12:00:00", "18:00:00");
        private RangoHorario rangoHorarioNoche = new RangoHorario("18:00:00", "23:59:59");

        private Temporizador temporizador;

        public ControladorModificacionAsistencias(ModificacionAsistencias vista, ModeloModificacionAsistencias modelo)
        {
            this.vista = vista;
            this.modelo = modelo;

            this.asistenciasTrabajadas = new ContenedorAsistencias();

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

            mostrarAsistenciasDeFecha(this.vista.obtenerFechaSeleccionada());

            asistenciaSeleccionada = null;

            vista.ponerEnEstado(determinarEstadoPlanillaAsistencia());
        }

        private void guardarAsistenciasModificadas()
        {
            List<Asistencia> asistenciasModificadasValidasParaGuardar = new List<Asistencia>();
            List<Asistencia> asistenciasNoValidas = new List<Asistencia>();

            foreach (Asistencia asistencia in asistenciasTrabajadas.obtenerDatos())
            {
                asistencia.calcularCondicion();

                if (asistencia.estaModificada())
                {
                    if (asistencia.esValidaParaGuardarse())
                    {
                        asistenciasModificadasValidasParaGuardar.Add(asistencia);
                    }
                    else
                    {
                        asistenciasNoValidas.Add(asistencia);
                    }
                }
            }

            int guardadas = asistenciasModificadasValidasParaGuardar.Count;
            int noValidas = asistenciasNoValidas.Count;

            if (guardadas > 0)
            {
                vista.mostrarMensaje("Se guardaron " + guardadas + " asistencias", Color.Green, 1500);
            }

            if (noValidas > 0)
            {
                vista.mostrarMensaje(noValidas + " asistencias tienen valores no validos", Color.Purple, 2000);
            }

            DAOAsistencias.updateAsistencias(asistenciasModificadasValidasParaGuardar);

            foreach (Asistencia asistencia in asistenciasModificadasValidasParaGuardar)
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
            this.asistenciaSeleccionada = asistencia;
            vista.mostrarDatosDeAsistencia(this.asistenciaSeleccionada.obtenerAsistencia());
            vista.ponerEnEstado(determinarEstadoPlanillaAsistencia());
        }

        // Cuando el usuario cambia la fecha seleccionada debe llamar a esta funcion.
        public void manejarCambioFechaSeleccionada(DateTime fechaSeleccionada)
        {
            this.asistenciaSeleccionada = null;
            mostrarAsistenciasDeFecha(fechaSeleccionada);

            vista.ponerEnEstado(determinarEstadoPlanillaAsistencia());
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

            List<AsistenciaTabla> asistenciasManana = new List<AsistenciaTabla>();
            List<AsistenciaTabla> asistenciasTarde = new List<AsistenciaTabla>();
            List<AsistenciaTabla> asistenciasNoche = new List<AsistenciaTabla>();

            if (asistenciasDeFecha != null)
            {
                foreach (Asistencia asistencia in asistenciasDeFecha)
                {
                    this.asistenciasTrabajadas.guardarDato(asistencia.Id, asistencia);

                    AsistenciaTabla nuevaAsistenciaTabla = new AsistenciaTabla(asistencia);
                    asistencia.calcularCondicion();

                    TimeSpan horaClase = asistencia.HoraEntradaEsperada;

                    if (rangoHorarioManana.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasManana.Add(nuevaAsistenciaTabla);
                    }
                    else if (rangoHorarioTarde.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasTarde.Add(nuevaAsistenciaTabla);
                    }
                    else
                    {
                        asistenciasNoche.Add(nuevaAsistenciaTabla);
                    }
                }

                OrdenadorAsistencias sorter = new OrdenadorAsistencias(ordenadorAsistenciaPorHoraEntradaEsperada);

                asistenciasManana.Sort((a1, a2) => sorter(a1, a2));
                asistenciasTarde.Sort((a1, a2) => sorter(a1, a2));
                asistenciasNoche.Sort((a1, a2) => sorter(a1, a2));
            }

            vista.cargarAsistenciasTurnoManana(asistenciasManana);
            vista.cargarAsistenciasTurnoTarde(asistenciasTarde);
            vista.cargarAsistenciasTurnoNoche(asistenciasNoche);
        }

        private delegate int OrdenadorAsistencias(AsistenciaTabla a1, AsistenciaTabla a2);
        private int ordenadorAsistenciaPorHoraEntradaEsperada(AsistenciaTabla a1, AsistenciaTabla a2)
        {
            return a1.obtenerAsistencia().obtenerEntradaEsperada().CompareTo(
                a2.obtenerAsistencia().obtenerEntradaEsperada());
        }

        private void actualizarModelo()
        {
            modelo.refrescarDatosSoporte();
            modelo.refrescarAsistencias();
        }

        private int determinarEstadoPlanillaAsistencia()
        {
            if (asistenciaSeleccionada == null)
            {
                if (hayAsistenciasModificadas())
                {
                    return ModificacionAsistencias.ESTADO_SICAMBIO_NOSELECCION;
                }
                else
                {
                    return ModificacionAsistencias.ESTADO_NOCAMBIO_NOSELECCION;
                }
            }
            else
            {
                if (hayAsistenciasModificadas())
                {
                    return ModificacionAsistencias.ESTADO_SICAMBIO_SISELECCION;
                }
                else
                {
                    return ModificacionAsistencias.ESTADO_NOCAMBIO_SISELECCION;
                }
            }
        }

        private void procesarModificacionDeAsistencia()
        {
            this.asistenciaSeleccionada.obtenerAsistencia().calcularCondicion();

            vista.ponerEnEstado(determinarEstadoPlanillaAsistencia());
        }

        void IObservadorCamposPlanilla.observarCambioDocente(Docente docente)
        {
            asistenciaSeleccionada.obtenerAsistencia().Docente = docente;
            procesarModificacionDeAsistencia();
        }

        void IObservadorCamposPlanilla.observarCambioAsignatura(Asignatura Asignatura)
        {
            asistenciaSeleccionada.obtenerAsistencia().Asignatura = Asignatura;
            procesarModificacionDeAsistencia();
        }

        void IObservadorCamposPlanilla.observarCambioHoraRealDeSalida(TimeSpan horaSalida)
        {
            asistenciaSeleccionada.obtenerAsistencia().HoraSalidaReal = horaSalida;
            procesarModificacionDeAsistencia();
        }

        void IObservadorCamposPlanilla.observarCambioHoraRealDeEntrada(TimeSpan horaEntrada)
        {
            asistenciaSeleccionada.obtenerAsistencia().HoraEntradaReal = horaEntrada;
            procesarModificacionDeAsistencia();
        }

        void IObservadorCamposPlanilla.observarCambioEstadoAsistencia(EstadoAsistencia estadoAsistencia)
        {
            asistenciaSeleccionada.obtenerAsistencia().EstadoAsistencia = estadoAsistencia;
            procesarModificacionDeAsistencia();
        }

        void IObservadorCamposPlanilla.observarCambioObservaciones(string observaciones)
        {
            asistenciaSeleccionada.Observaciones = observaciones;
            procesarModificacionDeAsistencia();
        }

        void IObservadorCamposPlanilla.observarCambioCantidadAlumnos(int cantidadAlumnos)
        {
            asistenciaSeleccionada.CantidadAlumnos = cantidadAlumnos;
            procesarModificacionDeAsistencia();
        }

        void Temporizador.ITemporizable.procesarTick()
        {
            actualizarModelo();
        }
    }
}
