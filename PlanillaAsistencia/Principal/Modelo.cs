using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using Utilidades;
using ContenedoresDeDatos;

namespace PlanillaAsistencia
{
    public class Modelo
    {
        private Controlador controlador;
        public Controlador Controlador
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
        
        public Modelo()
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
            asistencias.refrescarDatos();
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

            if (asistencias.Count == 0)
            {
                HashSet<DateTime> fechasConAsistencias = new HashSet<DateTime>();

                List<Asistencia> asistenciasDeSemana = DAOAsistencias.obtenerAsistenciasParaUnaSemana(fecha);
                foreach (Asistencia asistencia in asistenciasDeSemana)
                {
                    asistencias.guardarDato(asistencia.Id, asistencia);
                    fechasConAsistencias.Add(asistencia.Fecha);
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
    }
}