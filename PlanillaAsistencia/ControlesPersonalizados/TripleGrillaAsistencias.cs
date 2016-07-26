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

namespace PlanillaAsistencia
{
    public partial class TripleGrillaAsistencias : UserControl
    {
        private Presentador presentador;
        private ControladorRecargaDeGrillas controladorRecarga;

        private List<IObservadorTripleGrilla> observadores;
        
        private BindingList<AsistenciaDatosParaTabla> asistenciasTurnoManana;
        private BindingList<AsistenciaDatosParaTabla> asistenciasTurnoTarde;
        private BindingList<AsistenciaDatosParaTabla> asistenciasTurnoNoche;

        private AsistenciaDatosParaTabla asistenciaSeleccionada;
        private DataGridView grillaSeleccionada;

        private RangoHorario rangoHorarioManana;
        private RangoHorario rangoHorarioTarde;
        private RangoHorario rangoHorarioNoche;

        private DateTime fechaDeLasAsistencias;

        public TripleGrillaAsistencias()
        {
            InitializeComponent();
            presentador = new Presentador(this);
            presentador.configurarPresentacionGrillas();

            controladorRecarga = new ControladorRecargaDeGrillas(this);

            observadores = new List<IObservadorTripleGrilla>();

            asistenciasTurnoManana = new BindingList<AsistenciaDatosParaTabla>();
            asistenciasTurnoTarde = new BindingList<AsistenciaDatosParaTabla>();
            asistenciasTurnoNoche = new BindingList<AsistenciaDatosParaTabla>();

            bindearListaATabla(dgvTurnoManana, asistenciasTurnoManana);
            bindearListaATabla(dgvTurnoTarde, asistenciasTurnoTarde);
            bindearListaATabla(dgvTurnoNoche, asistenciasTurnoNoche);

            rangoHorarioManana = new RangoHorario("00:00:00", "12:00:00");
            rangoHorarioTarde = new RangoHorario("12:00:00", "18:00:00");
            rangoHorarioNoche = new RangoHorario("18:00:00", "23:59:59");
        }

        private void configuracionAdicional()
        {
            dgvTurnoNoche.Columns[0].MinimumWidth = 300;
            dgvTurnoNoche.BackgroundColor = System.Drawing.SystemColors.Control;
        }

        public void mostrarAsistencias(List<Asistencia> asistencias)
        {
            asistenciaSeleccionada = null;

            if (asistencias != null && asistencias.Count > 0)
            {
                ActualizarAsistencias(asistencias);
                fechaDeLasAsistencias = asistencias.ElementAt(0).ComienzoClaseEsperado.Date;
            }
        }

