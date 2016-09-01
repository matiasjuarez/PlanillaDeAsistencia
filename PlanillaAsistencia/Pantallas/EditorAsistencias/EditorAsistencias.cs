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
using PlanillaAsistencia.ControlesPersonalizados;
using PlanillaAsistencia.Pantallas;

namespace PlanillaAsistencia.Pantallas.EditorAsistencias
{
    public partial class EditorAsistencias : ResizableControl, IObservadorTripleGrilla
    {
        private ControladorEditorAsistencias controlador;
        public ControladorEditorAsistencias Controlador
        {
            set { 
                controlador = value;
                observadoresCamposEditables.Insert(0, controlador);
            }
        }

        private ManejadorControles manejadorControles;
        private ProcesadorEventos procesadorEventos;
        private MostradorDatos mostradorDatos;
        private ManejadorCambiosEstado manejadorCambiosEstado;

        private List<IObservadorCamposPlanilla> observadoresCamposEditables;

        public const int ESTADO_NOCAMBIO_NOSELECCION = 0;
        public const int ESTADO_NOCAMBIO_SISELECCION = 1;
        public const int ESTADO_SICAMBIO_NOSELECCION = 2;
        public const int ESTADO_SICAMBIO_SISELECCION = 3;

        public EditorAsistencias()
        {
            InitializeComponent();
            inicializarEscalador();

            observadoresCamposEditables = new List<IObservadorCamposPlanilla>();
            agregarObservadorCamposEditables(tripleGrillaAsistencias);

            tripleGrillaAsistencias.agregarObservador(this);

            manejadorControles = new ManejadorControles(this);
            procesadorEventos = new ProcesadorEventos(this);
            mostradorDatos = new MostradorDatos(this);
            manejadorCambiosEstado = new ManejadorCambiosEstado(this);
        }

        public void inicializar()
        {
            procesadorEventos.ProcesarEventosCamposEditables = false;

            mostradorDatos.cargarCombos();

            manejadorControles.habilitarBotonGuardado(false);
            manejadorControles.habilitarCampos(false);
            manejadorControles.resetearCampos();

            procesadorEventos.ProcesarEventosCamposEditables = true;
        }

        public void agregarObservadorCamposEditables(IObservadorCamposPlanilla observador)
        {
            if (!observadoresCamposEditables.Contains(observador))
            {
                observadoresCamposEditables.Add(observador);
            }
        }

        public void quitarObservadorCamposEditables(IObservadorCamposPlanilla observador)
        {
            observadoresCamposEditables.Remove(observador);
        }

        void IObservadorTripleGrilla.recibirNotificacionFilaSeleccionada(AsistenciaTabla asistencia)
        {
            controlador.manejarSeleccionDeAsistenciaDesdeGrilla(asistencia);
        }

        public void ponerEnEstado(int ESTADO)
        {
            manejadorCambiosEstado.EstadoActual = ESTADO;
        }

        public DateTime obtenerFechaSeleccionada()
        {
            DateTime fechaSeleccionada = this.datePickerCargaAsistencia.Value.Date;
            return fechaSeleccionada;
        }

        public void mostrarDatosDeAsistencia(Asistencia asistencia)
        {
            mostradorDatos.mostrarDatosAsistencia(asistencia);
            this.tripleGrillaAsistencias.marcarAsistenciaComoSeleccionada(asistencia);
        }

        public void actualizar()
        {
            this.tripleGrillaAsistencias.refrescarGrillas();
        }

        // *************************************************************************************
        // *******LOS MANEJADORES DE EVENTOS DE LOS CAMPOS DE LA PLANILLA***********************
        // *************************************************************************************
        private void cmbDocente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioSeleccionDocente(sender, e);
        }

        private void cmbAsignatura_SelectionChangeCommitted(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioSeleccionAsignatura(sender, e);
        }

