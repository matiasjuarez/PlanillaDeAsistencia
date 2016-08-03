using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Utilidades;
using Entidades;

namespace PlanillaAsistencia.ControlesPersonalizados
{
    public partial class TripleGrillaAsistencias : UserControl, IObservadorCamposPlanilla
    {
        private Presentador presentador;
        private ControladorRecargaDeGrillas controladorRecarga;
        private ManejadorBindings manejadorBindings;
        private ManejadorGrillas manejadorGrillas;

        private List<IObservadorTripleGrilla> observadores;
        
        public TripleGrillaAsistencias()
        {
            InitializeComponent();

            presentador = new Presentador(this);
            presentador.configurarPresentacionGrillas();

            controladorRecarga = new ControladorRecargaDeGrillas(this);

            manejadorBindings = new ManejadorBindings(this);

            manejadorGrillas = new ManejadorGrillas(this);

            observadores = new List<IObservadorTripleGrilla>();
        }

        public void cargarAsistenciasTurnoManana(List<AsistenciaTabla> asistencias)
        {
            manejadorBindings.actualizarBindingList(asistencias, manejadorBindings.AsistenciasTurnoManana);
        }

        public void cargarAsistenciasTurnoTarde(List<AsistenciaTabla> asistencias)
        {
            manejadorBindings.actualizarBindingList(asistencias, manejadorBindings.AsistenciasTurnoTarde);
        }

        public void cargarAsistenciasTurnoNoche(List<AsistenciaTabla> asistencias)
        {
            manejadorBindings.actualizarBindingList(asistencias, manejadorBindings.AsistenciasTurnoNoche);
        }

        public void agregarObservador(IObservadorTripleGrilla observador)
        {
            if (!observadores.Contains(observador))
            {
                observadores.Add(observador);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grilla = (DataGridView)sender;
            AsistenciaTabla asistenciaSeleccionada;
            try
            {
                // Esta linea obtiene el objeto AsistenciaDatosParaTabla que esta ligada a la fila de la grilla
                asistenciaSeleccionada = (AsistenciaTabla)grilla.SelectedRows[0].DataBoundItem;

                foreach (IObservadorTripleGrilla observador in observadores)
                {
                    observador.recibirNotificacionFilaSeleccionada(asistenciaSeleccionada);
                }
            }
            catch (Exception ex)
            {
                //GestorExcepciones.mostrarExcepcion(ex);
            }
        }

        // Este evento solamente se dispara porque si no las grillas no se pintan bien si nunca se
        // las selecciono antes
        private void tabDatosDelDia_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejadorGrillas.refrescarGrillas();
        }

        private class ManejadorGrillas
        {
            private TripleGrillaAsistencias tripleGrilla;
            private Timer timerRefrescoGrillas;

            public ManejadorGrillas(TripleGrillaAsistencias tripleGrilla)
            {
                this.tripleGrilla = tripleGrilla;

                tripleGrilla.dgvTurnoNoche.ClearSelection();

                configurarTimerRefresco();
            }

            public void refrescarGrillas()
            {
                if (this.timerRefrescoGrillas.Enabled == false)
                {
                    this.timerRefrescoGrillas.Enabled = true;
                }
            }

            private void procesarRefrescoGrillas()
            {
                repintarGrilla(tripleGrilla.dgvTurnoManana);
                repintarGrilla(tripleGrilla.dgvTurnoTarde);
                repintarGrilla(tripleGrilla.dgvTurnoNoche);
            }

            private void repintarGrilla(DataGridView grilla)
            {
                foreach (DataGridViewRow fila in grilla.Rows)
                {
                    this.tripleGrilla.presentador.determinarModoPresentacionFila(fila);
                }
                grilla.Update();
                grilla.Refresh();
            }

            private void configurarTimerRefresco()
            {
                // Esta negrada se hace porque cuando se modifica un campo en la plantilla, la notificacion de
                // modificacion se lanza dos veces porque a Microsoft se le ocurrio que asi sea. Entonces
                // eso provoca que la grilla se refresque dos veces de forma muy rapida y terminamos viendo
                // un valor viejo en lugar del que realmente deberia mostrar.
                this.timerRefrescoGrillas = new Timer();
                timerRefrescoGrillas.Interval = 1 * 200;
                timerRefrescoGrillas.Tick += (o, i) =>
                {
                    procesarRefrescoGrillas();
                    timerRefrescoGrillas.Enabled = false;
                };
            }
        }


        private class ManejadorBindings
        {
            private BindingList<AsistenciaTabla> asistenciasTurnoManana;
            public BindingList<AsistenciaTabla> AsistenciasTurnoManana 
            {
                get { return asistenciasTurnoManana; }
            }

