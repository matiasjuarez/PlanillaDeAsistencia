using System;
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

        private TimeSpan comienzoClaseEsperado;
        private TimeSpan finClaseEsperado;
        private TimeSpan comienzoClaseReal;
        private TimeSpan finClaseReal;
        private DateTime diaDeAsistencia;
        private int cantidadAlumnos;
        private Docente docente;
        private Asignatura asignatura;
        private Encargado encargado;
        private Curso curso;
        private EstadoAsistencia estadoAsistencia;
        private List<Aula> aulas;
        private int id;
        private int eventId;    
        private int appointmentId;
        private string observaciones;
        private bool comienzoClaseEsperadoSeteado = false;
        private bool finClaseEsperadoSeteado = false;

        public Asistencia()
        {
            comienzoClaseEsperado = new TimeSpan(0, 0, 0);
            finClaseEsperado = new TimeSpan(0, 0, 0);
            comienzoClaseReal = new TimeSpan(0, 0, 0);
            finClaseReal = new TimeSpan(0, 0, 0);
            docente = new Docente();
            asignatura = new Asignatura();
            encargado = new Encargado();
            curso = new Curso();
            estadoAsistencia = new EstadoAsistencia();
            aulas = new List<Aula>();
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

        public DateTime DiaDeAsistencia
        {
            get { return diaDeAsistencia; }
            set { diaDeAsistencia = value; }
        }

        public TimeSpan ComienzoClaseEsperado
        {
            get { return comienzoClaseEsperado; }
            set { comienzoClaseEsperado = value; }
        }

        public TimeSpan FinClaseEsperado
        {
            get { return finClaseEsperado; }
            set { finClaseEsperado = value; }
        }

        public TimeSpan ComienzoClaseReal
        {
            get { return comienzoClaseReal; }
            set { comienzoClaseReal = value; }
        }

        public TimeSpan FinClaseReal
        {
            get { return finClaseReal; }
            set { finClaseReal = value; }
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
        
        public Encargado Encargado
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

        public bool Equals(Asistencia otra)
        {
            if (otra == null) return false;

            if (this.Id != otra.Id) return false;

            if (this.EventId != otra.EventId) return false;

            if (this.AppointmentId != otra.AppointmentId) return false;

            if (this.ComienzoClaseEsperado != otra.ComienzoClaseEsperado) return false;

            if (this.FinClaseEsperado != otra.FinClaseEsperado) return false;

            if(this.ComienzoClaseReal != otra.ComienzoClaseReal) return false;

            if (this.FinClaseReal != otra.FinClaseReal) return false;

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

        public bool estaModificada()
        {
            Asistencia aux = new Asistencia();
            estadoGuardado.restaurarEstado(aux);

            return aux.Equals(this);
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

        private class AsistenciaMemento
        {
            private TimeSpan comienzoClaseEsperado;
            private TimeSpan finClaseEsperado;
            private TimeSpan comienzoClaseReal;
            private TimeSpan finClaseReal;
            private DateTime diaDeAsistencia;
            private int cantidadAlumnos;
            private Docente docente;
            private Asignatura asignatura;
            private Encargado encargado;
            private Curso curso;
            private EstadoAsistencia estadoAsistencia;
            private List<Aula> aulas;
            private string observaciones;

            public AsistenciaMemento(Asistencia asistencia)
            {
                Asistencia clon = asistencia.Clone();

                this.comienzoClaseEsperado = clon.ComienzoClaseEsperado;
                this.finClaseEsperado = clon.FinClaseEsperado;
                this.comienzoClaseReal = clon.ComienzoClaseReal;
                this.finClaseReal = clon.FinClaseReal;
                this.diaDeAsistencia = clon.DiaDeAsistencia;
                this.cantidadAlumnos = clon.CantidadAlumnos;
                this.docente = clon.Docente;
                this.asignatura = clon.Asignatura;
                this.encargado = clon.Encargado;
                this.curso = clon.Curso;
                this.estadoAsistencia = clon.EstadoAsistencia;
                this.aulas = clon.Aulas;
                this.observaciones = clon.Observaciones;
            }

            public void restaurarEstado(Asistencia asistencia)
            {
                asistencia.ComienzoClaseEsperado = this.comienzoClaseEsperado;
                asistencia.FinClaseEsperado = this.finClaseEsperado;
                asistencia.ComienzoClaseReal = this.comienzoClaseReal;
                asistencia.FinClaseReal = this.finClaseReal;
                asistencia.DiaDeAsistencia = this.diaDeAsistencia;
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
}