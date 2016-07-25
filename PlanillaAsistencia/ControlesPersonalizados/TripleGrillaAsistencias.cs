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
        public const int NOMBRE_ASIGNATURA = 0;
        public const int COMIENZO_CLASE_ESPERADO = 1;
        public const int FIN_CLASE_ESPERADO = 2;
        public const int COMIENZO_CLASE_REAL = 3;
        public const int FIN_CLASE_REAL = 4;
        public const int NOMBRE_PROFESOR = 5;
        public const int ESTADO_ASISTENCIA = 6;
        public const int CANTIDAD_ALUMNOS = 7;
        public const int ENCARGADOS = 8;
        public const int ASISTENCIA_ID = 9;
        public const int OBSERVACIONES = 10;

        private Color fondoAsistenciaModificada = Color.Yellow;
        public Color FondoAsistenciaModificada
        {
            set { fondoAsistenciaModificada = value; }
        }

        private Color fondoAsistenciaNormal = Color.White;
        public Color FondoAsistenciaNormal
        {
            set { fondoAsistenciaNormal = value; }
        }

        private List<IObservadorTripleGrilla> observadores;
        
        private List<AsistenciaDatosParaTabla> asistenciasTurnoManana;
        private List<AsistenciaDatosParaTabla> asistenciasTurnoTarde;
        private List<AsistenciaDatosParaTabla> asistenciasTurnoNoche;

        private AsistenciaDatosParaTabla asistenciaSeleccionada;
        private DataGridView grillaSeleccionada;

        private RangoHorario rangoHorarioManana;
        private RangoHorario rangoHorarioTarde;
        private RangoHorario rangoHorarioNoche;

        public TripleGrillaAsistencias()
        {
            InitializeComponent();

            observadores = new List<IObservadorTripleGrilla>();

            asistenciasTurnoManana = new List<AsistenciaDatosParaTabla>();
            asistenciasTurnoTarde = new List<AsistenciaDatosParaTabla>();
            asistenciasTurnoNoche = new List<AsistenciaDatosParaTabla>();

            rangoHorarioManana = new RangoHorario("00:00:00", "12:00:00");
            rangoHorarioTarde = new RangoHorario("12:00:00", "18:00:00");
            rangoHorarioNoche = new RangoHorario("18:00:00", "23:59:59");
        }

        public void mostrarAsistencias(List<Asistencia> asistencias)
        {
            asistenciaSeleccionada = null;

            separarAsistenciasEnDistintosTurnos(asistencias);

            cargarDatosParaTurno(dgvTurnoManana, asistenciasTurnoManana);
            cargarDatosParaTurno(dgvTurnoTarde, asistenciasTurnoTarde);
            cargarDatosParaTurno(dgvTurnoNoche, asistenciasTurnoNoche);

            refrescarGrillas();
        }

        public void modificarAsistenciaSeleccionada(int campoDeAsistencia, string nuevoValor)
        {
            if (asistenciaSeleccionada != null)
            {
                if (campoDeAsistencia == NOMBRE_ASIGNATURA)
                {
                    asistenciaSeleccionada.NombreAsignatura = nuevoValor;
                }
                else if (campoDeAsistencia == COMIENZO_CLASE_ESPERADO)
                {
                    asistenciaSeleccionada.ComienzoClaseEsperado = nuevoValor;
                }
                else if (campoDeAsistencia == FIN_CLASE_ESPERADO)
                {
                    asistenciaSeleccionada.FinClaseEsperado = nuevoValor;
                }
                else if (campoDeAsistencia == COMIENZO_CLASE_REAL)
                {
                    asistenciaSeleccionada.ComienzoClaseReal = nuevoValor;
                }
                else if (campoDeAsistencia == FIN_CLASE_REAL)
                {
                    asistenciaSeleccionada.FinClaseReal = nuevoValor;
                }
                else if (campoDeAsistencia == NOMBRE_PROFESOR)
                {
                    asistenciaSeleccionada.NombreProfesor = nuevoValor;
                }
                else if (campoDeAsistencia == ESTADO_ASISTENCIA)
                {
                    asistenciaSeleccionada.EstadoAsistencia = nuevoValor;
                }
                else if (campoDeAsistencia == CANTIDAD_ALUMNOS)
                {
                    asistenciaSeleccionada.CantidadAlumnos = Int32.Parse(nuevoValor);
                }
                else if (campoDeAsistencia == ENCARGADOS)
                {
                    asistenciaSeleccionada.Encargados = nuevoValor;
                }
                else if (campoDeAsistencia == OBSERVACIONES)
                {
                    asistenciaSeleccionada.Observaciones = nuevoValor;
                }

                refrescarGrillas();
            }
        }

        public string obtenerDatoDeAsistenciaSeleccionada(int campoDeAsistencia)
        {
            if (asistenciaSeleccionada != null)
            {
                if (campoDeAsistencia == NOMBRE_ASIGNATURA)
                {
                    return asistenciaSeleccionada.NombreAsignatura;
                }
                else if (campoDeAsistencia == COMIENZO_CLASE_ESPERADO)
                {
                    return asistenciaSeleccionada.ComienzoClaseEsperado;
                }
                else if (campoDeAsistencia == FIN_CLASE_ESPERADO)
                {
                    return asistenciaSeleccionada.FinClaseEsperado;
                }
                else if (campoDeAsistencia == COMIENZO_CLASE_REAL)
                {
                    return asistenciaSeleccionada.ComienzoClaseReal;
                }
                else if (campoDeAsistencia == FIN_CLASE_REAL)
                {
                    return asistenciaSeleccionada.FinClaseReal;
                }
                else if (campoDeAsistencia == NOMBRE_PROFESOR)
                {
                    return asistenciaSeleccionada.NombreProfesor;
                }
                else if (campoDeAsistencia == ESTADO_ASISTENCIA)
                {
                    return asistenciaSeleccionada.EstadoAsistencia;
                }
                else if (campoDeAsistencia == CANTIDAD_ALUMNOS)
                {
                    return asistenciaSeleccionada.CantidadAlumnos.ToString();
                }
                else if (campoDeAsistencia == ENCARGADOS)
                {
                    return asistenciaSeleccionada.Encargados;
                }
                else if (campoDeAsistencia == ASISTENCIA_ID)
                {
                    return asistenciaSeleccionada.IdAsistencia.ToString();
                }
                else if (campoDeAsistencia == OBSERVACIONES)
                {
                    return asistenciaSeleccionada.Observaciones;
                }
            }

            return null;
        }

        public void marcarAsistenciaComoModificada(Asistencia asistencia)
        {
            if (asistencia != null)
            {
                pintarFilaComoModificada(buscarFilaDeAsistencia(asistencia.Id));
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

            if (grillaSeleccionada != null)
            {
                foreach (DataGridViewRow row in grillaSeleccionada.Rows)
                {
                    DataGridViewCell celdaId = row.Cells[row.Cells.Count - 1];
                    if ((int)celdaId.Value == asistenciaSeleccionada.IdAsistencia)
                    {
                        row.Selected = true;
                    }
                }
            }
        }



        private void pintarTodasLasFilasComoNormales()
        {
            foreach (DataGridViewRow fila in dgvTurnoManana.Rows)
            {
                pintarFilaComoNormal(fila);
            }
            foreach (DataGridViewRow fila in dgvTurnoTarde.Rows)
            {
                pintarFilaComoNormal(fila);
            }
            foreach (DataGridViewRow fila in dgvTurnoNoche.Rows)
            {
                pintarFilaComoNormal(fila);
            }
        }

        private void pintarFilaComoNormal(DataGridViewRow fila)
        {
            pintarFila(fila, fondoAsistenciaNormal);
        }

        private void pintarFilaComoModificada(DataGridViewRow fila)
        {
            pintarFila(fila, fondoAsistenciaModificada);
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

        // Carga objetos de la clase AsistenciaDatosParaTabla que es una representacion de los objetos
        // de clase 'Asistencia' especialmente diseñada para comportarse bien con los dataGridViewRow en
        // la tabla pasada por parametro
        private void cargarDatosParaTurno(DataGridView tabla, List<AsistenciaDatosParaTabla> asistencias)
        {
            var bindingList = new BindingList<AsistenciaDatosParaTabla>(asistencias);
            var source = new BindingSource(bindingList, null);

            tabla.DataSource = source;
        }

        // Separa las asistencias pasadas por parametro en 3 grupos: 
        //uno para la manana, otro para la tarde y otro para la noche
        private void separarAsistenciasEnDistintosTurnos(List<Asistencia> asistencias)
        {
            asistenciasTurnoManana = new List<AsistenciaDatosParaTabla>();
            asistenciasTurnoTarde = new List<AsistenciaDatosParaTabla>();
            asistenciasTurnoNoche = new List<AsistenciaDatosParaTabla>();

            foreach (Asistencia asistencia in asistencias)
            {
                TimeSpan horaClase = asistencia.ComienzoClaseEsperado.TimeOfDay;
                if (rangoHorarioManana.estaDentroDelRangoHorario(horaClase))
                {
                    asistenciasTurnoManana.Add(new AsistenciaDatosParaTabla(asistencia));
                }
                else if (rangoHorarioTarde.estaDentroDelRangoHorario(horaClase))
                {
                    asistenciasTurnoTarde.Add(new AsistenciaDatosParaTabla(asistencia));
                }
                else
                {
                    asistenciasTurnoNoche.Add(new AsistenciaDatosParaTabla(asistencia));
                }
            }

            asistenciasTurnoManana.Sort();
            asistenciasTurnoTarde.Sort();
            asistenciasTurnoNoche.Sort();
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
    }
}
