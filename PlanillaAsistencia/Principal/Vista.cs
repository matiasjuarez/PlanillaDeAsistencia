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

namespace PlanillaAsistencia
{
    public partial class planillaAsistencia : Form, IObservadorTripleGrilla
    {
        private Controlador controlador;

        private List<IObservadorCamposPlanilla> observadoresCamposEditables;

        // Los objetos mensajes permiten crear mensaje en labels y añadirle algun tipo de efecto a esos mensajes
        private Mensaje mensaje = null;

        // Inicializa la planilla
        public planillaAsistencia()
        {
            InitializeComponent();

            observadoresCamposEditables = new List<IObservadorCamposPlanilla>();

            tripleGrillaAsistencias.agregarObservador(this);
        }

        public void inicializar()
        {
            cargarCombo(cmbDocente, controlador.obtenerDocentes(), "nombre", "id");
            cargarCombo(cmbAsignatura, controlador.obtenerAsignaturas(), "nombre", "id");
            cargarCombo(cmbEstadoAsistencia, controlador.obtenerEstadosDeAsistencia(), "nombre", "id");
        }

        private void cargarCombo<T>(ComboBox combo, List<T> data, string displayMember, string valueMember)
        {
            BindingSource source = new BindingSource();
            source.DataSource = data;

            combo.DataSource = source;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
        }