        public string NombreAsignatura
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.NombreAsignatura;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.NombreAsignatura = value;
            }
        }

        public string ComienzoClaseEsperado
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.ComienzoClaseEsperado;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.ComienzoClaseEsperado = value;
            }
        }

        public string FinClaseEsperado
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.FinClaseEsperado;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.FinClaseEsperado = value;
            }
        }

        public string ComienzoClaseReal
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.ComienzoClaseReal;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.ComienzoClaseReal = value;
            }
        }

        public string FinClaseReal
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.FinClaseReal;
                return "";
            }
            set
            {
                if(asistenciaSeleccionada != null) asistenciaSeleccionada.FinClaseReal = value;
            }
        }

        public string NombreProfesor
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.NombreProfesor;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.NombreProfesor = value;
            }
        }

        public string EstadoAsistencia
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.EstadoAsistencia;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.EstadoAsistencia = value;
            }
        }

        public int CantidadAlumnos
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.CantidadAlumnos;
                return -1;
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.CantidadAlumnos = value;
            }
        }

        public string Encargado
        {
            get
            {
                if (asistenciaSeleccionada != null)
                {
                    return asistenciaSeleccionada.Encargados;
                }
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.Encargados = value;
            }
        }

        public string Observaciones
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.Observaciones;
                return "";
            }
            set
            {
                if (asistenciaSeleccionada != null) asistenciaSeleccionada.Observaciones = value;
            }
        }

        public int IdAsistencia
        {
            get
            {
                if (asistenciaSeleccionada != null) return asistenciaSeleccionada.IdAsistencia;
                return -1;
            }
        }

        public void marcarAsistenciaComoModificada(Asistencia asistencia)
        {
            if (asistencia != null)
            {
                presentador.pintarFilaComoModificada(buscarFilaDeAsistencia(asistencia.Id));
            }
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
                AsistenciaDatosParaTabla asistencia = (AsistenciaDatosParaTabla)fila.DataBoundItem;
                if (asistencia.IdAsistencia == idAsistencia)
                {
                    return fila;
                }
            }

            foreach (DataGridViewRow fila in dgvTurnoTarde.Rows)
            {
                AsistenciaDatosParaTabla asistencia = (AsistenciaDatosParaTabla)fila.DataBoundItem;
                if (asistencia.IdAsistencia == idAsistencia)
                {
                    return fila;
                }
            }

            foreach (DataGridViewRow fila in dgvTurnoNoche.Rows)
            {
                AsistenciaDatosParaTabla asistencia = (AsistenciaDatosParaTabla)fila.DataBoundItem;
                if (asistencia.IdAsistencia == idAsistencia)
                {
                    return fila;
                }
            }

            return null;
        }

        private void bindearListaATabla(DataGridView tabla, BindingList<AsistenciaDatosParaTabla> asistencias)
        {
            tabla.DataSource = asistencias;
        }

        // Separa las asistencias pasadas por parametro en 3 grupos: 
        //uno para la manana, otro para la tarde y otro para la noche.
        // Luego de separar las asistencias, las coloca en la bindinglist correspondiente
        // a cada una de las tablas
        private void ActualizarAsistencias(List<Asistencia> asistencias)
        {
            List<AsistenciaDatosParaTabla> auxManana = new List<AsistenciaDatosParaTabla>();
            List<AsistenciaDatosParaTabla> auxTarde = new List<AsistenciaDatosParaTabla>();
            List<AsistenciaDatosParaTabla> auxNoche = new List<AsistenciaDatosParaTabla>();

            foreach (Asistencia asistencia in asistencias)
            {
                TimeSpan horaClase = asistencia.ComienzoClaseEsperado.TimeOfDay;
                if (rangoHorarioManana.estaDentroDelRangoHorario(horaClase))
                {
                    auxManana.Add(new AsistenciaDatosParaTabla(asistencia));
                }
                else if (rangoHorarioTarde.estaDentroDelRangoHorario(horaClase))
                {
                    auxTarde.Add(new AsistenciaDatosParaTabla(asistencia));
                }
                else
                {
                    auxNoche.Add(new AsistenciaDatosParaTabla(asistencia));
                }
            }

            auxManana.Sort();
            auxTarde.Sort();
            auxNoche.Sort();

            controladorRecarga.guardarSeleccionActual();

            asistenciasTurnoManana.Clear();
            agregarListABindingList(auxManana, asistenciasTurnoManana);
            asistenciasTurnoTarde.Clear();
            agregarListABindingList(auxTarde, asistenciasTurnoTarde);
            asistenciasTurnoNoche.Clear();
            agregarListABindingList(auxNoche, asistenciasTurnoNoche);

            controladorRecarga.restaurarSeleccion();
        }

        private void agregarListABindingList(List<AsistenciaDatosParaTabla> asistencias, BindingList<AsistenciaDatosParaTabla> bindingList)
        {
            foreach (AsistenciaDatosParaTabla asistencia in asistencias)
            {
                bindingList.Add(asistencia);
            }
        }

        

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grilla = (DataGridView)sender;

            try
            {
                // Esta linea obtiene el objeto AsistenciaDatosParaTabla que esta ligada a la fila de la grilla
                asistenciaSeleccionada = (AsistenciaDatosParaTabla)grilla.SelectedRows[0].DataBoundItem;
                grillaSeleccionada = grilla;

                foreach (IObservadorTripleGrilla observador in observadores)
                {
                    observador.recibirNotificacionFilaSeleccionada();
                }
            }
            catch (Exception ex)
            {
                GestorExcepciones.mostrarExcepcion(ex);
            }
            
        }

        private void dgvTurnoNoche_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[dgv.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        // Esta clase se encarga de controlar la logica que se debe hacer cuando
        // se recargan las grillas
        private class ControladorRecargaDeGrillas
        {
            private TripleGrillaAsistencias tga;
            // Se usan para restaurar la seleccion cuando se refresque la lista
            private int dgvMananaSelectedRowIndex = -1;
            private int dgvTardeSelectedRowIndex = -1;
            private int dgvNocheSelectedRowIndex = -1;
            private DateTime fechaCorrespondienteUltimasFilasSeleccionadas;

            public ControladorRecargaDeGrillas(TripleGrillaAsistencias tga)
            {
                this.tga = tga;
            }

            // Guarda el indice de la fila seleccionada en cada tabla
            public void guardarSeleccionActual()
            {
                dgvMananaSelectedRowIndex = -1;
                dgvTardeSelectedRowIndex = -1;
                dgvNocheSelectedRowIndex = -1;
                fechaCorrespondienteUltimasFilasSeleccionadas = tga.fechaDeLasAsistencias;

                if (tga.dgvTurnoManana.SelectedRows.Count > 0)
                {
                    dgvMananaSelectedRowIndex = tga.dgvTurnoManana.SelectedRows[0].Index;
                }

                if (tga.dgvTurnoTarde.SelectedRows.Count > 0)
                {
                    dgvTardeSelectedRowIndex = tga.dgvTurnoTarde.SelectedRows[0].Index;
                }

                if (tga.dgvTurnoNoche.SelectedRows.Count > 0)
                {
                    dgvNocheSelectedRowIndex = tga.dgvTurnoNoche.SelectedRows[0].Index;
                }
            }

            // Reselecciona las filas que estaban seleccionadas antes de que
            // las grillas se recargaran
            public void restaurarSeleccion()
            {
                if (this.fechaCorrespondienteUltimasFilasSeleccionadas.Equals(tga.fechaDeLasAsistencias))
                {
                    if (dgvMananaSelectedRowIndex >= 0)
                    {
                        tga.dgvTurnoManana.Rows[dgvMananaSelectedRowIndex].Selected = true;
                    }

                    if (dgvTardeSelectedRowIndex >= 0)
                    {
                        tga.dgvTurnoTarde.Rows[dgvTardeSelectedRowIndex].Selected = true;
                    }

                    if (dgvNocheSelectedRowIndex >= 0)
                    {
                        tga.dgvTurnoNoche.Rows[dgvNocheSelectedRowIndex].Selected = true;
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