            private BindingList<AsistenciaTabla> asistenciasTurnoTarde;
            public BindingList<AsistenciaTabla> AsistenciasTurnoTarde
            {
                get { return asistenciasTurnoTarde; }
            }

            private BindingList<AsistenciaTabla> asistenciasTurnoNoche;
            public BindingList<AsistenciaTabla> AsistenciasTurnoNoche
            {
                get { return asistenciasTurnoNoche; }
            }

            private TripleGrillaAsistencias tripleGrilla;

            public ManejadorBindings(TripleGrillaAsistencias tripleGrilla)
            {
                this.tripleGrilla = tripleGrilla;

                asistenciasTurnoManana = new BindingList<AsistenciaTabla>();
                asistenciasTurnoTarde = new BindingList<AsistenciaTabla>();
                asistenciasTurnoNoche = new BindingList<AsistenciaTabla>();

                bindearListaATabla(tripleGrilla.dgvTurnoManana, asistenciasTurnoManana);
                bindearListaATabla(tripleGrilla.dgvTurnoTarde, asistenciasTurnoTarde);
                bindearListaATabla(tripleGrilla.dgvTurnoNoche, asistenciasTurnoNoche);
            }

            private void bindearListaATabla(DataGridView tabla, BindingList<AsistenciaTabla> asistencias)
            {
                tabla.DataSource = asistencias;
            }

            public void actualizarBindingList(List<AsistenciaTabla> asistencias, BindingList<AsistenciaTabla> bindingList)
            {
                bindingList.Clear();
                foreach (AsistenciaTabla asistencia in asistencias)
                {
                    bindingList.Add(asistencia);
                }

                tripleGrilla.manejadorGrillas.refrescarGrillas();
            }

            private void redibujarGrilla(DataGridView grilla)
            {
                grilla.Refresh();
            }
        }

        // Esta clase se encarga de controlar la logica que se debe hacer cuando
        // se recargan las grillas
        private class ControladorRecargaDeGrillas
        {
            private TripleGrillaAsistencias tga;
            // Se usan para restaurar la seleccion cuando se refresque la lista
            private AsistenciaTabla dgvMananaAsistenciaSeleccionada = null;
            private AsistenciaTabla dgvTardeAsistenciaSeleccionada = null;
            private AsistenciaTabla dgvNocheAsistenciaSeleccionada = null;

            public ControladorRecargaDeGrillas(TripleGrillaAsistencias tga)
            {
                this.tga = tga;
            }

            // Guarda el indice de la fila seleccionada en cada tabla
            public void guardarSeleccionActual()
            {
                dgvMananaAsistenciaSeleccionada = null;
                dgvTardeAsistenciaSeleccionada = null;
                dgvNocheAsistenciaSeleccionada = null;

                if (tga.dgvTurnoManana.SelectedRows.Count > 0)
                {
                    dgvMananaAsistenciaSeleccionada =
                        (AsistenciaTabla)tga.dgvTurnoManana.SelectedRows[0].DataBoundItem;
                }

                if (tga.dgvTurnoTarde.SelectedRows.Count > 0)
                {
                    dgvTardeAsistenciaSeleccionada =
                        (AsistenciaTabla)tga.dgvTurnoTarde.SelectedRows[0].DataBoundItem;
                }

                if (tga.dgvTurnoNoche.SelectedRows.Count > 0)
                {
                    dgvNocheAsistenciaSeleccionada =
                        (AsistenciaTabla)tga.dgvTurnoNoche.SelectedRows[0].DataBoundItem;
                }
            }

            // Reselecciona las filas que estaban seleccionadas antes de que
            // las grillas se recargaran (AsistenciaDatosParaTabla)fila.DataBoundItem
            public void restaurarSeleccion()
            {
                buscarYseleccionarAsistenciaEnGrilla(tga.dgvTurnoManana, dgvMananaAsistenciaSeleccionada);
                buscarYseleccionarAsistenciaEnGrilla(tga.dgvTurnoTarde, dgvTardeAsistenciaSeleccionada);
                buscarYseleccionarAsistenciaEnGrilla(tga.dgvTurnoNoche, dgvNocheAsistenciaSeleccionada);
            }

            private void buscarYseleccionarAsistenciaEnGrilla(DataGridView grilla, AsistenciaTabla asistencia)
            {
                grilla.ClearSelection();
                if (asistencia != null)
                {
                    foreach (DataGridViewRow fila in grilla.Rows)
                    {
                        AsistenciaTabla asistenciaActual = (AsistenciaTabla)fila.DataBoundItem;
                        if (asistencia.IdAsistencia == asistenciaActual.IdAsistencia)
                        {
                            fila.Selected = true;
                        }
                    }
                }
            }
        }

        // Esta clase se encarga de todos los aspectos relacionados a como se debe visualizar
        // la grilla
        private class Presentador
        {
            private TripleGrillaAsistencias tga;

