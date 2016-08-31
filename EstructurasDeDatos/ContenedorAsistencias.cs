using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;

namespace ContenedoresDeDatos
{
    public class ContenedorAsistencias : Contenedor<int, Asistencia>
    {
        public List<Asistencia> obtenerAsistenciasDeFecha(DateTime fecha)
        {
            List<Asistencia> listaAsistenciasDeFecha = new List<Asistencia>();

            foreach (Asistencia asistencia in obtenerDatos())
            {
                //string fechaAsistencia = asistencia.DiaDeAsistencia.Date.ToString("d");

                if (asistencia.Fecha.Equals(fecha.Date))
                {
                    listaAsistenciasDeFecha.Add(asistencia);
                }
            }
            return listaAsistenciasDeFecha;
        }

        public Dictionary<DateTime, List<Asistencia>> obtenerAsistenciasAgrupadasPorFecha()
        {
            Dictionary<DateTime, List<Asistencia>> asistencias = new Dictionary<DateTime, List<Asistencia>>();

            foreach (Asistencia asistencia in obtenerDatos())
            {
                //string fechaAsistencia = asistencia.DiaDeAsistencia.Date.ToString("d");
                DateTime fechaAsistencia = asistencia.Fecha;

                List<Asistencia> listaAsistenciasDeFecha;

                if (!asistencias.TryGetValue(fechaAsistencia, out listaAsistenciasDeFecha))
                {
                    listaAsistenciasDeFecha = new List<Asistencia>();
                    asistencias.Add(fechaAsistencia, listaAsistenciasDeFecha);
                }

                listaAsistenciasDeFecha.Add(asistencia);
            }

            return asistencias;
        }
        
        public List<Asistencia> obtenerAsistenciasPorAppointment(Appointment appointment)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            foreach (Asistencia asistencia in obtenerDatos())
            {
                if (asistencia.EventId == appointment.IDEvento && asistencia.AppointmentId == appointment.AppointmentId)
                {
                    asistencias.Add(asistencia);
                }
            }
            return asistencias;
        }

        public List<DateTime> obtenerFechasDeAsistenciasAlmacenadas()
        {
            HashSet<DateTime> fechas = new HashSet<DateTime>();

            foreach (Asistencia asistencia in obtenerDatos())
            {
                fechas.Add(asistencia.Fecha);
            }

            return fechas.ToList<DateTime>();
        }

        public override void refrescarDatos()
        {
            List<DateTime> fechasDeAsistencias = obtenerFechasDeAsistenciasAlmacenadas();

            List<Asistencia> asistenciasBaseDatos = DAOAsistencias.obtenerAsistenciasDeFechas(fechasDeAsistencias);

            limpiarContenedor();

            foreach (Asistencia asistencia in asistenciasBaseDatos)
            {
                guardarDato(asistencia.Id, asistencia);
            }
        }
    }
}