using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using Utilidades;

namespace PlanillaAsistencia.Pantallas.EditorAsistencias
{
    public class ModeloEditorAsistencias
    {
        private HashSet<DateTime> fechasSinAsistencias;

        private ControladorEditorAsistencias controlador;
        public ControladorEditorAsistencias Controlador
        {
            set { controlador = value; }
        }

        private List<Asignatura> asignaturas = new List<Asignatura>();
        public List<Asignatura> Asignaturas
        {
            get { return asignaturas; }
            set { asignaturas = value; }
        }

        private Dictionary<int, Asistencia> diccionarioAsistencias = new Dictionary<int, Asistencia>();
        public List<Asistencia> Asistencias
        {
            get { return diccionarioAsistencias.Values.ToList<Asistencia>(); }
            set 
            {
                diccionarioAsistencias.Clear();
                agregarAsistencias(value);
            }
        }

        public void agregarAsistencias(List<Asistencia> asistencias)
        {
            foreach (Asistencia asistencia in asistencias)
            {
                agregarAsistencia(asistencia);
            }
        }

        public void agregarAsistencia(Asistencia asistencia)
        {
            if (diccionarioAsistencias.ContainsKey(asistencia.Id))
            {
                diccionarioAsistencias[asistencia.Id] = asistencia;
            }
            else
            {
                diccionarioAsistencias.Add(asistencia.Id, asistencia);
            }
        }

        public Asistencia obtenerAsistencia(int idAsistencia)
        {
            Asistencia asistencia;

            this.diccionarioAsistencias.TryGetValue(idAsistencia, out asistencia);

            return asistencia;
        }

        public List<Asistencia> obtenerAsistencias(List<int> idsAsistencias)
        {
            List<Asistencia> asistenciasDevolver = new List<Asistencia>();

            foreach (int idAsistencia in idsAsistencias)
            {
                Asistencia asistencia = obtenerAsistencia(idAsistencia);

                if (asistencia != null) asistenciasDevolver.Add(asistencia);
            }

            return asistenciasDevolver;
        }

        public List<Asistencia> obtenerAsistencias(List<Asistencia> asistencias)
        {
            List<int> idAsistencias = new List<int>();
            foreach (Asistencia asistencia in asistencias)
            {
                idAsistencias.Add(asistencia.Id);
            }

            return obtenerAsistencias(idAsistencias);
        }

        public bool quitarAsistencia(int idAsistencia)
        {
            return this.diccionarioAsistencias.Remove(idAsistencia);
        }

        public void quitarAsistencias(List<Asistencia> asistencias)
        {
            foreach (Asistencia asistencia in asistencias)
            {
                quitarAsistencia(asistencia.Id);
            }
        }

        public void quitarAsistencias(List<int> idsSsistencias)
        {
            foreach (int id in idsSsistencias)
            {
                quitarAsistencia(id);
            }
        }

        private List<Aula> aulas = new List<Aula>();
        public List<Aula> Aulas
        {
            get { return aulas; }
            set { aulas = value; }
        }

        private List<Curso> cursos = new List<Curso>();
        public List<Curso> Cursos
        {
            get { return cursos; }
            set { cursos = value; }
        }

        private List<Docente> docentes = new List<Docente>();
        public List<Docente> Docentes
        {
            get { return docentes; }
            set { docentes = value; }
        }

        private List<EstadoAsistencia> estadosAsistencia = new List<EstadoAsistencia>();
        public List<EstadoAsistencia> EstadosAsistencia
        {
            get { return estadosAsistencia; }
            set { estadosAsistencia = value; }
        }

        public ModeloEditorAsistencias()
        {
            fechasSinAsistencias = new HashSet<DateTime>();
        }

        public void inicializar()
        {
            refrescarDatosSoporte();
        }

        public void refrescarDatosSoporte()
        {
            recargarAsignaturas();
            recargarAulas();
            recargarCursos();
            recargarDocentes();
            recargarEstadosAsistencia();
        }

        private void recargarAsignaturas()
        {
            this.asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
        }

        private void recargarAulas()
        {
            this.aulas = DAOAulas.obtenerTodasLasAulas();
        }

        private void recargarCursos()
        {
            this.cursos = DAOCursos.obtenerTodosLosCursos();
        }

        private void recargarDocentes()
        {
            this.docentes = DAODocentes.obtenerTodosLosDocentes();
        }

        private void recargarEstadosAsistencia()
        {
            this.estadosAsistencia = DAOEstadoAsistencia.obtenerTodosLosEstadosAsistencia();
        }

