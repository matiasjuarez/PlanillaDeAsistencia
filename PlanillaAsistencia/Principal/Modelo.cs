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

        private HashSet<DateTime> fechasSinAsistencias;

        private ContenedorAsignaturas asignaturas;
        private ContenedorAsistencias asistencias;
        private ContenedorAulas aulas;
        private ContenedorCursos cursos;
        private ContenedorDocentes docentes;
        private ContenedorEstadosAsistencia estadosAsistencia;
        
        public Modelo(Controlador controlador)
        {
            this.controlador = controlador;

            fechasSinAsistencias = new HashSet<DateTime>();

            asignaturas = new ContenedorAsignaturas();
            asistencias = new ContenedorAsistencias();
            aulas = new ContenedorAulas();
            cursos = new ContenedorCursos();
            docentes = new ContenedorDocentes();
            estadosAsistencia = new ContenedorEstadosAsistencia();
        }

        public void refrescarDatosSoporte()
        {
            asignaturas.refrescarDatos();
            aulas.refrescarDatos();
            cursos.refrescarDatos();
            docentes.refrescarDatos();
            estadosAsistencia.refrescarDatos();
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
                    fechasConAsistencias.Add(asistencia.DiaDeAsistencia);
                }

                if (!fechasConAsistencias.Contains(fecha.Date))
                {
                    this.fechasSinAsistencias.Add(fecha.Date);
                }
            }

            return asistenciasDeFecha;
        }

        // Crea una asistencia dual y la agrega a los diccionarios por id y por fecha
        public bool agregarAsistenciaEnDiccionarios(Asistencia asistencia)
        {
            // Si ya existe una asistencia con ese id agregada a los diccionarios, no hacemos nada
            if(asistenciasPorId.ContainsKey(asistencia.Id)){
                return false;
            }

            AsistenciaDual asistenciaD = new AsistenciaDual(asistencia);
            // Se agrega al diccionario de ids
            asistenciasPorId[asistencia.Id] = asistenciaD;

            // Se agrega al diccionario por fechas
            diccionarioAsistenciasPorFecha.agregarAsistencia(asistenciaD);

            return true;
        }

        // Devuelve true si existia una asistencia en el diccionario con el id de la asistencia
        // pasada por parametro y se la quito del diccionario. Si no devuelve false
        public bool quitarAsistenciaDeDiccionarios(Asistencia asistencia)
        {
            if (!asistenciasPorId.ContainsKey(asistencia.Id))
            {
                return false;
            }

            // Quitamos del diccionario por id
            asistenciasPorId.Remove(asistencia.Id);

            // Quitamos del diccionario por fecha
            return diccionarioAsistenciasPorFecha.quitarAsistencia(asistencia);
        }

        public bool quitarAsistenciaDeDiccionarios(AsistenciaDual asistencia)
        {
            return quitarAsistenciaDeDiccionarios(asistencia.Original);
        }

        public void limpiarAsistenciasDelModelo()
        {
            asistenciasPorId.Clear();
            diccionarioAsistenciasPorFecha.limpiarDiccionario();
        }

        // Devuelve el listado de asistencias que han sufrido una modificacion
        public List<AsistenciaDual> getAsistenciasModificadas()
        {
            List<AsistenciaDual> asistencias = new List<AsistenciaDual>();

            foreach (AsistenciaDual asistenciaD in getAsistenciasEnMemoria())
            {
                if (asistenciaD.esModificada())
                {
                    asistencias.Add(asistenciaD);
                }
            }

            return asistencias;
        }

        // Obtiene la asistencia con la id pasada por parametro. Si no encuentra
        // nada devuelve null
        public AsistenciaDual getAsistencia(int idAsistencia)
        {
            foreach (AsistenciaDual asistencia in getAsistenciasEnMemoria())
            {
                if (asistencia.Original.Id == idAsistencia)
                {
                    return asistencia;
                }
            }
            return null;
        }

        // Devuelve una List de asignaturas
        public List<Asignatura> getAsignaturas()
        {
            // Solo iremos a la base de datos la primera vez
            if (asignaturas == null)
            {
                asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
            }
            
            return asignaturas;
        }

        // Devuelve un List de docentes
        public List<Docente> getDocentes()
        {
            if (docentes == null)
            {
                docentes = DAODocentes.obtenerTodosLosDocentes();
            }

            return docentes;
        }

        public List<EstadoAsistencia> getEstadosAsistencia()
        {
            if (estadosAsistencia == null)
            {
                estadosAsistencia = DAOEstadoAsistencia.obtenerTodosLosEstadosAsistencia();
            }

            return estadosAsistencia;
        }

        // Guarda en la base de datos las asistencias que se encuentran en la lista de modificadas
        public bool guardarAsistenciasModificadas()
        {
            List<Asistencia> asistenciasModificadas = new List<Asistencia>();
            foreach (AsistenciaDual asistencia in getAsistenciasModificadas())
            {
                asistenciasModificadas.Add(asistencia.Clonada);
            }

            bool seGuardo = DAOAsistencias.updateAsistencias(asistenciasModificadas);

            if (seGuardo)
            {
                foreach (AsistenciaDual asistencia in getAsistenciasModificadas())
                {
                    asistencia.Original = asistencia.Clonada;
                }
            }

            return seGuardo;
        }

        public List<DateTime> getFechasDeAsistenciaCargadasEnDiccionario()
        {
            return diccionarioAsistenciasPorFecha.getFechasAlmacenadasComoDateTime();
        }

        public List<AsistenciaDual> getAsistenciasEnMemoria()
        {
            List<AsistenciaDual> asistencias = new List<AsistenciaDual>();
            asistencias.AddRange(asistenciasPorId.Values);
            return asistencias;
        }

        public void setearAsistenciasClonadasComoOriginales()
        {
            foreach (AsistenciaDual asistenciaD in getAsistenciasEnMemoria())
            {
                asistenciaD.Original = asistenciaD.Clonada;
            }
        }
       /* public void agregarObservador(IObservadorModelo observador)
        {
            if (!observadores.Contains(observador))
            {
                observadores.Add(observador);
            }
        }

        public void notificarCambiosEnModelo(){
            foreach (IObservadorModelo observador in observadores)
            {
                observador.observarCambioDatosModelo();
            }
        }

        public void notificarVaciadoAsistenciasModificadas()
        {
            foreach (IObservadorModelo observador in observadores)
            {
                observador.observarVaciadoDeAsistenciasModificadas();
            }
        }*/
    }
}