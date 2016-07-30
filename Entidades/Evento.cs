using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Evento
    {
        private int idEvento;
        private int appointmentId;
        private String docente;
        private String jefeCatedra;
        private String curso;
        private DateTime inicioEsperado;
        private DateTime finEsperado;
        private string aula;
        private string materia;
        private bool esParcial;
        private bool esExamen;

        public int AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
        }

        public bool EsParcial
        {
            get { return esParcial; }
            set { esParcial = value; }
        }

        public bool EsExamen
        {
            get { return esExamen; }
            set { esExamen = value; }
        }

        public int IDEvento
        {
            get { return idEvento; }
            set { idEvento = value; }
        }

        public String Docente
        {
            get { return docente; }
            set { docente = value; }
        }

        public String JefeCatedra
        {
            get { return jefeCatedra; }
            set { jefeCatedra = value; }
        }

        public String Curso
        {
            get { return curso; }
            set { curso = value; }
        }

        public DateTime InicioEsperado
        {
            get { return inicioEsperado; }
            set { inicioEsperado = value; }
        }

        public DateTime FinEsperado
        {
            get { return finEsperado; }
            set { finEsperado = value; }
        }

        public string Aula
        {
            get { return aula; }
            set { aula = value; }
        }

        public string Materia
        {
            get { return materia; }
            set { materia = value; }
        }

        // NOTA: la asistencia que se obtiene tendra todos los objetos que la componen
        // de forma incompleta. Por ejemplo, el evento solo trae el nombre del docente. Por lo tanto
        // el objeto docente de la asistencia no va a tener la id correspondiente.
        public Asistencia convertirEnAsistencia()
        {
            Asistencia asistencia = new Asistencia();

            asistencia.AppointmentId = this.AppointmentId;
            asistencia.EventId = this.IDEvento;

            asistencia.ComienzoClaseEsperado = this.InicioEsperado.TimeOfDay;
            asistencia.FinClaseEsperado = this.FinEsperado.TimeOfDay;
            asistencia.DiaDeAsistencia = this.InicioEsperado.Date;

            asistencia.Docente = new Docente(this.Docente);

            asistencia.Asignatura = new Asignatura();
            asistencia.Asignatura.Nombre = this.Materia;

            string[] aulasNombres = this.Aula.Split(',');
            foreach (string aulaNombre in aulasNombres)
            {
                Aula aula = new Aula();
                aula.Nombre = aulaNombre;
                asistencia.agregarAula(aula);
            }

            asistencia.Curso = new Curso();
            asistencia.Curso.Nombre = this.Curso;

            return asistencia;
        }
    }
}