        private void cmbEstadoAsistencia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioSeleccionEstadoAsistencia(sender, e);
        }

        private void mktxtHoraEntradaReal_TextChanged(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioHoraEntradaReal(sender, e);
        }

        private void mktxtHoraSalidaReal_TextChanged(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioHoraSalidaReal(sender, e);
        }

        private void numUpDownAlumnos_ValueChanged(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioCantidadAlumnos(sender, e);
        }

        private void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            procesadorEventos.procesarCambioObservaciones(sender, e);
        }

        private void cmb_MouseClick(object sender, MouseEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            // Este metodo selecciona todo el texto del comboBox
            combo.SelectAll();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            controlador.manejarGuardarCambios();
        }

        // ****************************************************************************************
        // *********METODOS PARA MOSTRAR MENSAJES INFORMATIVOS*************************************
        // ****************************************************************************************
        // El mensaje, el color del mensaje y la cantidad de tiempo que queremos que sea visible
        public void mostrarMensaje(string textoMensaje, Color color, int milisegundos)
        {
            this.lbleMensajes.mostrarMensaje(textoMensaje, color, milisegundos);
        }

        public void mostrarMensaje(string textoMensaje)
        {
            this.lbleMensajes.mostrarMensaje(textoMensaje);
        }

        public void mostrarMensaje(string textoMensaje, int milisegundos)
        {
            this.lbleMensajes.mostrarMensaje(textoMensaje, Color.Black, milisegundos);
        }

        public void mostrarMensaje(string textoMensaje, Color color)
        {
            this.lbleMensajes.mostrarMensaje(textoMensaje, color);
        }

        // ****************************************************************************************
        // ****************************************************************************************
        // ****************************************************************************************

        public void cargarAsistenciasTurnoManana(List<AsistenciaTabla> asistencias)
        {
            tripleGrillaAsistencias.cargarAsistenciasTurnoManana(asistencias);
        }

        public void cargarAsistenciasTurnoTarde(List<AsistenciaTabla> asistencias)
        {
            tripleGrillaAsistencias.cargarAsistenciasTurnoTarde(asistencias);
        }

        public void cargarAsistenciasTurnoNoche(List<AsistenciaTabla> asistencias)
        {
            tripleGrillaAsistencias.cargarAsistenciasTurnoNoche(asistencias);
        }

        private void datePickerCargaAsistencia_CloseUp(object sender, EventArgs e)
        {
            controlador.manejarCambioFechaSeleccionada(obtenerFechaSeleccionada());
        }

        private class ManejadorCambiosEstado
        {
            private int estadoActual;
            public int EstadoActual
            {
                get { return estadoActual; }
                set 
                {
                    manejarCambioEstado(value);
                }
            }

            private EditorAsistencias principal;

            public ManejadorCambiosEstado(EditorAsistencias principal)
            {
                this.principal = principal;
            }

            private void manejarCambioEstado(int estado)
            {
                int estadoAux = this.estadoActual;
                this.estadoActual = estado;

                if (estado == EditorAsistencias.ESTADO_NOCAMBIO_NOSELECCION)
                {
                    principal.manejadorControles.habilitarBotonGuardado(false);
                    principal.manejadorControles.habilitarCampos(false);
                    principal.manejadorControles.resetearCampos();
                }
                else if (estado == EditorAsistencias.ESTADO_NOCAMBIO_SISELECCION)
                {
                    principal.manejadorControles.habilitarBotonGuardado(false);
                    principal.manejadorControles.habilitarCampos(true);
                }
                else if (estado == EditorAsistencias.ESTADO_SICAMBIO_NOSELECCION)
                {
                    principal.manejadorControles.habilitarBotonGuardado(true);
                    principal.manejadorControles.habilitarCampos(false);
                    principal.manejadorControles.resetearCampos();
                }
                else if (estado == EditorAsistencias.ESTADO_SICAMBIO_SISELECCION)
                {
                    principal.manejadorControles.habilitarBotonGuardado(true);
                    principal.manejadorControles.habilitarCampos(true);
                }
                else
                {
                    this.estadoActual = estadoAux;
                }
            }
        }

        private class MostradorDatos
        {
            private EditorAsistencias principal;

            public MostradorDatos(EditorAsistencias principal)
            {
                this.principal = principal;
            }

            public void cargarCombos()
            {
                CargadorCombo.cargar<Docente>(principal.cmbDocente, 
                    principal.controlador.obtenerDocentes(), "nombre", "id");

                CargadorCombo.cargar<Asignatura>(principal.cmbAsignatura, 
                    principal.controlador.obtenerAsignaturas(), "nombre", "id");

                CargadorCombo.cargar<EstadoAsistencia>(principal.cmbEstadoAsistencia, 
                    principal.controlador.obtenerEstadosDeAsistencia(), "nombre", "id");
            }

            public void mostrarDatosAsistencia(Asistencia asistencia)
            {
                principal.procesadorEventos.ProcesarEventosCamposEditables = false;

                principal.mktxtHoraEntradaEsperada.Text = asistencia.HoraEntradaEsperada.ToString();
                principal.mktxtHoraSalidaEsperada.Text = asistencia.HoraSalidaEsperada.ToString();
                principal.mktxtHoraEntradaReal.Text = asistencia.HoraEntradaReal.ToString();
                principal.mktxtHoraSalidaReal.Text = asistencia.HoraSalidaReal.ToString();
                principal.txtObservaciones.Text = asistencia.Observaciones;
                principal.numUpDownAlumnos.Value = asistencia.CantidadAlumnos;

                seleccionarEstadoAsistencia(asistencia.EstadoAsistencia.Id);
                seleccionarDocente(asistencia.Docente.Id);
                seleccionarAsignatura(asistencia.Asignatura.Id);

                principal.procesadorEventos.ProcesarEventosCamposEditables = true;
            }

            // Recibe como parametro el id correspondiente a un estadoAsistencia y basandose en ese
            // id seleccionada el estadoAsistencia que corresponda del comboBox
            private void seleccionarEstadoAsistencia(int idEstadoAsistencia)
            {
                int length = principal.cmbEstadoAsistencia.Items.Count;
                EstadoAsistencia estadoAsistencia = null;

                for (int i = 0; i < length; i++)
                {
                    estadoAsistencia = (EstadoAsistencia)principal.cmbEstadoAsistencia.Items[i];

                    if (estadoAsistencia.Id == idEstadoAsistencia)
                    {
                        principal.cmbEstadoAsistencia.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Recibe como parametro el id correspondiente a un docente y basandose en ese
            // id seleccionada el docente que corresponda del comboBox
            private void seleccionarDocente(int idDocente)
            {
                int length = principal.cmbDocente.Items.Count;
                Docente docente = null;

                for (int i = 0; i < length; i++)
                {
                    docente = (Docente)principal.cmbDocente.Items[i];

                    if (docente.Id == idDocente)
                    {
                        principal.cmbDocente.SelectedIndex = i;
                    }
                }
            }

            // Recibe como parametro el id correspondiente a una asignatura(materia) y basandose en ese
            // id seleccionada la asignatura que corresponda del comboBox
            private void seleccionarAsignatura(int idAsignatura)
            {
                int length = principal.cmbAsignatura.Items.Count;
                Asignatura asignatura = null;

                for (int i = 0; i < length; i++)
                {
                    asignatura = (Asignatura)principal.cmbAsignatura.Items[i];

                    if (asignatura.Id == idAsignatura)
                    {
                        principal.cmbAsignatura.SelectedIndex = i;
                    }
                }
            }

        }

        private class ProcesadorEventos
        {
            private EditorAsistencias principal;

            private bool procesarEventosCamposEditables = true;
            public bool ProcesarEventosCamposEditables
            {
                get { return procesarEventosCamposEditables; }
                set { procesarEventosCamposEditables = value; }
            }

            public ProcesadorEventos(EditorAsistencias principal)
            {
                this.principal = principal;
            }

            public void procesarCambioSeleccionDocente(Object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                ComboBox combo = (ComboBox)sender;
                Docente docenteSeleccionado = (Docente)combo.SelectedItem;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    observador.observarCambioDocente(docenteSeleccionado);
                }
            }

            public void procesarCambioSeleccionAsignatura(Object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                ComboBox combo = (ComboBox)sender;
                Asignatura asignaturaSeleccionada = (Asignatura)combo.SelectedItem;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    observador.observarCambioAsignatura(asignaturaSeleccionada);
                }
            }

            public void procesarCambioSeleccionEstadoAsistencia(Object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                ComboBox combo = (ComboBox)sender;
                EstadoAsistencia estadoAsistenciaSeleccionado = (EstadoAsistencia)combo.SelectedItem;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    observador.observarCambioEstadoAsistencia(estadoAsistenciaSeleccionado);
                }
            }

            public void procesarCambioHoraEntradaReal(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                MaskedTextBox mask = (MaskedTextBox)sender;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    TimeSpan horaProcesada = procesarTextoHoraEntradaSalidaReal(mask);
                    observador.observarCambioHoraRealDeEntrada(horaProcesada);
                }
            }

            public void procesarCambioHoraSalidaReal(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                MaskedTextBox mask = (MaskedTextBox)sender;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    TimeSpan horaProcesada = procesarTextoHoraEntradaSalidaReal(mask);
                    observador.observarCambioHoraRealDeSalida(horaProcesada);
                }
            }

            public void procesarCambioCantidadAlumnos(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                NumericUpDown spinner = (NumericUpDown)sender;
                int valor = (int)spinner.Value;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    observador.observarCambioCantidadAlumnos(valor);
                }
            }

            public void procesarCambioObservaciones(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                TextBox txt = (TextBox)sender;
                string texto = txt.Text;

                foreach (IObservadorCamposPlanilla observador in principal.observadoresCamposEditables)
                {
                    observador.observarCambioObservaciones(texto);
                }
            }

            private TimeSpan procesarTextoHoraEntradaSalidaReal(MaskedTextBox masked)
            {
                string textoCampo = masked.Text;

                TimeSpan hora = new TimeSpan(0, 0, 0);

                if (textoCampo != null && textoCampo != "")
                {
                    try { hora = TimeSpan.Parse(textoCampo); }
                    catch { hora = new TimeSpan(0, 0, 0); }
                }

                return hora;
            }
        }

        private class ManejadorControles
        {
            private EditorAsistencias principal;

            public ManejadorControles(EditorAsistencias principal)
            {
                this.principal = principal;
            }

            // Limpias los campos de texto, deselecciona los combo box. Para hacer eso se pasa un valor true o false
            // dependiendo de si queremos o no resetear un cierto campo
            public void resetearCampos(bool docentes, bool asignaturas, bool horaEntradaEsperada,
                bool horaSalidaEsperada, bool horaEntradaReal, bool horaSalidaReal,
                bool cantidadAlumnos, bool asistencia, bool observaciones)
            {
                principal.procesadorEventos.ProcesarEventosCamposEditables = false;

                if (docentes) principal.cmbDocente.SelectedIndex = -1;
                if (asignaturas) principal.cmbAsignatura.SelectedIndex = -1;
                if (horaEntradaEsperada) principal.mktxtHoraEntradaEsperada.ResetText();
                if (horaSalidaEsperada) principal.mktxtHoraSalidaEsperada.ResetText();
                if (horaEntradaReal) principal.mktxtHoraEntradaReal.ResetText();
                if (horaSalidaReal) principal.mktxtHoraSalidaReal.ResetText();
                if (cantidadAlumnos) principal.numUpDownAlumnos.ResetText();
                if (asistencia) principal.cmbEstadoAsistencia.SelectedIndex = -1;
                if (observaciones) principal.txtObservaciones.ResetText();

                principal.procesadorEventos.ProcesarEventosCamposEditables = true;
            }

            // Resetea todos los campos del formulario
            public void resetearCampos()
            {
                resetearCampos(true, true, true, true, true, true, true, true, true);
            }

            public void habilitarBotonGuardado(bool habilitar)
            {
                principal.btnGuardar.Enabled = habilitar;
            }

            // Habilita o deshabilita un cierto campo a traves de las bandearas pasadas por parametro
            public void habilitarCampos(bool docentes, bool asignaturas, bool horaEntradaEsperada,
                bool horaSalidaEsperada, bool horaEntradaReal, bool horaSalidaReal,
                bool cantidadAlumnos, bool asistencia, bool observaciones)
            {
                principal.cmbDocente.Enabled = docentes;
                principal.cmbAsignatura.Enabled = false;
                principal.mktxtHoraEntradaEsperada.Enabled = horaEntradaEsperada;
                principal.mktxtHoraSalidaEsperada.Enabled = horaSalidaEsperada;
                principal.mktxtHoraEntradaReal.Enabled = horaEntradaReal;
                principal.mktxtHoraSalidaReal.Enabled = horaSalidaReal;
                principal.numUpDownAlumnos.Enabled = cantidadAlumnos;
                principal.cmbEstadoAsistencia.Enabled = asistencia;
                principal.txtObservaciones.Enabled = observaciones;
            }

            // Habilita o deshabilita todos los campos con excepcion de las fechas del formulario
            public void habilitarCampos(bool habilitar)
            {
                habilitarCampos(habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar);
            }
        }
    }
}