        // El valor devuelto indica si fue necesario sincronizar el modelo o no
        public bool sincronizarAsistencias()
        {
            bool seSincronizo = false;

            List<Asistencia> asistenciasSincronizadas = new List<Asistencia>();

            List<Asistencia> asistenciasBase = 
                DAOAsistencias.obtenerAsistenciasPorId(this.diccionarioAsistencias.Keys.ToList<int>());

            Dictionary<int, Asistencia> diccionarioAsistenciasBase = new Dictionary<int, Asistencia>();
            foreach (Asistencia asistenciaBase in asistenciasBase)
            {
                diccionarioAsistenciasBase.Add(asistenciaBase.Id, asistenciaBase);

                Asistencia asistenciaMemoria;

                // Intentamos obtener una asistencia de la memoria que coincida con la asistencia de la base de datos
                if (this.diccionarioAsistencias.TryGetValue(asistenciaBase.Id, out asistenciaMemoria))
                {
                    if (!asistenciaMemoria.EqualsEstadoInicial(asistenciaBase))
                    {
                        // Si no son iguales, damos prioridad a los cambios existentes en la asistencia de la base de datos
                        // y usamos esta asistencia en lugar de la que esta en memoria.
                        asistenciasSincronizadas.Add(asistenciaBase);
                        seSincronizo = true;
                    }
                }
                else
                {
                    // En caso que en las asistencias de la base de datos venga una asistencia
                    // que no figure en el diccionario de asistencias en memoria, la almacenamos en memoria sin
                    // preguntar nada
                    asistenciasSincronizadas.Add(asistenciaBase);
                    seSincronizo = true;
                }
            }

            /*
             * Si alguna de las asistencias que tenemos en memoria no figura entre las asistencias
             * que trajimos de la base de datos, la eliminamos.
             * */
            foreach (Asistencia asistenciaMemoria in Asistencias)
            {
                if (!diccionarioAsistenciasBase.ContainsKey(asistenciaMemoria.Id))
                {
                    quitarAsistencia(asistenciaMemoria.Id);
                    seSincronizo = true;
                }
            }

            agregarAsistencias(asistenciasSincronizadas);
            foreach (Asistencia asistencia in asistenciasSincronizadas)
            {
                asistencia.guardarEstado();
            }

            return seSincronizo;
        }

        /*
         * Toma una fecha por parametro y busca en la lista de asistencias local aquellas asistencias
         * cuya fecha sea igual a la pasada por parametro. Si no hay ninguna asistencia para esta fecha, 
         * se consulta la base de datos y se trae todas las asistencias
         * que se encuentren dentro de la misma semana a la que pertenece la fecha parametro. Finalmente,
         * nos fijamos que fechas de la semana con la que estamos trabajando no poseen asistencias y dejamos
         * dichas fechas marcadas para no hacer una busqueda contra la base de datos innecesariamente
         * */
        public List<Asistencia> obtenerAsistenciasParaFecha(DateTime fecha)
        {
            if (fechasSinAsistencias.Contains(fecha)) return new List<Asistencia>();

            List<Asistencia> asistenciasDeFecha = buscarAsistenciasParaFechaLocalmente(fecha);

            if (asistenciasDeFecha.Count == 0)
            {
                asistenciasDeFecha = buscarAsistenciasParaFechaBaseDatos(fecha);
            }

            return asistenciasDeFecha;
        }

        private List<Asistencia> buscarAsistenciasParaFechaLocalmente(DateTime fecha)
        {
            List<Asistencia> asistenciasDeFecha = new List<Asistencia>();
            foreach (Asistencia asistencia in Asistencias)
            {
                if (asistencia.Fecha.Equals(fecha.Date))
                {
                    asistenciasDeFecha.Add(asistencia);
                }
            }

            return asistenciasDeFecha;
        }

        private List<Asistencia> buscarAsistenciasParaFechaBaseDatos(DateTime fecha)
        {
            List<Asistencia> asistenciasDeSemana = DAOAsistencias.obtenerAsistenciasParaUnaSemana(fecha);
            this.agregarAsistencias(asistenciasDeSemana);

            List<Asistencia> asistenciasDeFecha = new List<Asistencia>();
            foreach (Asistencia asistencia in asistenciasDeSemana)
            {
                // Se guarda el estado inicial con que llegan las asistencias desde la base de datos
                asistencia.guardarEstado();

                if (asistencia.Fecha.Equals(fecha))
                {
                    asistenciasDeFecha.Add(asistencia);
                }
            }

            marcarFechasSinAsistencias(asistenciasDeSemana, fecha);

            return asistenciasDeFecha;
        }

        private void marcarFechasSinAsistencias(List<Asistencia> asistencias, DateTime fecha)
        {
            HashSet<DateTime> fechasConAsistencias = new HashSet<DateTime>();

            foreach (Asistencia asistencia in asistencias)
            {
                fechasConAsistencias.Add(asistencia.Fecha);
            }

            List<DateTime> fechasDeSemana = obtenerFechasDeSemana(fecha);
            foreach (DateTime fechaDeSemana in fechasDeSemana)
            {
                if (!fechasConAsistencias.Contains(fechaDeSemana.Date))
                {
                    this.fechasSinAsistencias.Add(fechaDeSemana.Date);
                }
            }
        }

        private List<DateTime> obtenerFechasDeSemana(DateTime fecha)
        {
            List<DateTime> diasDeSemana = new List<DateTime>();

            DateTime inicioSemana = fecha.AddDays(-(int)fecha.DayOfWeek);

            for (int i = 0; i <= 6; i++)
            {
                DateTime dia = inicioSemana.AddDays(i);
                diasDeSemana.Add(dia);
            }

            return diasDeSemana;
        }
    }
}