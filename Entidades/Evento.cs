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
        private TimeSpan inicioEsperado;
        private TimeSpan finEsperado;
        private DateTime fechaEvento;
        private string aulas;
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

        public TimeSpan InicioEsperado
        {
            get { return inicioEsperado; }
            set { inicioEsperado = value; }
        }

        public TimeSpan FinEsperado
        {
            get { return finEsperado; }
            set { finEsperado = value; }
        }


        public DateTime FechaEvento
        {
            get { return fechaEvento; }
            set { fechaEvento = value; }
        }

        public string Aulas
        {
            get { return aulas; }
            set { aulas = value; }
        }

        public string Asignatura
        {
            get { return materia; }
            set { materia = value; }
        }
    }
}