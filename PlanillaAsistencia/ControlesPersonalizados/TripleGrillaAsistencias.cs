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
    public partial class TripleGrillaAsistencias : UserControl
    {
        private Presentador presentador;
        private ControladorRecargaDeGrillas controladorRecarga;
        private ManejadorBindings manejadorBindings;

        private List<IObservadorTripleGrilla> observadores;
        
        public TripleGrillaAsistencias()
        {
            InitializeComponent();

            presentador = new Presentador(this);
            presentador.configurarPresentacionGrillas();

            controladorRecarga = new ControladorRecargaDeGrillas(this);

            manejadorBindings = new ManejadorBindings(this);

            observadores = new List<IObservadorTripleGrilla>();
        }

        public void cargarAsistenciasTurnoManana(List<Asistencia> asistencias)
        {
            manejadorBindings.actualizarBindingList(
                convertirEnAsistenciasParaTabla(asistencias), manejadorBindings.AsistenciasTurnoManana);
        }

        public void cargarAsistenciasTurnoTarde(List<Asistencia> asistencias)
        {
            manejadorBindings.actualizarBindingList(
                convertirEnAsistenciasParaTabla(asistencias), manejadorBindings.AsistenciasTurnoTarde);
        }

        public void cargarAsistenciasTurnoNoche(List<Asistencia> asistencias)
        {
            manejadorBindings.actualizarBindingList(
                convertirEnAsistenciasParaTabla(asistencias), manejadorBindings.AsistenciasTurnoNoche);
        }

        private List<AsistenciaTabla> convertirEnAsistenciasParaTabla(List<Asistencia> asistencias)
        {
            List<AsistenciaTabla> asistenciasParaTabla = new List<AsistenciaTabla>();
            foreach (Asistencia asistencia in asistencias)
            {
                AsistenciaTabla asistenciaTabla = new AsistenciaTabla(asistencia);
                asistenciasParaTabla.Add(asistenciaTabla);
            }

            return asistenciasParaTabla;
        }

        public void agregarObservador(IObservadorTripleGrilla observador)
        {
            if (!observadores.Contains(observador))
            {
                observadores.Add(observador);
            }
        }

        private void refrescarGrillas()
        {
            dgvTurnoManana.Refresh();
            dgvTurnoTarde.Refresh();
            dgvTurnoNoche.Refresh();
        }

        // Busca en la lista de DataGridViewRow si existe alguna fila que contenga una asistencia
        // con el id pasado por parametro
        private DataGridViewRow buscarFilaDeAsistencia(int idAsistencia)
        {
            foreach (DataGridViewRow fila in dgvTurnoManana.Rows)
            {
                AsistenciaTabla asistencia = (AsistenciaTabla)fila.DataBoundItem;
                if (asistencia.IdAsistencia == idAsistencia)
                {
                    return fila;
                }
            }

            foreach (DataGridViewRow fila in dgvTurnoTarde.Rows)
            {
                AsistenciaTabla asistencia = (AsistenciaTabla)fila.DataBoundItem;
                if (asistencia.IdAsistencia == idAsistencia)
                {
                    return fila;
                }
            }

            foreach (DataGridViewRow fila in dgvTurnoNoche.Rows)
            {
                AsistenciaTabla asistencia = (AsistenciaTabla)fila.DataBoundItem;
                if (asistencia.IdAsistencia == idAsistencia)
                {
                    return fila;
                }
            }

            return null;
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
                GestorExcepciones.mostrarExcepcion(ex);
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[dgv.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private class ManejadorBindings
        {
            private BindingList<AsistenciaTabla> asistenciasTurnoManana;
            public BindingList<AsistenciaTabla> AsistenciasTurnoManana { get; private set; }

            private BindingList<AsistenciaTabla> asistenciasTurnoTarde;
            public BindingList<AsistenciaTabla> AsistenciasTurnoTarde { get; private set; }

            private BindingList<AsistenciaTabla> asistenciasTurnoNoche;
            public BindingList<AsistenciaTabla> AsistenciasTurnoNoche { get; private set; }

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

            public Presentador(TripleGrillaAsistencias tga)
            {
                this.tga = tga;
            }

            public void pintarTodasLasFilasComoNormales()
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

            public void pintarFilaComoNormal(DataGridViewRow fila)
            {
                pintarFila(fila, fondoAsistenciaNormal);
            }

            public void pintarFilaComoModificada(DataGridViewRow fila)
            {
                pintarFila(fila, fondoAsistenciaModificada);
            }

            public void pintarFila(DataGridViewRow fila, Color color)
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

            }
        }
    }
}