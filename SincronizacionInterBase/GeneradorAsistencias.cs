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
        public static List<Asistencia> generarAsistenciasDesdeAppointment(Appointment appointment)
        {
            if (appointment.TipoRepeticion != null)
            {
                int a = 2;
            }
            List<DateTime> fechasAppointment = obtenerFechasDelAppointmentSinConsiderarExcepciones(appointment);
            List<DateTime> excepciones = obtenerFechasExcepcion(appointment);
            List<DateTime> fechasFiltradas = filtrarFechasSegunExcepciones(fechasAppointment, excepciones);

            Asistencia asistenciaPrototipo = obtenerAsistenciaPrototipo(appointment);
            List<Asistencia> asistenciasParaAppointment = new List<Asistencia>();

            foreach (DateTime fechaFiltrada in fechasFiltradas)
            {
                Asistencia asistencia = asistenciaPrototipo.hacerCopiaSuperficial();
                asistencia.Fecha = fechaFiltrada;
                asistenciasParaAppointment.Add(asistencia);
            }
            return asistenciasParaAppointment;
        }

        private static List<DateTime> filtrarFechasSegunExcepciones(List<DateTime> fechas, List<DateTime> excepciones)
        {
            List<DateTime> fechasFiltradas = new List<DateTime>();

            foreach(DateTime fecha in fechas)
            {
                bool fechaValida = true;
                foreach(DateTime excepcion in excepciones)
                {
                    if (fecha == excepcion)
                    {
                        fechaValida = false;
                        break;
                    }
                }

                if (fechaValida)
                {
                    fechasFiltradas.Add(fecha);
                }
            }

            return fechasFiltradas;
        }

        private static List<DateTime> obtenerFechasExcepcion(Appointment appointment)
        {
            List<DateTime> excepciones = new List<DateTime>();

            string stringExcepciones = appointment.Excepciones.Trim();
            if(stringExcepciones == string.Empty) return excepciones;

            string[] excepcionesString = appointment.Excepciones.Split(',');

            foreach (string excepcionString in excepcionesString)
            {
                DateTime excepcion = DateTime.Parse(excepcionString);
                excepciones.Add(excepcion.Date);
            }

            return excepciones;
        }

        private static List<DateTime> obtenerFechasDelAppointmentSinConsiderarExcepciones(Appointment appointment)
        {
            List<DateTime> fechasAppointment = new List<DateTime>();

            int repeticiones = calcularCantidadRepeticiones(appointment);
            int diasEntreRepeticiones = obtenerDiasParaProximaRepeticion(appointment);

            for(int i = 0; i < repeticiones; i++)
            {
                DateTime fechaAppointment = appointment.Inicio;
                fechaAppointment.AddDays(diasEntreRepeticiones * i);
                fechasAppointment.Add(fechaAppointment.Date);
            }

            return fechasAppointment;
        }

        private static int calcularCantidadRepeticiones(Appointment appointment)
        {
            DateTime fechaInicio = appointment.Inicio.Date;
            DateTime fechaFin = appointment.FinRepeticion.Date;

            TimeSpan diferencia = fechaFin.Subtract(fechaInicio);

            int diasEntreRepeticiones = obtenerDiasParaProximaRepeticion(appointment);

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

        private static int obtenerDiasParaProximaRepeticion(Appointment appointment)
        {
            if (appointment.TipoRepeticion == null) return 0;
            if (appointment.TipoRepeticion.ToLower() == "daily") return 1;
            if (appointment.TipoRepeticion.ToLower() == "weekly") return 7;

            return 0;
        }
    }
}
