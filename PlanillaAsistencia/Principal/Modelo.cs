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
        //private List<IObservadorModelo> observadores = new List<IObservadorModelo>();

        // Este diccionario se usara para mantener en memoria las asistencias que ya se hayan buscado desde la base de datos ordenadas
        // en listas por fecha.
        private DiccionarioAsistenciasPorFecha diccionarioAsistenciasPorFecha = new DiccionarioAsistenciasPorFecha();
        
        // Diccionario que mantiene en memoria las asistencias que ya se han buscado pero clasificadas por su id
        private Dictionary<int, Asistencia> diccionarioAsistencias = new Dictionary<int, Asistencia>();

        // Las asistencias sobre las que el usuario ha realizado alguna modificacion
        private Dictionary<int, Asistencia> asistenciasModificadas = new Dictionary<int,Asistencia>();

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
        // que es un List<Asistencia>. Si no encuentra, busca en la base de datos las asistencias que esten
        // en la misma semana correspondiente a la fecha pasada por parametro. Esto ultimo lo hace invocando
        // al metodo privado cargarAsistenciasDeSemanaAlDiccionario
        public List<Asistencia> getAsistenciasParaFecha(DateTime fecha, bool tenerEnCuentaAsistenciasModificadas)
        {
            List<Asistencia> asistenciasDeFecha = diccionarioAsistenciasPorFecha.obtenerAsistenciasParaFecha(fecha);

            if (asistenciasDeFecha == null)
            {
                cargarAsistenciasDeSemanaADiccionarios(fecha);
                asistenciasDeFecha = diccionarioAsistenciasPorFecha.obtenerAsistenciasParaFecha(fecha);
            }
             
            if (tenerEnCuentaAsistenciasModificadas)
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
            }
            
        }

        public void combinarAsistenciasModificadasEnDiccionarios()
        {
            foreach (Asistencia asistenciaModificada in asistenciasModificadas.Values)
            {
                quitarAsistenciaDeDiccionarios(asistenciaModificada);
                agregarAsistenciaEnDiccionarios(asistenciaModificada);
            }

            asistenciasModificadas.Clear();
        }

        // Este metodo toma todas las asistencias de una semana que viene dada por la semana a la cual pertenece
        // la fecha pasada por parametro y las agrega en el diccionaro de asistencias. De esta forma no se tiene que
        // acceder a la base de datos dia por dia.
        private void cargarAsistenciasDeSemanaADiccionarios(DateTime fecha)
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
                List<Asistencia> asistenciasDeFecha = diccionarioAsistenciasPorFecha.obtenerAsistenciasParaFecha(fechaDeAsistenciaActual);

                if (asistenciasDeFecha == null)
                {
                    diccionarioAsistenciasPorFecha.setearEntrada(fechaDeAsistenciaActual, new List<Asistencia>());
                }
                /*if (!diccionarioAsistenciasPorFecha.ContainsKey(fechaDeAsistenciaActual))
                {
                    diccionarioAsistenciasPorFecha[fechaDeAsistenciaActual] = new List<Asistencia>();
                }*/
            }
        }

        // Agrega la asistencia en el diccionario de asistencias organizadas por id y en el que esta organizado por fecha.
        // Si la asistencia ya exista, no hace nada y devuelve false. De lo contrario, la agrega a los diccionarios y
        // devuelve true
        public bool agregarAsistenciaEnDiccionarios(Asistencia asistencia)
        {
            // Si ya existe una asistencia con ese id agregada a los diccionarios, no hacemos nada
            if(diccionarioAsistencias.ContainsKey(asistencia.Id)){
                return false;
            }

            // Se agrega al diccionario de ids
            diccionarioAsistencias[asistencia.Id] = asistencia;

            // Se agrega al diccionario por fechas
            diccionarioAsistenciasPorFecha.agregarAsistencia(asistencia);

            return true;
        }

        // Devuelve true si existia una asistencia en el diccionario con el id de la asistencia
        // pasada por parametro y se la quito del diccionario. Si no devuelve false
        public bool quitarAsistenciaDeDiccionarios(Asistencia asistencia)
        {
            if (!diccionarioAsistencias.ContainsKey(asistencia.Id))
            {
                return false;
            }

            // Quitamos del diccionario por id
            diccionarioAsistencias.Remove(asistencia.Id);

            // Quitamos del diccionario por fecha
            return diccionarioAsistenciasPorFecha.quitarAsistencia(asistencia);
        }

        public void limpiarAsistenciasDelModelo()
        {
            diccionarioAsistencias.Clear();
            diccionarioAsistenciasPorFecha.limpiarDiccionario();
        }

        public void quitarAsistenciaModificada(Asistencia asistencia)
        {
            asistenciasModificadas.Remove(asistencia.Id);
        }

        // Devuelve el listado de asistencias que han sufrido una modificacion
        public List<Asistencia> getAsistenciasModificadas()
        {
            List<Asistencia> asistencias = new List<Asistencia>();
            asistencias.AddRange(asistenciasModificadas.Values);

            return asistencias;
        }

        // Obtiene la asistencia con la id pasada por parametro. Si hay una asistencia modificada con ese id,
        // se devolvera dicha asistencia. Si no la hay, se la buscar en el diccionario de asistencia. Si no encuentra
        // nada devuelve null
        public Asistencia getAsistencia(int idAsistencia)
        {
            Asistencia asistenciaReturn = null;

            if (asistenciasModificadas.TryGetValue(idAsistencia, out asistenciaReturn))
            {
                asistenciaReturn = asistenciasModificadas[idAsistencia];
                return asistenciaReturn;
            }

            foreach (Asistencia asistencia in diccionarioAsistencias.Values)
            {
                if (asistencia.Id == idAsistencia)
                {
                    asistenciaReturn = asistencia;
                    return asistenciaReturn;
                }
            }

            return null;
        }

        // Limpia la lista de asistencias modificadas
        public void limpiarAsistenciasModificadas()
        {
            asistenciasModificadas.Clear();
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

        public void agregarAsistenciaModificada(Asistencia asistencia)
        {
            asistenciasModificadas[asistencia.Id] = asistencia;
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
            bool seGuardo = DAOAsistencias.updateAsistencias(getAsistenciasModificadas());

            if (seGuardo)
            {
                limpiarAsistenciasModificadas();
            }
            return seGuardo;
        }

        // Comprueba si hay que actualizar las asistencias que tiene en el diccionario de asistencias
        public void actualizar()
        {
            ControladorSincronizacionModelo controlador = new ControladorSincronizacionModelo(this);
            controlador.actualizarModelo();
        }

        public List<DateTime> getFechasDeAsistenciaCargadasEnDiccionario()
        {
            return diccionarioAsistenciasPorFecha.getFechasAlmacenadasComoDateTime();
        }

        public List<Asistencia> getAsistenciasEnMemoria()
        {
            List<Asistencia> asistencias = new List<Asistencia>();
            asistencias.AddRange(diccionarioAsistencias.Values);
            return asistencias;
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