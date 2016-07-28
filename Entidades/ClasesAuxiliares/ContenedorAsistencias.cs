using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.ClasesAuxiliares
{
    public class ContenedorAsistencias
    {
        private Dictionary<int, Asistencia> asistenciasPorId = new Dictionary<int, Asistencia>();
        private Dictionary<string, List<Asistencia>> asistenciasPorFecha = new Dictionary<string, List<Asistencia>>();
        private bool noPermitirAsistenciasRepetidas = true;

        public int Count
        {
            get { return asistenciasPorId.Count; }
        }

        public void agregarAsistencia(Asistencia asistencia)
        {
            if (noPermitirAsistenciasRepetidas)
            {
                if (existeAsistencia(asistencia)) return;
            }

            asistenciasPorId.Add(asistencia.Id, asistencia);

            // Se agrega al diccionario por fechas
            string fechaAsistencia = asistencia.DiaDeAsistencia.Date.ToString("d");
            List<Asistencia> listaAsistenciasDeFecha;

            if (!asistenciasPorFecha.TryGetValue(fechaAsistencia, out listaAsistenciasDeFecha))
            {
                listaAsistenciasDeFecha = new List<Asistencia>();
                asistenciasPorFecha[fechaAsistencia] = listaAsistenciasDeFecha;
            }

            listaAsistenciasDeFecha.Add(asistencia);
        }

        public void agregarListaAsistencias(List<Asistencia> asistencias)
        {
            foreach (Asistencia asistencia in asistencias)
            {
                agregarAsistencia(asistencia);
            }
        }

        public List<Asistencia> obtenerAsistenciasDeFecha(DateTime fecha)
        {
            // Esto obtiene la parte de la fecha sin la hora
            String fechaDeBusqueda = fecha.Date.ToString("d");

            return obtenerAsistenciasDeFecha(fechaDeBusqueda);
        }

        public List<Asistencia> obtenerAsistenciasDeFecha(string fecha)
        {
            List<Asistencia> asistenciasDeFecha;

            if (asistenciasPorFecha.TryGetValue(fecha, out asistenciasDeFecha))
            {
                asistenciasDeFecha = asistenciasPorFecha[fecha];
                return asistenciasDeFecha;
            }
            else
            {
                return null;
            }
        }

        public List<Asistencia> obtenerTodasLasAsistencias()
        {
            List<Asistencia> asistencias = new List<Asistencia>(asistenciasPorId.Values);
            return asistencias;
        }

        public bool quitarAsistencia(Asistencia asistencia)
        {
            if (!existeAsistencia(asistencia))
            {
                return false;
            }
            // Se elimina la asistencia del diccionario por id
            asistenciasPorId.Remove(asistencia.Id);

            // Se elimina la asistencia del diccionario por fecha
            string fechaAsistencia = asistencia.DiaDeAsistencia.Date.ToString("d");
            List<Asistencia> asistenciasParaFecha;

            if (asistenciasPorFecha.TryGetValue(fechaAsistencia, out asistenciasParaFecha))
            {
                for (int i = 0; i < asistenciasParaFecha.Count; i++)
                {
                    Asistencia asistenciaEnLista = asistenciasParaFecha.ElementAt(i);

                    if (asistenciaEnLista.Id == asistencia.Id)
                    {
                        asistenciasParaFecha.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public void limpiarDiccionario()
        {
            asistenciasPorId.Clear();
            asistenciasPorFecha.Clear();
        }

        public List<string> getFechasAlmacenadasString()
        {
            List<string> fechas = new List<string>();

            foreach (string fechaKey in asistenciasPorFecha.Keys)
            {
                fechas.Add(fechaKey);
            }

            return fechas;
        }

        public List<DateTime> getFechasAlmacenadasDatetime()
        {
            List<DateTime> fechas = new List<DateTime>();

            foreach (string fechaKey in getFechasAlmacenadasString())
            {
                fechas.Add(DateTime.Parse(fechaKey));
            }

            return fechas;
        }

        private bool existeAsistencia(Asistencia asistencia)
        {
            return asistenciasPorId.ContainsKey(asistencia.Id);
        }
    }
}