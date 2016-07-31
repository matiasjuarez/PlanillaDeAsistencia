using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

using AccesoDatos;
using Entidades;
using Utilidades;

namespace PlanillaAsistencia
{
    public partial class planillaAsistencia : Form, IObservadorTripleGrilla
    {
        private Controlador controlador;
        public Controlador Controlador
        {
            set { controlador = value; }
        }

        private ManejadorControles manejadorControles;
        private ProcesadorEventos procesadorEventos;
        private MostradorDatos mostradorDatos;
        private ManejadorCambiosEstado manejadorCambiosEstado;

        private List<IObservadorCamposPlanilla> observadoresCamposEditables;

        public Escalador escalador;

        public const int ESTADO_NOCAMBIO_NOSELECCION = 0;
        public const int ESTADO_NOCAMBIO_SISELECCION = 1;
        public const int ESTADO_SICAMBIO_NOSELECCION = 2;
        public const int ESTADO_SICAMBIO_SISELECCION = 3;

        public planillaAsistencia()
        {
            InitializeComponent();

            observadoresCamposEditables = new List<IObservadorCamposPlanilla>();
            tripleGrillaAsistencias.agregarObservador(this);

            manejadorControles = new ManejadorControles(this);
            procesadorEventos = new ProcesadorEventos(this);
            mostradorDatos = new MostradorDatos(this);
            manejadorCambiosEstado = new ManejadorCambiosEstado(this);

            escalador = new Escalador(this);
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

        public void IObservadorTripleGrilla.recibirNotificacionFilaSeleccionada(AsistenciaTabla asistencia)
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

        public void cargarAsistenciasTurnoManana(List<Asistencia> asistencias)
        {
            tripleGrillaAsistencias.cargarAsistenciasTurnoManana(asistencias);
        }

        public void cargarAsistenciasTurnoTarde(List<Asistencia> asistencias)
        {
            tripleGrillaAsistencias.cargarAsistenciasTurnoTarde(asistencias);
        }

        public void cargarAsistenciasTurnoNoche(List<Asistencia> asistencias)
        {
            tripleGrillaAsistencias.cargarAsistenciasTurnoNoche(asistencias);
        }

        private void planillaAsistencia_Resize(object sender, EventArgs e)
        {
            cambiarTamanoControles(this);
        }

        
        private void cambiarTamanoControles(Control parent)
        {
            escalador.resize();
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

            private planillaAsistencia planilla;

            public ManejadorCambiosEstado(planillaAsistencia planilla)
            {
                this.planilla = planilla;
            }

            private void manejarCambioEstado(int estado)
            {
                int estadoAux = this.estadoActual;
                this.estadoActual = estado;

                if (estado == planillaAsistencia.ESTADO_NOCAMBIO_NOSELECCION)
                {
                    planilla.manejadorControles.habilitarBotonGuardado(false);
                    planilla.manejadorControles.habilitarCampos(false);
                    planilla.manejadorControles.resetearCampos();
                }
                else if (estado == planillaAsistencia.ESTADO_NOCAMBIO_SISELECCION)
                {
                    planilla.manejadorControles.habilitarBotonGuardado(false);
                    planilla.manejadorControles.habilitarCampos(true);
                }
                else if (estado == planillaAsistencia.ESTADO_SICAMBIO_NOSELECCION)
                {
                    planilla.manejadorControles.habilitarBotonGuardado(true);
                    planilla.manejadorControles.habilitarCampos(true);
                    planilla.manejadorControles.resetearCampos();
                }
                else if (estado == planillaAsistencia.ESTADO_SICAMBIO_SISELECCION)
                {
                    planilla.manejadorControles.habilitarBotonGuardado(true);
                    planilla.manejadorControles.habilitarCampos(true);
                }
                else
                {
                    this.estadoActual = estadoAux;
                }
            }
        }

        private class MostradorDatos
        {
            private planillaAsistencia planilla;

            public MostradorDatos(planillaAsistencia planilla)
            {
                this.planilla = planilla;
            }

            public void cargarCombos()
            {
                cargarCombo(planilla.cmbDocente, planilla.controlador.obtenerDocentes(), "nombre", "id");
                cargarCombo(planilla.cmbAsignatura, planilla.controlador.obtenerAsignaturas(), "nombre", "id");
                cargarCombo(planilla.cmbEstadoAsistencia, planilla.controlador.obtenerEstadosDeAsistencia(), "nombre", "id");
            }

            public void mostrarDatosAsistencia(Asistencia asistencia)
            {
                planilla.procesadorEventos.ProcesarEventosCamposEditables = false;

                planilla.mktxtHoraEntradaEsperada.Text = asistencia.ComienzoClaseEsperado.ToString();
                planilla.mktxtHoraSalidaEsperada.Text = asistencia.FinClaseEsperado.ToString();
                planilla.mktxtHoraEntradaReal.Text = asistencia.ComienzoClaseReal.ToString();
                planilla.mktxtHoraSalidaReal.Text = asistencia.FinClaseReal.ToString();
                planilla.txtObservaciones.Text = asistencia.Observaciones;
                planilla.numUpDownAlumnos.Value = asistencia.CantidadAlumnos;

                seleccionarEstadoAsistencia(asistencia.EstadoAsistencia.Id);
                seleccionarDocente(asistencia.Docente.Id);
                seleccionarAsignatura(asistencia.Asignatura.Id);

                planilla.procesadorEventos.ProcesarEventosCamposEditables = true;
            }

            // Recibe como parametro el id correspondiente a un estadoAsistencia y basandose en ese
            // id seleccionada el estadoAsistencia que corresponda del comboBox
            private void seleccionarEstadoAsistencia(int idEstadoAsistencia)
            {
                int length = planilla.cmbEstadoAsistencia.Items.Count;
                EstadoAsistencia estadoAsistencia = null;

                for (int i = 0; i < length; i++)
                {
                    estadoAsistencia = (EstadoAsistencia)planilla.cmbEstadoAsistencia.Items[i];

                    if (estadoAsistencia.Id == idEstadoAsistencia)
                    {
                        planilla.cmbEstadoAsistencia.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Recibe como parametro el id correspondiente a un docente y basandose en ese
            // id seleccionada el docente que corresponda del comboBox
            private void seleccionarDocente(int idDocente)
            {
                int length = planilla.cmbDocente.Items.Count;
                Docente docente = null;

                for (int i = 0; i < length; i++)
                {
                    docente = (Docente)planilla.cmbDocente.Items[i];

                    if (docente.Id == idDocente)
                    {
                        planilla.cmbDocente.SelectedIndex = i;
                    }
                }
            }

            // Recibe como parametro el id correspondiente a una asignatura(materia) y basandose en ese
            // id seleccionada la asignatura que corresponda del comboBox
            private void seleccionarAsignatura(int idAsignatura)
            {
                int length = planilla.cmbAsignatura.Items.Count;
                Asignatura asignatura = null;

                for (int i = 0; i < length; i++)
                {
                    asignatura = (Asignatura)planilla.cmbAsignatura.Items[i];

                    if (asignatura.Id == idAsignatura)
                    {
                        planilla.cmbAsignatura.SelectedIndex = i;
                    }
                }
            }

            private void cargarCombo<T>(ComboBox combo, List<T> data, string displayMember, string valueMember)
            {
                BindingSource source = new BindingSource();
                source.DataSource = data;

                combo.DataSource = source;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;
            }
        }

        private class ProcesadorEventos
        {
            private planillaAsistencia planilla;

            private bool procesarEventosCamposEditables = true;
            public bool ProcesarEventosCamposEditables
            {
                get { return procesarEventosCamposEditables; }
                set { procesarEventosCamposEditables = value; }
            }

            public ProcesadorEventos(planillaAsistencia planilla)
            {
                this.planilla = planilla;
            }

            public void procesarCambioSeleccionDocente(Object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                ComboBox combo = (ComboBox)sender;
                Docente docenteSeleccionado = (Docente)combo.SelectedItem;

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
                {
                    observador.observarCambioDocente(docenteSeleccionado);
                }
            }

            public void procesarCambioSeleccionAsignatura(Object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                ComboBox combo = (ComboBox)sender;
                Asignatura asignaturaSeleccionada = (Asignatura)combo.SelectedItem;

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
                {
                    observador.observarCambioAsignatura(asignaturaSeleccionada);
                }
            }

            public void procesarCambioSeleccionEstadoAsistencia(Object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                ComboBox combo = (ComboBox)sender;
                EstadoAsistencia estadoAsistenciaSeleccionado = (EstadoAsistencia)combo.SelectedItem;

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
                {
                    observador.observarCambioEstadoAsistencia(estadoAsistenciaSeleccionado);
                }
            }

            public void procesarCambioHoraEntradaReal(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                MaskedTextBox mask = (MaskedTextBox)sender;

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
                {
                    TimeSpan horaProcesada = procesarTextoHoraEntradaSalidaReal(mask);
                    observador.observarCambioHoraRealDeEntrada(horaProcesada);
                }
            }

            public void procesarCambioHoraSalidaReal(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                MaskedTextBox mask = (MaskedTextBox)sender;

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
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

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
                {
                    observador.observarCambioCantidadAlumnos(valor);
                }
            }

            public void procesarCambioObservaciones(object sender, EventArgs e)
            {
                if (!procesarEventosCamposEditables) return;

                TextBox txt = (TextBox)sender;
                string texto = txt.Text;

                foreach (IObservadorCamposPlanilla observador in planilla.observadoresCamposEditables)
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
                    try
                    {
                        hora = TimeSpan.Parse(textoCampo);
                    }
                    catch
                    {
                        hora = new TimeSpan(0, 0, 0);
                    }
                }

                return hora;
            }
        }

        private class ManejadorControles
        {
            private planillaAsistencia planilla;

            public ManejadorControles(planillaAsistencia planilla)
            {
                this.planilla = planilla;
            }

            // Limpias los campos de texto, deselecciona los combo box. Para hacer eso se pasa un valor true o false
            // dependiendo de si queremos o no resetear un cierto campo
            public void resetearCampos(bool docentes, bool asignaturas, bool horaEntradaEsperada,
                bool horaSalidaEsperada, bool horaEntradaReal, bool horaSalidaReal,
                bool cantidadAlumnos, bool asistencia, bool observaciones)
            {
                planilla.procesadorEventos.ProcesarEventosCamposEditables = false;

                if (docentes) planilla.cmbDocente.SelectedIndex = -1;
                if (asignaturas) planilla.cmbAsignatura.SelectedIndex = -1;
                if (horaEntradaEsperada) planilla.mktxtHoraEntradaEsperada.ResetText();
                if (horaSalidaEsperada) planilla.mktxtHoraSalidaEsperada.ResetText();
                if (horaEntradaReal) planilla.mktxtHoraEntradaReal.ResetText();
                if (horaSalidaReal) planilla.mktxtHoraSalidaReal.ResetText();
                if (cantidadAlumnos) planilla.numUpDownAlumnos.ResetText();
                if (asistencia) planilla.cmbEstadoAsistencia.SelectedIndex = -1;
                if (observaciones) planilla.txtObservaciones.ResetText();

                planilla.procesadorEventos.ProcesarEventosCamposEditables = true;
            }

            // Resetea todos los campos del formulario
            public void resetearCampos()
            {
                resetearCampos(true, true, true, true, true, true, true, true, true);
            }

            public void habilitarBotonGuardado(bool habilitar)
            {
                planilla.btnGuardar.Enabled = habilitar;
            }

            // Habilita o deshabilita un cierto campo a traves de las bandearas pasadas por parametro
            public void habilitarCampos(bool docentes, bool asignaturas, bool horaEntradaEsperada,
                bool horaSalidaEsperada, bool horaEntradaReal, bool horaSalidaReal,
                bool cantidadAlumnos, bool asistencia, bool observaciones)
            {
                planilla.cmbDocente.Enabled = docentes;
                planilla.cmbAsignatura.Enabled = asignaturas;
                planilla.mktxtHoraEntradaEsperada.Enabled = horaEntradaEsperada;
                planilla.mktxtHoraSalidaEsperada.Enabled = horaSalidaEsperada;
                planilla.mktxtHoraEntradaReal.Enabled = horaEntradaReal;
                planilla.mktxtHoraSalidaReal.Enabled = horaSalidaReal;
                planilla.numUpDownAlumnos.Enabled = cantidadAlumnos;
                planilla.cmbEstadoAsistencia.Enabled = asistencia;
                planilla.txtObservaciones.Enabled = observaciones;
            }

            // Habilita o deshabilita todos los campos con excepcion de las fechas del formulario
            public void habilitarCampos(bool habilitar)
            {
                habilitarCampos(habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar);
            }
        }
    }
}