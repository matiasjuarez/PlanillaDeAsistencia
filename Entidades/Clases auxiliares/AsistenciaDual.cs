using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class AsistenciaDual
    {
        private const int ESTADO_SIN_MODIFICAR = 0;
        private const int ESTADO_MODIFICADA = 1;

        private Asistencia original;
        private Asistencia clonada;
        private int estado;

        public AsistenciaDual(Asistencia asistencia)
        {
            original = asistencia;
            clonada = asistencia.Clone();
        }

        public Asistencia Original
        {
            get
            {
                return original;
            }
            set
            {
                original = value;
                clonada = original.Clone();
            }
        }

        public Asistencia Clonada
        {
            get
            {
                return clonada;
            }
        }

        public bool esModificada()
        {
            return !original.poseeLosMismosDatosQueEstaAsistencia(clonada);
        }

        public int EventId
        {
            get { return clonada.EventId; }
            set { clonada.EventId = value; }
        }

        public int AppointmentId
        {
            get { return clonada.AppointmentId; }
            set { clonada.AppointmentId = value; }
        }

        public String Observaciones
        {
            get { return clonada.Observaciones; }
            set { clonada.Observaciones = value; }
        }

        public int Id
        {
            get { return clonada.Id; }
            set { clonada.Id = value; }
        }

        public DateTime DiaDeAsistencia
        {
            get { return clonada.DiaDeAsistencia; }
            set { clonada.DiaDeAsistencia = value; }
        }

        public TimeSpan ComienzoClaseEsperado
        {
            get { return clonada.ComienzoClaseEsperado; }
            set { clonada.ComienzoClaseEsperado = value; }
        }

        public TimeSpan FinClaseEsperado
        {
            get { return clonada.FinClaseEsperado; }
            set { clonada.FinClaseEsperado = value; }
        }

        public TimeSpan ComienzoClaseReal
        {
            get { return clonada.ComienzoClaseReal; }
            set { clonada.ComienzoClaseReal = value; }
        }

        public TimeSpan FinClaseReal
        {
            get { return clonada.FinClaseReal; }
            set { clonada.FinClaseReal = value; }
        }

        public int CantidadAlumnos
        {
            get
            {
                if (clonada.CantidadAlumnos < 0)
                {
                    return 0;
                }
                return clonada.CantidadAlumnos;
            }
            set { clonada.CantidadAlumnos = value; }
        }

        public Docente Docente
        {
            get
            {
                return clonada.Docente;
            }
            set { clonada.Docente = value; }
        }

        public Asignatura Asignatura
        {
            get
            {
                return clonada.Asignatura;
            }
            set { clonada.Asignatura = value; }
        }

        public Encargado Encargado
        {
            get
            {
                return clonada.Encargado;
            }
            set { clonada.Encargado = value; }
        }

        public Curso Curso
        {
            get
            {
                return clonada.Curso;
            }
            set { clonada.Curso = value; }
        }

        public EstadoAsistencia EstadoAsistencia
        {
            get
            {
                return clonada.EstadoAsistencia;
            }
            set { clonada.EstadoAsistencia = value; }
        }

        public List<Aula> Aulas
        {
            get { return clonada.Aulas; }
            set { clonada.Aulas = value; }
        }

        public void agregarAula(Aula aula)
        {
            if (clonada.Aulas == null)
            {
                clonada.Aulas = new List<Aula>();
            }

            if (!clonada.Aulas.Contains(aula))
            {
                clonada.Aulas.Add(aula);
            }
        }

        public bool poseeLosMismosDatosQueEstaAsistencia(Asistencia otra)
        {
            return this.clonada.poseeLosMismosDatosQueEstaAsistencia(otra);
        }

        public bool poseeLosMismosDatosQueEstaAsistencia(AsistenciaDual otra)
        {
            return poseeLosMismosDatosQueEstaAsistencia(otra.clonada);
        }

        public bool poseeLosMismosDatosOriginalesQueEstaAsistencia(Asistencia otra)
        {
            return this.original.poseeLosMismosDatosQueEstaAsistencia(otra);
        }

        public bool poseeLosMismosDatosOriginalesQueEstaAsistencia(AsistenciaDual otra)
        {
            return poseeLosMismosDatosQueEstaAsistencia(otra.original);
        }
    }
}
