using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Entidades
{
    /*
     * Esta case representa cada uno de los cursos que se van a dictar
     * en un dia determinado
     */
    [Serializable]
    public class Asistencia
    {
        private DateTime comienzoClaseEsperado;
        private DateTime finClaseEsperado;
        private DateTime comienzoClaseReal;
        private DateTime finClaseReal;
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
        private String observaciones;

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

        public DateTime ComienzoClaseEsperado
        {
            get { return comienzoClaseEsperado; }
            set { comienzoClaseEsperado = value; }
        }
        
        public DateTime FinClaseEsperado
        {
            get { return finClaseEsperado; }
            set { finClaseEsperado = value; }
        }

        public DateTime ComienzoClaseReal
        {
            get { return comienzoClaseReal; }
            set { comienzoClaseReal = value; }
        }
        
        public DateTime FinClaseReal
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
            get 
            {
                /*if (docente == null)
                {
                    Docente inventado = new Docente();
                    inventado.Nombre = config.DefectoStringNull;
                    inventado.Id = config.DefectoIntNull;
                    return inventado;
                }*/
                return docente; 
            }
            set { docente = value; }
        }
        
        public Asignatura Asignatura
        {
            get 
            {
                /*if (asignatura == null)
                {
                    Asignatura inventada = new Asignatura();
                    inventada.Nombre = config.DefectoStringNull;
                    inventada.Id = config.DefectoIntNull;
                    return inventada;
                }*/
                return asignatura; 
            }
            set { asignatura = value; }
        }
        
        public Encargado Encargado
        {
            get 
            {
                /*if (encargado == null)
                {
                    Encargado inventado = new Encargado();
                    inventado.Nombre = config.DefectoStringNull;
                    inventado.Id = config.DefectoIntNull;
                    return inventado;
                }*/
                return encargado; 
            }
            set { encargado = value; }
        }
        
        public Curso Curso
        {
            get 
            {
                /*if (curso == null)
                {
                    Curso inventado = new Curso();
                    inventado.Nombre = config.DefectoStringNull;
                    inventado.Id = config.DefectoIntNull;
                    return inventado;
                }*/
                return curso; 
            }
            set { curso = value; }
        }
        
        public EstadoAsistencia EstadoAsistencia
        {
            get 
            {
                /*if (estadoAsistencia == null)
                {
                    EstadoAsistencia inventada = new EstadoAsistencia();
                    inventada.Nombre = config.DefectoStringNull;
                    inventada.Id = config.DefectoIntNull;
                    return inventada;
                }*/
                return estadoAsistencia; 
            }
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

        public bool poseeLosMismosDatosQueEstaAsistencia(Asistencia otra)
        {
            if (otra == null)
            {
                return false;
            }

            if (this.Id != otra.Id)
            {
                return false;
            }

            if (this.ComienzoClaseEsperado != otra.ComienzoClaseEsperado)
            {
                return false;
            }

            if (this.FinClaseEsperado != otra.FinClaseEsperado)
            {
                return false;
            }

            if(this.ComienzoClaseReal != otra.ComienzoClaseReal)
            {
                return false;
            }

            if (this.FinClaseReal != otra.FinClaseReal)
            {
                return false;
            }

            if (this.CantidadAlumnos != otra.CantidadAlumnos)
            {
                return false;
            }

            if (!this.Docente.Equals(otra.Docente))
            {
                return false;
            }

            if (!this.Asignatura.Equals(otra.Asignatura))
            {
                return false;
            }

            if (!this.Curso.Equals(otra.Curso))
            {
                return false;
            }

            if (!this.EstadoAsistencia.Equals(otra.EstadoAsistencia))
            {
                return false;
            }

            if (!this.Aulas.Equals(otra.Aulas))
            {
                return false;
            }

            if (this.Observaciones != otra.Observaciones)
            {
                return false;
            }

            return true;
        }

        // Toma los datos de la asistencia pasada por paremtro que solo pueden ser generados por 
        //los usuarios(datos que no pueden ser tomados del rapla) y se los setea a si misma
        public void clonarDatosGeneradosPorUsuario(Asistencia otraAsistencia)
        {
            this.finClaseReal = otraAsistencia.finClaseReal;
            this.comienzoClaseReal = otraAsistencia.comienzoClaseReal;
            this.cantidadAlumnos = otraAsistencia.cantidadAlumnos;
            this.observaciones = otraAsistencia.observaciones;
            this.id = otraAsistencia.id;
            this.encargado = otraAsistencia.encargado;
            this.estadoAsistencia = otraAsistencia.estadoAsistencia;
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

    }
}