            private Color fondoAsistenciaModificada = Color.Yellow;
            private Color fondoAsistenciaNormal = Color.White;
            private Color fondoSinHoraCargada = Color.DarkRed;
            private Color fondoAsistenciaNoValidaParaGuardar = Color.Purple;

            public Presentador(TripleGrillaAsistencias tga)
            {
                this.tga = tga;
            }

            public void determinarModoPresentacionFila(DataGridViewRow fila)
            {
                AsistenciaTabla asistenciaActual = (AsistenciaTabla)fila.DataBoundItem;
                if (asistenciaActual.esModificada())
                {
                    if (asistenciaActual.esValidaParaGuardarse()) pintarFilaComoModificada(fila);
                    else pintarFilaComoNoValidaParaGuardarse(fila);
                }
                else if (asistenciaActual.esSinHoraEntradaReal_PostHoraEntradaEsperada() || 
                    asistenciaActual.esSinHoraSalidaReal_PostHoraSalidaEsperada())
                {
                    pintarFilaSinHoraCargada(fila);
                }
                else
                {
                    pintarFilaComoNormal(fila);
                }
            }

            private void pintarTodasLasFilasComoNormales()
            {
                foreach (DataGridViewRow fila in tga.dgvTurnoManana.Rows)
                {
                    pintarFilaComoNormal(fila);
                }
                foreach (DataGridViewRow fila in tga.dgvTurnoTarde.Rows)
                {
                    pintarFilaComoNormal(fila);
                }
                foreach (DataGridViewRow fila in tga.dgvTurnoNoche.Rows)
                {
                    pintarFilaComoNormal(fila);
                }
            }

            private void pintarFilaComoNormal(DataGridViewRow fila)
            {
                pintarFila(fila, this.fondoAsistenciaNormal);
                fila.DefaultCellStyle.ForeColor = Color.Black;
            }

            private void pintarFilaComoModificada(DataGridViewRow fila)
            {
                pintarFila(fila, this.fondoAsistenciaModificada);
                fila.DefaultCellStyle.ForeColor = Color.Black;
            }

            private void pintarFilaSinHoraCargada(DataGridViewRow fila)
            {
                pintarFila(fila, this.fondoSinHoraCargada);
                fila.DefaultCellStyle.ForeColor = Color.White;
                
            }

            private void pintarFilaComoNoValidaParaGuardarse(DataGridViewRow fila)
            {
                pintarFila(fila, this.fondoAsistenciaNoValidaParaGuardar);
                fila.DefaultCellStyle.ForeColor = Color.White;
            }

            private void pintarFila(DataGridViewRow fila, Color color)
            {
                if (fila != null)
                {
                    if (color != null)
                    {
                        fila.DefaultCellStyle.BackColor = color;
                    }
                }
            }

            public void configurarPresentacionGrillas()
            {
                configurarGrilla(tga.dgvTurnoManana);
                configurarGrilla(tga.dgvTurnoTarde);
                configurarGrilla(tga.dgvTurnoNoche);
            }

            private void configurarGrilla(DataGridView grilla)
            {
                grilla.BackgroundColor = System.Drawing.SystemColors.Control;

                foreach (DataGridViewColumn column in grilla.Columns)
                {
                    column.MinimumWidth = 100;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                grilla.Columns[0].MinimumWidth = 300;

                grilla.DataBindingComplete += (o, i) =>
                {
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    grilla.Columns[grilla.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                };

                grilla.AllowUserToResizeColumns = false;
                grilla.AllowUserToResizeRows = false;
            }

            private void configurarAutosizeGrilla(DataGridView grilla)
            {
                grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                grilla.Columns[grilla.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public void observarCambioDocente(Docente docente)
        {
            manejadorGrillas.refrescarGrillas();
        }

        public void observarCambioAsignatura(Asignatura Asignatura)
        {
            manejadorGrillas.refrescarGrillas();
        }

        public void observarCambioHoraRealDeSalida(TimeSpan horaSalida)
        {
            manejadorGrillas.refrescarGrillas();
        }

        public void observarCambioHoraRealDeEntrada(TimeSpan horaEntrada)
        {
            manejadorGrillas.refrescarGrillas();
        }

        public void observarCambioEstadoAsistencia(EstadoAsistencia estadoAsistencia)
        {
            manejadorGrillas.refrescarGrillas();
        }

        public void observarCambioObservaciones(string observaciones)
        {
            manejadorGrillas.refrescarGrillas();
        }

        public void observarCambioCantidadAlumnos(int cantidadAlumnos)
        {
            manejadorGrillas.refrescarGrillas();
        }

        private void dgvTurnoNoche_CellStyleChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grilla = (DataGridView)sender;
            grilla.DefaultCellStyle.Font = new Font(grilla.Font, FontStyle.Bold);
        }
    }
}