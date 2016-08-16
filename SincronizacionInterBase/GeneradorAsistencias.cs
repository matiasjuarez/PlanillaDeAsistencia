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
        private static ContenedorDatosSoporte contenedorDatosSoporte = ContenedorDatosSoporte.getInstance();

        public static List<Asistencia> generarAsistenciasDesdeAppointment(Appointment appointment)
        {
            List<Asistencia> asistencias;

            asistencias = obtenerAsistenciasSinConsiderarExcepciones(appointment);
        }

        private static List<Asistencia> filtrarAsistenciasSegunExcepciones(List<Asistencia> asistencias, List<DateTime> excepciones)
        {
            List<Asistencia> asistenciasFiltradas = new List<Asistencia>();

            for (int i = 0; i < asistencias.Count; i++)
            {
                bool hayQueFiltrar = false;
                foreach(DateTime excepcion in excepciones)
                {
                    if (asistencias.ElementAt<Asistencia>(i).Fecha == excepcion)
                    {
                        hayQueFiltrar = true;
                        excepciones.Remove(excepcion);
                        break;
                    }

                    if (!hayQueFiltrar)
                    {
                        asistenciasFiltradas.Add(asistencias.ElementAt(i));
                    }
                }
            }
        }

        private static List<DateTime> obtenerListaExcepciones(Appointment appointment)
        {
            string[] excepcionesString = appointment.Excepciones.Split(',');
            List<DateTime> excepciones = new List<DateTime>();

            foreach (string excepcionString in excepcionesString)
            {
                DateTime excepcion = DateTime.Parse(excepcionString);
                excepciones.Add(excepcion.Date);
            }

            return excepciones;
        }

        private static List<Asistencia> obtenerAsistenciasSinConsiderarExcepciones(Appointment appointment)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            int diasEntreRepeticiones = obtenerDiasParaProximaRepeticion(appointment);

            int cantidadRepeticiones = appointment.CantidadRepeticiones;

            int i = 0;

            do
            {
                Asistencia asistenciaNueva = obtenerAsistenciaDesdeAppointment(appointment, diasEntreRepeticiones * i);
                asistencias.Add(asistenciaNueva);

                i++;
            } while (i < cantidadRepeticiones);

            return asistencias;
        }

        private static Asistencia obtenerAsistenciaDesdeAppointment(Appointment appointment, int diasAgregar)
        {
            Asistencia asistencia = new Asistencia();

            asistencia.AppointmentId = appointment.AppointmentId;
            asistencia.EventId = appointment.IDEvento;

            asistencia.HoraEntradaEsperada = appointment.Inicio.TimeOfDay;
            asistencia.HoraSalidaEsperada = appointment.Fin.TimeOfDay;
            asistencia.Fecha = appointment.Inicio.AddDays(diasAgregar);

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
