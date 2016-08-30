using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace SincronizacionInterBase
{
    public class GeneradorAsistencias
    {
        private DateTime fechaDesde;
        private DateTime fechaHasta;

        public GeneradorAsistencias(DateTime fechaDesde, DateTime fechaHasta)
        {
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
        }

        public List<Asistencia> generarAsistenciasDesdeAppointment(Appointment appointment)
        {
            List<DateTime> fechasAppointment = obtenerFechasParaAppointment(appointment);

            Asistencia asistenciaPrototipo = obtenerAsistenciaPrototipo(appointment);

            List<Asistencia> asistenciasParaAppointment = new List<Asistencia>();
            foreach (DateTime fecha in fechasAppointment)
            {
                Asistencia asistencia = asistenciaPrototipo.hacerCopiaSuperficial();
                asistencia.Fecha = fecha;
                asistenciasParaAppointment.Add(asistencia);
            }
            return asistenciasParaAppointment;
        }

        private List<DateTime> obtenerFechasParaAppointment(Appointment appointment)
        {
            // Obtenemos las fechas en las que el appointment se deberia repetir******************
            List<DateTime> fechasAppointment = new List<DateTime>();

            int repeticiones = calcularCantidadRepeticiones(appointment);
            int diasEntreRepeticiones = obtenerDiasEntreRepeticiones(appointment);

            for (int i = 0; i < repeticiones; i++)
            {
                DateTime fechaAppointment = appointment.Inicio;
                fechaAppointment = fechaAppointment.AddDays(diasEntreRepeticiones * i);

                // Agregamos esta fecha unicamente si se encuentra entre los limites definidos por fechaDesde y fechaHasta
                if (fechaAppointment >= this.fechaDesde && fechaAppointment <= this.fechaHasta)
                {
                    fechasAppointment.Add(fechaAppointment.Date);
                }
            }

            // Obtenemos las excepciones para el appointment***************************************
            List<DateTime> excepciones = new List<DateTime>();

            string stringExcepciones = appointment.Excepciones;
            if (stringExcepciones != string.Empty && stringExcepciones != null)
            {
                stringExcepciones = stringExcepciones.Trim();

                string[] excepcionesString = appointment.Excepciones.Split(',');

                foreach (string excepcionString in excepcionesString)
                {
                    DateTime excepcion = DateTime.Parse(excepcionString);
                    excepciones.Add(excepcion.Date);
                }
            }

            // Filtramos las fechas segun las excepciones obtenidas **********************
            HashSet<DateTime> fechasFiltradas = new HashSet<DateTime>();

            foreach (DateTime fecha in fechasAppointment)
            {
                fechasFiltradas.Add(fecha);
            }

            foreach (DateTime excepcion in excepciones)
            {
                fechasFiltradas.Remove(excepcion);
            }

            return fechasFiltradas.ToList<DateTime>();
        }

        private static int calcularCantidadRepeticiones(Appointment appointment)
        {
            DateTime fechaInicio = appointment.Inicio.Date;
            DateTime fechaFin = appointment.FinRepeticion.Date;

            TimeSpan diferencia = fechaFin.Subtract(fechaInicio);

            int diasEntreRepeticiones = obtenerDiasEntreRepeticiones(appointment);

            int cantidadRepeticiones = 0;
            if(diasEntreRepeticiones != 0) cantidadRepeticiones = diferencia.Days / diasEntreRepeticiones;

            /*
             * Si tenemos que un appointment ocurre el 12/04 y tiene una repeticion el 19/04, cuando hagamos
             * las operaciones de arriba nos queda una diferencia de 7 dias y al dividirlo entre la cantidad de
             * dias entre repeticiones (7) nos d igual a 1. Esto es porque en las operaciones de arriba no se tiene
             * en cuenta el dias 0 cuando arranca el appointment. Por eso se suma 1 en el return.
             * */
            return cantidadRepeticiones + 1;
        }

        private static int obtenerDiasEntreRepeticiones(Appointment appointment)
        {
            if (appointment.TipoRepeticion == null) return 0;
            if (appointment.TipoRepeticion.ToLower() == "daily") return 1;
            if (appointment.TipoRepeticion.ToLower() == "weekly") return 7;

            return 0;
        }

        private static Asistencia obtenerAsistenciaPrototipo(Appointment appointment)
        {
            ContenedorDatosSoporte contenedorDatosSoporte = ContenedorDatosSoporte.getInstance();

            Asistencia asistencia = new Asistencia();

            asistencia.AppointmentId = appointment.AppointmentId;
            asistencia.EventId = appointment.IDEvento;

            asistencia.HoraEntradaEsperada = appointment.Inicio.TimeOfDay;
            asistencia.HoraSalidaEsperada = appointment.Fin.TimeOfDay;
            asistencia.Fecha = appointment.Inicio;

            foreach (Docente docente in contenedorDatosSoporte.obtenerDocentes())
            {
                if (docente.Nombre == appointment.Docente)
                {
                    asistencia.Docente = docente;
                    break;
                }
            }

            foreach (Asignatura asignatura in contenedorDatosSoporte.obtenerAsignaturas())
            {
                if (asignatura.Nombre == appointment.Asignatura)
                {
                    asistencia.Asignatura = asignatura;
                    break;
                }
            }

            if (appointment.Aulas != null)
            {
                string[] aulasNombresRapla = appointment.Aulas.Split(',');
                foreach (string aulaNombreRapla in aulasNombresRapla)
                {
                    foreach (Aula aula in contenedorDatosSoporte.obtenerAulas())
                    {
                        if (aula.Nombre == aulaNombreRapla)
                        {
                            asistencia.agregarAula(aula);
                            break;
                        }
                    }
                }
            }
            
            foreach (Curso curso in contenedorDatosSoporte.obtenerCursos())
            {
                if (curso.Nombre == appointment.Curso)
                {
                    asistencia.Curso = curso;
                    break;
                }
            }

            return asistencia;
        }
    }
}