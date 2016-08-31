using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class AsistenciaMemento
    {
        private TimeSpan horaEntradaEsperada;
        private TimeSpan horaSalidaEsperada;
        private TimeSpan horaEntradaReal;
        private TimeSpan horaSalidaReal;
        private DateTime fecha;
        private int cantidadAlumnos;
        private Docente docente;
        private Asignatura asignatura;
        private Encargado encargado;
        private Curso curso;
        private EstadoAsistencia estadoAsistencia;
        private List<Aula> aulas;
        private string observaciones;
        private int id;
        private int appointmentId;
        private int eventId;

        public AsistenciaMemento(Asistencia asistencia)
        {
            Asistencia clon = asistencia.Clone();

            this.horaEntradaEsperada = clon.HoraEntradaEsperada;
            this.horaSalidaEsperada = clon.HoraSalidaEsperada;
            this.horaEntradaReal = clon.HoraEntradaReal;
            this.horaSalidaReal = clon.HoraSalidaReal;
            this.fecha = clon.Fecha;
            this.cantidadAlumnos = clon.CantidadAlumnos;
            this.docente = clon.Docente;
            this.asignatura = clon.Asignatura;
            this.encargado = clon.Encargado;
            this.curso = clon.Curso;
            this.estadoAsistencia = clon.EstadoAsistencia;
            this.aulas = clon.Aulas;
            this.observaciones = clon.Observaciones;
            this.id = clon.Id;
            this.appointmentId = clon.AppointmentId;
            this.eventId = clon.EventId;
        }

        public void restaurarEstado(Asistencia asistencia)
        {
            asistencia.Id = this.id;
            asistencia.AppointmentId = this.appointmentId;
            asistencia.EventId = this.eventId;
            asistencia.HoraEntradaEsperada = this.horaEntradaEsperada;
            asistencia.HoraSalidaEsperada = this.horaSalidaEsperada;
            asistencia.HoraEntradaReal = this.horaEntradaReal;
            asistencia.HoraSalidaReal = this.horaSalidaReal;
            asistencia.Fecha = this.fecha;
            asistencia.CantidadAlumnos = this.cantidadAlumnos;
            asistencia.Docente = this.docente;
            asistencia.Asignatura = this.asignatura;
            asistencia.Encargado = this.encargado;
            asistencia.Curso = this.curso;
            asistencia.EstadoAsistencia = this.estadoAsistencia;
            asistencia.Aulas = this.aulas;
            asistencia.Observaciones = this.observaciones;
        }
    }
}