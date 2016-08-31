using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using Utilidades;
using ContenedoresDeDatos;

namespace PlanillaAsistencia.Pantallas.EditorAsistencias
{
    public class ModeloEditorAsistencias
    {
        private ControladorEditorAsistencias controlador;
        public ControladorEditorAsistencias Controlador
        {
            set { controlador = value; }
        }

        private HashSet<DateTime> fechasSinAsistencias;

        private ContenedorAsignaturas asignaturas;
        private ContenedorAsistencias asistencias;
        private ContenedorAulas aulas;
        private ContenedorCursos cursos;
        private ContenedorDocentes docentes;
        private ContenedorEstadosAsistencia estadosAsistencia;

        public ModeloEditorAsistencias()
        {
            fechasSinAsistencias = new HashSet<DateTime>();

            asignaturas = new ContenedorAsignaturas();
            asistencias = new ContenedorAsistencias();
            aulas = new ContenedorAulas();
            cursos = new ContenedorCursos();
            docentes = new ContenedorDocentes();
            estadosAsistencia = new ContenedorEstadosAsistencia();
        }

        public void inicializar()
        {
            refrescarDatosSoporte();
        }

        public void refrescarDatosSoporte()
        {
            asignaturas.refrescarDatos();
            aulas.refrescarDatos();
            cursos.refrescarDatos();
            docentes.refrescarDatos();
            estadosAsistencia.refrescarDatos();
        }

        public void refrescarAsistencias()
        {
            List<DateTime> fechasDeAsistencias = asistencias.obtenerFechasDeAsistenciasAlmacenadas();
            List<Asistencia> asistenciasMemoria = asistencias.obtenerDatos();
            List<Asistencia> asistenciasBase = DAOAsistencias.obtenerAsistenciasDeFechas(fechasDeAsistencias);
            List<Asistencia> resultadoComparacion = new List<Asistencia>();

            Dictionary<int, Asistencia> asistenciasMemoriaDic = new Dictionary<int, Asistencia>();
            foreach (Asistencia asistencia in asistenciasMemoria)
            {
                asistenciasMemoriaDic.Add(asistencia.Id, asistencia);
            }

            Dictionary<int, Asistencia> asistenciasBaseDic = new Dictionary<int, Asistencia>();
            foreach (Asistencia asistencia in asistenciasBase)
            {
                asistenciasBaseDic.Add(asistencia.Id, asistencia);
            }

            // Recorremos todas las entradas del diccionario de asistencias de la base de datos
            foreach(KeyValuePair<int, Asistencia> valorBase in asistenciasBaseDic)
            {
                Asistencia asistenciaBase = valorBase.Value;
                Asistencia asistenciaMemoria;

                // Intentamos obtener una asistencia de la memoria que coincida con la asistencia de la base de datos
                if (asistenciasMemoriaDic.TryGetValue(valorBase.Key, out asistenciaMemoria))
                {
                    if (asistenciaMemoria.esModificada())
                    {
                        /*
                         * Si la asistencia en memoria fue modificada por el usuario y aun no la guardo, verificamos
                         * que la asistencia que acabamos de traer de la base de datos sea igual a la asistencia modificada
                         * por el usuario pero en su estado inicial (Usamos un memento).
                         * */
                        if (asistenciaMemoria.EqualsEstadoInicial(asistenciaBase))
                        {
                            // Si efectivamente son iguales, dejamos la asistencia en memoria tal y como estaba (modificada)
                            resultadoComparacion.Add(asistenciaMemoria);
                        }
                        else
                        {
                            // Si no son iguales, damos prioridad a los cambios existentes en la asistencia de la base de datos
                            // y usamos esta asistencia en lugar de la que esta en memoria.
                            resultadoComparacion.Add(asistenciaBase);
                        }
                    }
                    else
                    {
                        // Aca metemos la asistencia de la base de datos directamente porque puede pasar que la asistencia
                        // que vino de la base de datos tenga algun cambio
                        resultadoComparacion.Add(asistenciaBase);
                    }
                }
                else
                {
                    // En caso de que en el diccionario de asistencia de la base de datos venga una asistencia
                    // que no figure en el diccionario de asistencias en memoria, la almacenamos en memoria sin
                    // preguntar nada
                    resultadoComparacion.Add(valorBase.Value);
                }
            }

            foreach (Asistencia asistencia in asistencias.obtenerDatos())
            {
                asistencia.guardarEstado();
            }
        }

        /*
         * Toma una fecha por parametro y busca en el contenedor de asistencias aquellas asistencias
         * cuya fecha sea igual a la pasada por parametro. Si no hay ninguna asistencia que coincida
         * con el criterio de busqueda, se consulta la base de datos y se trae todas las asistencias
         * que se encuentren dentro de la misma semana a la que pertenece la fecha parametro. Si tras
         * esta busqueda resulta que en esa fecha no hay asistencias, entonces agregamos dicha fecha
         * al hashSet 'fechasSinAsistencias' para que en el futuro no sea necesario hacer una nueva
         * busqueda para las fechas que figuran en este hashSet.
         * */

        // Falta ver como trabajar con el hashSet 'fechasSinAsistencias'
        public List<Asistencia> obtenerAsistenciasParaFecha(DateTime fecha)
        {
            List<Asistencia> asistenciasDeFecha = asistencias.obtenerAsistenciasDeFecha(fecha);

            if (asistenciasDeFecha.Count == 0)
            {
                HashSet<DateTime> fechasConAsistencias = new HashSet<DateTime>();

                List<Asistencia> asistenciasDeSemana = DAOAsistencias.obtenerAsistenciasParaUnaSemana(fecha);
                foreach (Asistencia asistencia in asistenciasDeSemana)
                {
                    asistencias.guardarDato(asistencia.Id, asistencia);
                    fechasConAsistencias.Add(asistencia.Fecha);

                    // Se guarda el estado inicial con que llegan las asistencias desde la base de datos
                    asistencia.guardarEstado();
                }

                if (!fechasConAsistencias.Contains(fecha.Date))
                {
                    this.fechasSinAsistencias.Add(fecha.Date);
                }
                else
                {
                    asistenciasDeFecha = asistencias.obtenerAsistenciasDeFecha(fecha);
                }
            }

            return asistenciasDeFecha;
        }

        public List<Asistencia> obtenerAsistencias()
        {
            return asistencias.obtenerDatos();
        }

        public void guardarAsistencia(Asistencia asistencia)
        {
            asistencias.guardarDato(asistencia.Id, asistencia);
        }

        public bool eliminarAsistencia(Asistencia asistencia)
        {
            return asistencias.eliminarDato(asistencia.Id);
        }

        public void eliminarAsistencias()
        {
            asistencias.limpiarContenedor();
        }

        public Asistencia obtenerAsistencia(int idAsistencia)
        {
            return asistencias.obtenerDato(idAsistencia);
        }

        public List<Asignatura> obtenerAsignaturas()
        {
            return asignaturas.obtenerDatos();
        }

        public List<Docente> obtenerDocentes()
        {
            return docentes.obtenerDatos();
        }

        public List<EstadoAsistencia> obtenerEstadosAsistencia()
        {
            return estadosAsistencia.obtenerDatos();
        }

        private class SincronizadorSoporte
        {

        }
    }
}