        // Setea el controlador que manejara los eventos que se produzcan en la vista y en el modelo
        public void setControlador(Controlador controlador)
        {
            this.controlador = controlador;
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

        // Evento que se dispara cuando el usuario seleccionada una fecha del datePicker
        private void datePickerCargaAsistencia_ValueChanged(object sender, EventArgs e)
        {
            /*
             * Casteamos el sender que es el datePicker que desencadeno el evento
             */
            DateTimePicker dateTimePicker = (DateTimePicker)sender;

            // Obtenemos la fecha seleccionada
            DateTime fechaSeleccionada = dateTimePicker.Value.Date;

            cargarDatosParaFechaSeleccionada(fechaSeleccionada);
        }

        private void cargarDatosParaFechaSeleccionada(DateTime fechaSeleccionada)
        {
            habilitarManejadoresDeEventos(false);

            controlador.manejarCambioFechaSeleccionada(fechaSeleccionada);

            habilitarManejadoresDeEventos(true);
        }

        public DateTime obtenerFechaSeleccionada()
        {
            DateTime fechaSeleccionada = this.datePickerCargaAsistencia.Value.Date;
            return fechaSeleccionada;
        }

        // Limpias los campos de texto, deselecciona los combo box. Para hacer eso se pasa un valor true o false
        // dependiendo de si queremos o no resetear un cierto campo
        public void resetearCampos(bool docentes, bool asignaturas, bool horaEntradaEsperada,
            bool horaSalidaEsperada, bool horaEntradaReal, bool horaSalidaReal,
            bool cantidadAlumnos, bool asistencia, bool observaciones)
        {
            habilitarManejadoresDeEventos(false);
            if (docentes) cmbDocente.SelectedIndex = -1;
            if (asignaturas) cmbAsignatura.SelectedIndex = -1;
            if (horaEntradaEsperada) mktxtHoraEntradaEsperada.ResetText();
            if (horaSalidaEsperada) mktxtHoraSalidaEsperada.ResetText();
            if (horaEntradaReal) mktxtHoraEntradaReal.ResetText();
            if (horaSalidaReal) mktxtHoraSalidaReal.ResetText();
            if (cantidadAlumnos) numUpDownAlumnos.ResetText();
            if (asistencia) cmbEstadoAsistencia.SelectedIndex = -1;
            if (observaciones) txtObservaciones.ResetText();
            habilitarManejadoresDeEventos(true);
        }

        public void habilitarBotonGuardado(bool habilitar)
        {
            this.btnGuardar.Enabled = habilitar;
        }

        // Resetea todos los campos del formulario
        public void resetearCampos()
        {
            resetearCampos(true, true, true, true, true, true, true, true, true);
        }

        // Habilita o deshabilita un cierto campo a traves de las bandearas pasadas por parametro
        public void habilitarCampos(bool docentes, bool asignaturas, bool horaEntradaEsperada,
            bool horaSalidaEsperada, bool horaEntradaReal, bool horaSalidaReal,
            bool cantidadAlumnos, bool asistencia, bool observaciones)
        {

            cmbDocente.Enabled = docentes;
            cmbAsignatura.Enabled = asignaturas;
            mktxtHoraEntradaEsperada.Enabled = horaEntradaEsperada;
            mktxtHoraSalidaEsperada.Enabled = horaSalidaEsperada;
            mktxtHoraEntradaReal.Enabled = horaEntradaReal;
            mktxtHoraSalidaReal.Enabled = horaSalidaReal;
            numUpDownAlumnos.Enabled = cantidadAlumnos;
            cmbEstadoAsistencia.Enabled = asistencia;
            txtObservaciones.Enabled = observaciones;
            
        }

        // Habilita o deshabilita todos los campos con excepcion de las fechas del formulario
        public void habilitarCampos(bool habilitar)
        {
            habilitarCampos(habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar, habilitar);
        }

        // Este evento se dispara cuando se crea la planilla. Aca deberiamos pedirle al controlador
        // que deshabilite todos los campos de la planilla ya que no hay una asistencia seleccionada.
        private void planillaAsistencia_Load(object sender, EventArgs e)
        {
            controlador.manejarEventoLoad();
        }

        

        // Recibe como parametro los datos correspondientes a una asistencia dada y los carga
        // en los campos correspondientes
        public void cargarDatosDeAsistenciaSeleccionada(string horaEntradaEsperada, string horaSalidaEsperada,
            string horaEntradaReal, string horaSalidaReal, int cantidadAlumnos, 
            int idAsistencia, int idDocente, int idAsignatura, string observaciones, Asistencia asistencia) 
        {
            //habilitarManejadoresDeEventos(false);
            mktxtHoraEntradaEsperada.Text = horaEntradaEsperada;
            mktxtHoraSalidaEsperada.Text = horaSalidaEsperada;
            mktxtHoraEntradaReal.Text = horaEntradaReal;
            mktxtHoraSalidaReal.Text = horaSalidaReal;
            txtObservaciones.Text = observaciones;
            numUpDownAlumnos.Value = cantidadAlumnos;

            seleccionarEstadoAsistencia(idAsistencia);
            seleccionarDocente(idDocente);
            seleccionarAsignatura(idAsignatura);
            //habilitarManejadoresDeEventos(true);
        }

        public void cargarDatosDeAsistencia(Asistencia asistencia)
        {
            //habilitarManejadoresDeEventos(false);
            mktxtHoraEntradaEsperada.Text = asistencia.ComienzoClaseEsperado.TimeOfDay.ToString(); ;
            mktxtHoraSalidaEsperada.Text = asistencia.FinClaseEsperado.TimeOfDay.ToString();
            mktxtHoraEntradaReal.Text = asistencia.ComienzoClaseReal.TimeOfDay.ToString();
            mktxtHoraSalidaReal.Text = asistencia.FinClaseReal.TimeOfDay.ToString();
            txtObservaciones.Text = asistencia.Observaciones;
            numUpDownAlumnos.Value = asistencia.CantidadAlumnos;

            seleccionarEstadoAsistencia(asistencia.EstadoAsistencia.Id);
            seleccionarDocente(asistencia.Docente.Id);
            seleccionarAsignatura(asistencia.Asignatura.Id);
            //habilitarManejadoresDeEventos(true);
        }

        // Recibe como parametro el id correspondiente a un estadoAsistencia y basandose en ese
        // id seleccionada el estadoAsistencia que corresponda del comboBox
        private void seleccionarEstadoAsistencia(int idEstadoAsistencia)
        {
            int length = cmbEstadoAsistencia.Items.Count;
            EstadoAsistencia estadoAsistencia = null;

            for (int i = 0; i < length; i++)
            {
                estadoAsistencia = (EstadoAsistencia)cmbEstadoAsistencia.Items[i];

                if (estadoAsistencia.Id == idEstadoAsistencia)
                {
                    cmbEstadoAsistencia.SelectedIndex = i;
                    break;
                }
            }
        }

        // Recibe como parametro el id correspondiente a un docente y basandose en ese
        // id seleccionada el docente que corresponda del comboBox
        private void seleccionarDocente(int idDocente)
        {
            int length = cmbDocente.Items.Count;
            Docente docente = null;

            for (int i = 0; i < length; i++)
            {
                docente = (Docente)cmbDocente.Items[i];

                if (docente.Id == idDocente)
                {
                    cmbDocente.SelectedIndex = i;
                }
            }
        }

        // Recibe como parametro el id correspondiente a una asignatura(materia) y basandose en ese
        // id seleccionada la asignatura que corresponda del comboBox
        private void seleccionarAsignatura(int idAsignatura)
        {
            int length = cmbAsignatura.Items.Count;
            Asignatura asignatura = null;

            for (int i = 0; i < length; i++)
            {
                asignatura = (Asignatura)cmbAsignatura.Items[i];

                if (asignatura.Id == idAsignatura)
                {
                    cmbAsignatura.SelectedIndex = i;
                }
            }
        }

        // *************************************************************************************
        // *******LOS MANEJADORES DE EVENTOS DE LOS CAMPOS DE LA PLANILLA***********************
        // *************************************************************************************
        private void cmbDocente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            Docente docenteSeleccionado = (Docente)combo.SelectedItem;

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.NOMBRE_PROFESOR, docenteSeleccionado.Nombre);

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioDocente(docenteSeleccionado);
            }
        }

        private void cmbAsignatura_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            Asignatura asignaturaSeleccionada = (Asignatura)combo.SelectedItem;

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.NOMBRE_ASIGNATURA, asignaturaSeleccionada.Nombre);

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioAsignatura(asignaturaSeleccionada);
            }
        }

        private void cmbEstadoAsistencia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            EstadoAsistencia estadoAsistenciaSeleccionado = (EstadoAsistencia)combo.SelectedItem;

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.ESTADO_ASISTENCIA, estadoAsistenciaSeleccionado.Nombre);

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioEstadoAsistencia(estadoAsistenciaSeleccionado);
            }
        }

        private void mktxtHoraEntradaReal_TextChanged(object sender, EventArgs e)
        {
            MaskedTextBox mask = (MaskedTextBox)sender;
            string textoCampo = mask.Text;

            DateTime fecha = datePickerCargaAsistencia.Value.Date;

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

            fecha = fecha.Add(hora);

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.COMIENZO_CLASE_REAL, textoCampo);

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioHoraRealDeEntrada(fecha);
            }
        }

        private void mktxtHoraSalidaReal_TextChanged(object sender, EventArgs e)
        {
            MaskedTextBox mask = (MaskedTextBox)sender;
            string textoCampo = mask.Text;

            DateTime fecha = datePickerCargaAsistencia.Value.Date;
            
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

            fecha = fecha.Add(hora);

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.FIN_CLASE_REAL, textoCampo);

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioHoraRealDeSalida(fecha);
            }
        }

        private void numUpDownAlumnos_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown spinner = (NumericUpDown)sender;

            int valor = (int)spinner.Value;

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.CANTIDAD_ALUMNOS, valor.ToString());

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioCantidadAlumnos(valor);
            }
        }

        private void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string texto = txt.Text;

            tripleGrillaAsistencias.modificarAsistenciaSeleccionada(TripleGrillaAsistencias.OBSERVACIONES, texto);

            foreach (IObservadorCamposPlanilla observador in observadoresCamposEditables)
            {
                observador.observarCambioObservaciones(texto);
            }
        }

        private void cmbAsignatura_MouseClick(object sender, MouseEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            // Este metodo 'selectAll' tan mal llamado, selecciona todo el texto que aparece
            // en el combo box cuando seleccionas un elemento. La idea es que el usuario pueda empezar
            // a escribir directamente sin tener que hacer doble click sobre el texto para seleccionarlo
            // y luego borrarlo.
            combo.SelectAll();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (controlador.guardarAsistenciasModificadas())
            {
                tripleGrillaAsistencias.limpiarFilasModificadas();
                habilitarBotonGuardado(false);
                habilitarCampos(false);
                resetearCampos();
                mostrarMensaje("Los datos fueron guardados", Color.Green, 3000);
            }
            else
            {
                
            }
        }

        // Permite habilitar o deshabilitar los manejadores de eventos de los componentes de la plantilla.
        // Los vamos a deshabilitar cuando el usuario quiera seleccionar una asistencia de la grilla ya que
        // al seleccionar una asistencia, los datos de la misma se cargan en los campos correspondientes de
        // la planilla de asistencia y esto provoca que se disparen los eventos de cambio. Queremos evitar
        // justamente que eso pase, asi que los deshabilitamos, cargamos los datos y rehabilitamos los manejadores
        public void habilitarManejadoresDeEventos(bool habilitar)
        {
            if (habilitar)
            {
                habilitarManejadoresDeEventos(false);
                cmbDocente.SelectionChangeCommitted += cmbDocente_SelectionChangeCommitted;
                cmbAsignatura.SelectionChangeCommitted += cmbAsignatura_SelectionChangeCommitted;
                cmbEstadoAsistencia.SelectionChangeCommitted += cmbEstadoAsistencia_SelectionChangeCommitted;
                mktxtHoraEntradaReal.TextChanged += mktxtHoraEntradaReal_TextChanged;
                mktxtHoraSalidaReal.TextChanged += mktxtHoraSalidaReal_TextChanged;
                numUpDownAlumnos.ValueChanged += numUpDownAlumnos_ValueChanged;
                txtObservaciones.TextChanged += txtObservaciones_TextChanged;
            }
            else
            {
                cmbDocente.SelectionChangeCommitted -= cmbDocente_SelectionChangeCommitted;
                cmbAsignatura.SelectionChangeCommitted -= cmbAsignatura_SelectionChangeCommitted;
                cmbEstadoAsistencia.SelectionChangeCommitted -= cmbEstadoAsistencia_SelectionChangeCommitted;
                mktxtHoraEntradaReal.TextChanged -= mktxtHoraEntradaReal_TextChanged;
                mktxtHoraSalidaReal.TextChanged -= mktxtHoraSalidaReal_TextChanged;
                numUpDownAlumnos.ValueChanged -= numUpDownAlumnos_ValueChanged;
                txtObservaciones.TextChanged -= txtObservaciones_TextChanged;
            }
        }

        // ****************************************************************************************
        // *********METODOS PARA MOSTRAR MENSAJES INFORMATIVOS*************************************
        // ****************************************************************************************
        // El mensaje, el color del mensaje y la cantidad de tiempo que queremos que sea visible
        public void mostrarMensaje(string textoMensaje, Color color, int milisegundos)
        {
            this.mensaje.mostrarMensaje(textoMensaje, color, milisegundos);
        }

        public void mostrarMensaje(string textoMensaje)
        {
            this.mensaje.mostrarMensaje(textoMensaje);
        }

        public void mostrarMensaje(string textoMensaje, int milisegundos)
        {
            this.mensaje.mostrarMensaje(textoMensaje, Color.Black, milisegundos);
        }

        public void mostrarMensaje(string textoMensaje, Color color)
        {
            this.mensaje.mostrarMensaje(textoMensaje, color);
        }

        // ****************************************************************************************
        // ****************************************************************************************
        // ****************************************************************************************

        public void recibirNotificacionFilaSeleccionada()
        {
            controlador.manejarSeleccionDeAsistenciaDesdeGrilla();
        }

        public int obtenerIdAsistenciaSeleccionada()
        {
            int retorno = -1;
            string valorDeGrilla = tripleGrillaAsistencias.obtenerDatoDeAsistenciaSeleccionada(TripleGrillaAsistencias.ASISTENCIA_ID);
            try{
                if(valorDeGrilla != null){
                    retorno = Int32.Parse(valorDeGrilla);
                }
            }
            catch{
                return -1;
            }
            return retorno;
        }

        public void cargarAsistenciasEnGrillas(List<Asistencia> asistencias)
        {
            tripleGrillaAsistencias.mostrarAsistencias(asistencias);
        }

        public void marcarAsistenciaComoModificada(Asistencia asistencia)
        {
             tripleGrillaAsistencias.marcarAsistenciaComoModificada(asistencia);
        }
    }
}