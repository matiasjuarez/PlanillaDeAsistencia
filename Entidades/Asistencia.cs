﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Entidades
{
    /*
     * Esta clase representa una clase que se dicta en un dia determinado
     */
    [Serializable]
    public class Asistencia
    {
        // La idea es guardar el estado original en esta variable para luego poder saber
        // si la asistencia fue o no modificada
        private AsistenciaMemento estadoGuardado;

        private TimeSpan horaEntradaEsperada;
        private TimeSpan horaSalidaEsperada;
        private TimeSpan horaEntradaReal;
        private TimeSpan horaSalidaReal;
        private DateTime fecha;
        private int cantidadAlumnos;
        private Docente docente;
        private Asignatura asignatura;
        private Personal encargado;
        private Curso curso;
        private EstadoAsistencia estadoAsistencia;
        private List<Aula> aulas;
        private int id;
        private int eventId;    
        private int appointmentId;
        private string observaciones;
        private bool esParcial;

        public Asistencia()
        {
            horaEntradaEsperada = new TimeSpan(0, 0, 0);
            horaSalidaEsperada = new TimeSpan(0, 0, 0);
            horaEntradaReal = new TimeSpan(0, 0, 0);
            horaSalidaReal = new TimeSpan(0, 0, 0);
            docente = new Docente();
            asignatura = new Asignatura();
            encargado = new Personal();
            curso = new Curso();
            estadoAsistencia = new EstadoAsistencia();
            aulas = new List<Aula>();
        }

        public bool EsParcial
        {
            get { return esParcial; }
            set { esParcial = value; }
        }

        public int EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        public int AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
        }

        public String Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime Fecha
        {
            get { return fecha.Date; }
            set { fecha = value.Date; }
        }

        public TimeSpan HoraEntradaEsperada
        {
            get { return horaEntradaEsperada; }
            set { horaEntradaEsperada = value; }
        }

        public TimeSpan HoraSalidaEsperada
        {
            get { return horaSalidaEsperada; }
            set { horaSalidaEsperada = value; }
        }

        public TimeSpan HoraEntradaReal
        {
            get { return horaEntradaReal; }
            set { horaEntradaReal = value; }
        }

        public TimeSpan HoraSalidaReal
        {
            get { return horaSalidaReal; }
            set { horaSalidaReal = value; }
        }
        
        public int CantidadAlumnos
        {
            get {
                if (cantidadAlumnos < 0)
                {
                    return 0;
                }
                return cantidadAlumnos;
            }
            set { cantidadAlumnos = value; }
        }
        
        public Docente Docente
        {
            get { return docente; }
            set { docente = value; }
        }
        
        public Asignatura Asignatura
        {
            get { return asignatura; }
            set { asignatura = value; }
        }
        
        public Personal Encargado
        {
            get { return encargado; }
            set { encargado = value; }
        }
        
        public Curso Curso
        {
            get { return curso; }
            set { curso = value; }
        }
        
        public EstadoAsistencia EstadoAsistencia
        {
            get { return estadoAsistencia; }
            set { estadoAsistencia = value; }
        }
        
        public List<Aula> Aulas
        {
            get { return aulas; }
            set { aulas = value; }
        }

        public void agregarAula(Aula aula)
        {
            if (aulas == null)
            {
                aulas = new List<Aula>();
            }

            if (!aulas.Contains(aula))
            {
                aulas.Add(aula);
            }
        }

        public DateTime obtenerEntradaEsperada()
        {
            return this.fecha.Add(this.horaEntradaEsperada);
        }

        public DateTime obtenerSalidaEsperada()
        {
            return this.fecha.Add(this.horaSalidaEsperada);
        }

        public DateTime obtenerEntradaReal()
        {
            return this.fecha.Add(this.horaEntradaReal);
        }

        public DateTime obtenerSalidaReal()
        {
            return this.fecha.Add(this.horaSalidaReal);
        }

        public bool EqualsEstadoInicial(Asistencia otra)
        {
            Asistencia aux = new Asistencia();
            estadoGuardado.restaurarEstado(aux);

            return aux.Equals(otra);
        }

        public bool Equals(Asistencia otra)
        {
            if (otra == null) return false;

            if (this.Id != otra.Id) return false;

            if (this.EventId != otra.EventId) return false;

            if (this.AppointmentId != otra.AppointmentId) return false;

            if (this.HoraEntradaEsperada != otra.HoraEntradaEsperada) return false;

            if (this.HoraSalidaEsperada != otra.HoraSalidaEsperada) return false;

            if(this.HoraEntradaReal != otra.HoraEntradaReal) return false;

            if (this.HoraSalidaReal != otra.HoraSalidaReal) return false;

            if (this.CantidadAlumnos != otra.CantidadAlumnos) return false;

            if (!this.Docente.Equals(otra.Docente)) return false;

            if (!this.Asignatura.Equals(otra.Asignatura)) return false;

            if (!this.Curso.Equals(otra.Curso)) return false;

            if (!this.EstadoAsistencia.Equals(otra.EstadoAsistencia)) return false;

            if (this.Observaciones != otra.Observaciones) return false;

            if (this.Aulas.Count != otra.Aulas.Count) return false;
            foreach (Aula aulaO in this.Aulas)
            {
                bool seEncontroAulaIgual = false;
                foreach (Aula aulaC in otra.Aulas)
                {
                    if (aulaO.Equals(aulaC))
                    {
                        seEncontroAulaIgual = true;
                        break;
                    }
                }

                if (!seEncontroAulaIgual) return false;
            }

            return true;
        }

        public void guardarEstado()
        {
            estadoGuardado = new AsistenciaMemento(this);
        }

        public bool esModificada()
        {
            Asistencia aux = new Asistencia();
            estadoGuardado.restaurarEstado(aux);

            return !aux.Equals(this);
        }

        public bool esSinHoraEntradaReal_PostHoraEntradaEsperada()
        {
            DateTime fechaHoraActual = DateTime.Now;

            if (fechaHoraActual >= obtenerEntradaEsperada())
            {
                if (HoraEntradaReal.Equals(new TimeSpan(0, 0, 0)))
                {
                    return true;
                }
            }

            return false;
        }

        public bool esSinHoraSalidaReal_PostHoraSalidaEsperada()
        {
            DateTime fechaHoraActual = DateTime.Now;

            if (fechaHoraActual >= obtenerSalidaEsperada())
            {
                if (HoraSalidaReal.Equals(new TimeSpan(0, 0, 0)))
                {
                    return true;
                }
            }

            return false;
        }

        public Asistencia Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    object miObjeto = formatter.Deserialize(stream);
                    return (Asistencia)miObjeto;
                }
                return null;
            }
        }

        public Asistencia cloneSuperficial()
        {
            Asistencia copia = new Asistencia();

            copia.HoraEntradaEsperada = this.HoraEntradaEsperada;
            copia.HoraSalidaEsperada = this.HoraSalidaEsperada;
            copia.HoraEntradaReal = this.HoraEntradaReal;
            copia.HoraSalidaReal = this.HoraSalidaReal;
            copia.Fecha = this.Fecha;
            copia.CantidadAlumnos = this.CantidadAlumnos;
            copia.Docente = this.Docente;
            copia.Asignatura = this.Asignatura;
            copia.Encargado = this.Encargado;
            copia.Curso = this.Curso;
            copia.EstadoAsistencia = this.EstadoAsistencia;
            copia.Aulas = this.Aulas;
            copia.Observaciones = this.Observaciones;
            copia.Id = this.Id;
            copia.AppointmentId = this.AppointmentId;
            copia.EventId = this.EventId;

            return copia;
        }
    }
}