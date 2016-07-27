using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class DiccionarioAsistenciasPorFecha
    {
        private Dictionary<string, List<AsistenciaDual>> diccionarioAsistencias;

        public int Count
        {
            get 
            {
                int acumulado = 0;

                foreach (string fecha in getFechasAlmacenadasComoString())
                {
                    acumulado += diccionarioAsistencias[fecha].Count;
                }

                return acumulado;
            }
        }

        public DiccionarioAsistenciasPorFecha()
        {
            diccionarioAsistencias = new Dictionary<string, List<AsistenciaDual>>();
        }

        public void agregarListAsistencias(List<AsistenciaDual> listaAsistencias, bool comprobarAsistenciaNoRepetida = true)
        {
            foreach (AsistenciaDual asistencia in listaAsistencias)
            {
                agregarAsistencia(asistencia, comprobarAsistenciaNoRepetida);
            }
        }

        public void agregarListAsistencias(List<Asistencia> listaAsistencias, bool comprobarAsistenciaNoRepetida = true)
        {
            List<AsistenciaDual> asistenciasDuales = new List<AsistenciaDual>();
            foreach (Asistencia asistencia in listaAsistencias)
            {
                AsistenciaDual asistenciaD = new AsistenciaDual(asistencia);
                asistenciasDuales.Add(asistenciaD);
            }
            agregarListAsistencias(asistenciasDuales, comprobarAsistenciaNoRepetida);
        } 

        public void agregarAsistencia(AsistenciaDual asistencia, bool comprobarAsistenciaNoRepetida = true)
        {
            // Se agrega al diccionario por fechas
            string fechaAsistencia = asistencia.Original.DiaDeAsistencia.Date.ToString("d");
            List<AsistenciaDual> asistenciasParaFecha;

            if (!diccionarioAsistencias.TryGetValue(fechaAsistencia, out asistenciasParaFecha))
            {
                asistenciasParaFecha = new List<AsistenciaDual>();
                diccionarioAsistencias[fechaAsistencia] = asistenciasParaFecha;
            }

            if (comprobarAsistenciaNoRepetida)
            {
                if (!contieneAsistencia(asistencia, asistenciasParaFecha))
                {
                    asistenciasParaFecha.Add(asistencia);
                }
            }
            else
            {
                asistenciasParaFecha.Add(asistencia);
            }
        }

        public void agregarAsistencia(Asistencia asistencia)
        {
            AsistenciaDual asistenciaD = new AsistenciaDual(asistencia);
            agregarAsistencia(asistenciaD);
        }

        private bool contieneAsistencia(AsistenciaDual asistencia, List<AsistenciaDual> lista)
        {
            if (asistencia == null) return false;

            foreach (AsistenciaDual asistenciaD in lista)
            {
                if (asistencia.Original.Id == asistenciaD.Original.Id)
                {
                    return true;
                }
            }

            return false;
        }

        public List<AsistenciaDual> obtenerAsistenciasParaFecha(DateTime fecha)
        {
            // Esto obtiene la parte de la fecha sin la hora
            String fechaDeBusqueda = fecha.Date.ToString("d");

            return obtenerAsistenciasParaFecha(fechaDeBusqueda);
        }

        public List<AsistenciaDual> obtenerAsistenciasParaFecha(string fecha)
        {
            List<AsistenciaDual> asistenciasDeFecha;

            if (diccionarioAsistencias.TryGetValue(fecha, out asistenciasDeFecha))
            {
                asistenciasDeFecha = diccionarioAsistencias[fecha];
                return asistenciasDeFecha;
            }
            else
            {
                return null;
            }
        }

        public void setearEntrada(DateTime fecha, List<AsistenciaDual> asistencias)
        {
            String fechaDeBusqueda = fecha.Date.ToString("d");
            setearEntrada(fechaDeBusqueda, asistencias);
        }

        public void setearEntrada(string fecha, List<AsistenciaDual> asistencias)
        {
            diccionarioAsistencias[fecha] = asistencias;
        }

        public bool quitarAsistencia(Asistencia asistencia)
        {
            string fechaAsistencia = asistencia.DiaDeAsistencia.Date.ToString("d");
            List<AsistenciaDual> asistenciasParaFecha;

            if (diccionarioAsistencias.TryGetValue(fechaAsistencia, out asistenciasParaFecha))
            {
                for (int i = 0; i < asistenciasParaFecha.Count; i++)
                {
                    AsistenciaDual asistenciaEnLista = asistenciasParaFecha.ElementAt(i);

                    if (asistenciaEnLista.Original.Id == asistencia.Id)
                    {
                        asistenciasParaFecha.RemoveAt(i);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool quitarAsistencia(AsistenciaDual asistencia)
        {
            return quitarAsistencia(asistencia.Original);
        }

        public void limpiarDiccionario()
        {
            diccionarioAsistencias.Clear();
        }

        public List<string> getFechasAlmacenadasComoString()
        {
            List<string> fechas = new List<string>();

            foreach (string fechaKey in diccionarioAsistencias.Keys)
            {
                fechas.Add(fechaKey);
            }

            return fechas;
        }

        public List<DateTime> getFechasAlmacenadasComoDateTime()
        {
            List<DateTime> fechas = new List<DateTime>();

            foreach (string fechaKey in getFechasAlmacenadasComoString())
            {
                fechas.Add(DateTime.Parse(fechaKey));
            }

            return fechas;
        }
    }
}
