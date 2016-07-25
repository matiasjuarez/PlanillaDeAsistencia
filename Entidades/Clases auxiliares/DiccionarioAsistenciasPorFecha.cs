using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class DiccionarioAsistenciasPorFecha
    {
        private Dictionary<string, List<Asistencia>> diccionarioAsistencias;

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
            diccionarioAsistencias = new Dictionary<string, List<Asistencia>>();
        }

        public void agregarListAsistencias(List<Asistencia> listaAsistencias, bool comprobarAsistenciaNoRepetida = true)
        {
            foreach (Asistencia asistencia in listaAsistencias)
            {
                agregarAsistencia(asistencia, comprobarAsistenciaNoRepetida);
            }
        }

        public void agregarAsistencia(Asistencia asistencia, bool comprobarAsistenciaNoRepetida = true)
        {
            // Se agrega al diccionario por fechas
            string fechaAsistencia = asistencia.ComienzoClaseEsperado.Date.ToString("d");
            List<Asistencia> asistenciasParaFecha;

            if (!diccionarioAsistencias.TryGetValue(fechaAsistencia, out asistenciasParaFecha))
            {
                asistenciasParaFecha = new List<Asistencia>();
                diccionarioAsistencias[fechaAsistencia] = asistenciasParaFecha;
            }

            if (comprobarAsistenciaNoRepetida)
            {
                if (!asistenciasParaFecha.Contains(asistencia))
                {
                    asistenciasParaFecha.Add(asistencia);
                }
            }
            else
            {
                asistenciasParaFecha.Add(asistencia);
            }
        }

        public List<Asistencia> obtenerAsistenciasParaFecha(DateTime fecha)
        {
            // Esto obtiene la parte de la fecha sin la hora
            String fechaDeBusqueda = fecha.Date.ToString("d");

            return obtenerAsistenciasParaFecha(fechaDeBusqueda);
        }

        public List<Asistencia> obtenerAsistenciasParaFecha(string fecha)
        {
            List<Asistencia> asistenciasDeFecha;

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

        public void setearEntrada(DateTime fecha, List<Asistencia> asistencias)
        {
            String fechaDeBusqueda = fecha.Date.ToString("d");
            setearEntrada(fechaDeBusqueda, asistencias);
        }

        public void setearEntrada(string fecha, List<Asistencia> asistencias)
        {
            diccionarioAsistencias[fecha] = asistencias;
        }

        public bool quitarAsistencia(Asistencia asistencia)
        {
            string fechaAsistencia = asistencia.ComienzoClaseEsperado.Date.ToString("d");
            List<Asistencia> asistenciasParaFecha;

            if (diccionarioAsistencias.TryGetValue(fechaAsistencia, out asistenciasParaFecha))
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
