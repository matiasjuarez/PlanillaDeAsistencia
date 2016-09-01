using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using AccesoDatos;
using Utilidades;
using Entidades;
using Configuracion;

namespace PlanillaAsistencia.Pantallas.EditorAsistencias
{
    public class ControladorEditorAsistencias : IObservadorCamposPlanilla, Temporizador.ITemporizable
    {
        private Config config = Config.getInstance();

        private EditorAsistencias vista;
        private ModeloEditorAsistencias modelo;

        // La asistencia sobre la que el usuario hizo click en la grilla
        private AsistenciaTabla asistenciaSeleccionada;
        private Dictionary<int, Asistencia> asistenciasTrabajadas = new Dictionary<int, Asistencia>();

        private Temporizador temporizadorSincronizacionModelo;

        public ControladorEditorAsistencias(EditorAsistencias vista, ModeloEditorAsistencias modelo)
        {
            this.vista = vista;
            this.modelo = modelo;

            vista.Controlador = this;
            modelo.Controlador = this;

            modelo.inicializar();
            vista.inicializar();

            temporizadorSincronizacionModelo = new Temporizador(5);
            temporizadorSincronizacionModelo.agregarObjetoTemporizable(this);
            temporizadorSincronizacionModelo.habilitar(true);
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

            foreach (Asistencia asistencia in asistenciasTrabajadas.Values)
            {
                if (asistencia.esModificada())
                {
                    if (esAsistenciaValidaParaGuardar(asistencia))
                    {
                        asistenciasModificadasValidasParaGuardar.Add(asistencia);
                    }
                    else
                    {
                        asistenciasNoValidas.Add(asistencia);
                    }
                }
            }

            DAOAsistencias.updateAsistencias(asistenciasModificadasValidasParaGuardar);

            foreach (Asistencia asistencia in asistenciasModificadasValidasParaGuardar)
            {
                asistencia.guardarEstado();
                modelo.agregarAsistencia(asistencia);
                this.asistenciasTrabajadas.Remove(asistencia.Id);
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
        }

        private bool esAsistenciaValidaParaGuardar(Asistencia asistencia)
        {
            if (asistencia.CantidadAlumnos == 0) return false;
            if (asistencia.HoraEntradaReal.Equals(new TimeSpan(0, 0, 0))) return false;
            if (asistencia.esSinHoraSalidaReal_PostHoraSalidaEsperada()) return false;

            return true;
        }

        public List<Asignatura> obtenerAsignaturas()
        {
            return modelo.Asignaturas;
        }

        public List<Docente> obtenerDocentes()
        {
            return modelo.Docentes;
        }

        public List<EstadoAsistencia> obtenerEstadosDeAsistencia()
        {
            return modelo.EstadosAsistencia;
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
            foreach (Asistencia asistencia in this.asistenciasTrabajadas.Values)
            {
                if (asistencia.esModificada()) return true;
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
                    AsistenciaTabla nuevaAsistenciaTabla = new AsistenciaTabla(asistencia);

                    TimeSpan horaClase = asistencia.HoraEntradaEsperada;

                    if (config.RangoManana.estaDentroDelRangoHorario(horaClase))
                    {
                        asistenciasManana.Add(nuevaAsistenciaTabla);
                    }
                    else if (config.RangoTarde.estaDentroDelRangoHorario(horaClase))
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
            modelo.sincronizarAsistencias();
        }

        private int determinarEstadoPlanillaAsistencia()
        {
            bool existenAsistenciasModificadas = hayAsistenciasModificadas();

            if (asistenciaSeleccionada == null)
            {
                if (existenAsistenciasModificadas)
                {
                    return EditorAsistencias.ESTADO_SICAMBIO_NOSELECCION;
                }
                else
                {
                    return EditorAsistencias.ESTADO_NOCAMBIO_NOSELECCION;
                }
            }
            else
            {
                if (existenAsistenciasModificadas)
                {
                    return EditorAsistencias.ESTADO_SICAMBIO_SISELECCION;
                }
                else
                {
                    return EditorAsistencias.ESTADO_NOCAMBIO_SISELECCION;
                }
            }
        }

        private void procesarModificacionDeAsistencia()
        {
            if (asistenciasTrabajadas.ContainsKey(asistenciaSeleccionada.IdAsistencia))
            {
                asistenciasTrabajadas[asistenciaSeleccionada.IdAsistencia] = asistenciaSeleccionada.obtenerAsistencia();
            }
            else
            {
                asistenciasTrabajadas.Add(asistenciaSeleccionada.IdAsistencia, asistenciaSeleccionada.obtenerAsistencia());
            }
            
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

            if (this.asistenciaSeleccionada != null)
            {
                AsistenciaTabla asistenciaTabla = null;
                Asistencia asistencia = modelo.obtenerAsistencia(asistenciaSeleccionada.obtenerAsistencia().Id);

                if (asistencia != null) asistenciaTabla = new AsistenciaTabla(asistencia);
                 
                this.asistenciaSeleccionada = asistenciaTabla;

                if (asistencia != null)
                {
                    vista.mostrarDatosDeAsistencia(asistencia);
                }
                else
                {
                    vista.ponerEnEstado(this.determinarEstadoPlanillaAsistencia());
                }
            }

            List<Asistencia> asistenciasModeloActualizadas = 
                modelo.obtenerAsistencias(this.asistenciasTrabajadas.Values.ToList<Asistencia>());

            foreach (Asistencia asistencia in asistenciasModeloActualizadas)
            {
                if (this.asistenciasTrabajadas.ContainsKey(asistencia.Id))
                {
                    asistenciasTrabajadas[asistencia.Id] = asistencia;
                }
                else
                {
                    asistenciasTrabajadas.Add(asistencia.Id, asistencia);
                }
            }

            this.mostrarAsistenciasDeFecha(vista.obtenerFechaSeleccionada());
        }

        private void actualizarVista()
        {
            DateTime fechaSeleccionada = vista.obtenerFechaSeleccionada();
            this.mostrarAsistenciasDeFecha(fechaSeleccionada);

            if (fechaSeleccionada.Date.Equals(asistenciaSeleccionada.Fecha))
            {
                this.vista.mostrarDatosDeAsistencia(asistenciaSeleccionada.obtenerAsistencia());
            }
        }
    }
}