using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using Utilidades;

namespace PlanillaAsistencia
{
    public class Modelo
    {
        // Diccionario que mantiene en memoria las asistencias que ya se han buscado pero clasificadas por su id
        private Dictionary<int, AsistenciaDual> asistenciasPorId = new Dictionary<int, AsistenciaDual>();

        // Este diccionario se usara para mantener en memoria las asistencias que ya se hayan buscado desde la base de datos ordenadas
        // en listas por fecha.
        private DiccionarioAsistenciasPorFecha diccionarioAsistenciasPorFecha = new DiccionarioAsistenciasPorFecha();

        private List<EstadoAsistencia> estadosAsistencia;
        private List<Asignatura> asignaturas;
        private List<Docente> docentes;

        private Controlador controlador;

        public void inicializar()
        {
            estadosAsistencia = getEstadosAsistencia();
            asignaturas = getAsignaturas();
            docentes = getDocentes();
        }

        public void setControlador(Controlador controlador)
        {
            this.controlador = controlador;
        }

        // Busca en el diccionario 'diccionarioAsistencias' si hay una entrada cargada para la fecha
        // pasada por parametro. Si la encuentra, devuelve el valor almacenado en dicha entrada
        // que es un List<AsistenciaDual>. Si no encuentra, busca en la base de datos las asistencias que esten
        // en la misma semana correspondiente a la fecha pasada por parametro. Esto ultimo lo hace invocando
        // al metodo privado cargarAsistenciasDeSemanaAlDiccionario
        public List<AsistenciaDual> getAsistenciasParaFecha(DateTime fecha, bool tenerEnCuentaAsistenciasModificadas)
        {
            List<AsistenciaDual> asistenciasDeFecha = diccionarioAsistenciasPorFecha.obtenerAsistenciasParaFecha(fecha);

            if (asistenciasDeFecha == null)
            {
                buscarAsistenciasEnBaseDatos(fecha);
                asistenciasDeFecha = diccionarioAsistenciasPorFecha.obtenerAsistenciasParaFecha(fecha);
            }

            return asistenciasDeFecha;
            /*if (tenerEnCuentaAsistenciasModificadas)
            {
                // Nos fijamos en el diccionario de asistencias modificadas si es que
                // hay una asistencia modificada para algun id que figure en la lista
                // asistenciasDeFecha. Si la hay, entonces la reemplazamos por la asistencia
                // que corresponda de la lista mencionada.
                Asistencia asistenciaModificada;
                List<Asistencia> listaDevolver = new List<Asistencia>();
                listaDevolver.AddRange(asistenciasDeFecha);

                for (int i = 0; i < listaDevolver.Count; i++)
                {
                    int keyBusqueda = listaDevolver.ElementAt(i).Id;

                    if (asistenciasModificadas.TryGetValue(keyBusqueda, out asistenciaModificada))
                    {
                        asistenciaModificada = asistenciasModificadas[keyBusqueda];
                        listaDevolver[i] = asistenciaModificada;
                    }
                }

                return listaDevolver;
            }
            else
            {
                return asistenciasDeFecha;
            }*/
            
        }

        /*public void combinarAsistenciasModificadasEnDiccionarios()
        {
            foreach (Asistencia asistenciaModificada in asistenciasModificadas.Values)
            {
                quitarAsistenciaDeDiccionarios(asistenciaModificada);
                agregarAsistenciaEnDiccionarios(asistenciaModificada);
            }

            asistenciasModificadas.Clear();
        }*/

        // Este metodo toma todas las asistencias de una semana que viene dada por la semana a la cual pertenece
        // la fecha pasada por parametro y las agrega en el diccionaro de asistencias. De esta forma no se tiene que
        // acceder a la base de datos dia por dia.
        private void buscarAsistenciasEnBaseDatos(DateTime fecha)
        {
            // Se obtienen las asistencias de una semana
            List<Asistencia> asistenciasDeSemana = DAOAsistencias.obtenerAsistenciasParaUnaSemana(fecha);

            foreach (Asistencia asistencia in asistenciasDeSemana)
            {
                agregarAsistenciaEnDiccionarios(asistencia);
            }

            // Si la fecha pasada por parametro representa una fecha en la que no hay asistencias en el rapla,
            // entonces cada vez que consultemos el diccionario nos va a devolver que no hay asistencias
            // para esa fecha y se va a hacer una consulta a la base de datos innecesaria. Para evitar esto,
            // le preguntamos al diccionario si tiene una entrada para la fecha pasada por parametro. Si la tiene,
            // no hacemos nada. Si NO la tiene, agregamos una entrada en la que colocaremos una lista vacia.
            // Hacemos esto no solo para la fecha pasada por parametro, si no para cada dia que pertenece a la semana
            // que se esta analizando.

            DateTime inicioDeSemana = fecha.AddDays(-(int)fecha.DayOfWeek);
            string fechaDeAsistenciaActual = "";

            for (int i = 0; i < 7; i++)
            {
                fechaDeAsistenciaActual = inicioDeSemana.AddDays(i).Date.ToString("d");
                List<AsistenciaDual> asistenciasDeFecha = diccionarioAsistenciasPorFecha.obtenerAsistenciasParaFecha(fechaDeAsistenciaActual);

                if (asistenciasDeFecha == null)
                {
                    diccionarioAsistenciasPorFecha.setearEntrada(fechaDeAsistenciaActual, new List<AsistenciaDual>());
                }
                /*if (!diccionarioAsistenciasPorFecha.ContainsKey(fechaDeAsistenciaActual))
                {
                    diccionarioAsistenciasPorFecha[fechaDeAsistenciaActual] = new List<Asistencia>();
                }*/
            }
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