using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Appointment
    {
        private int idEvento;
        private int appointmentId;
        private string docente;
        private string jefeCatedra;
        private string curso;
        private string tipoRepeticion;
        private DateTime finRepeticion;
        private DateTime inicio;
        private DateTime fin;
        private string aulas;
        private string materia;
        private bool esParcial;
        private bool esExamen;
        private string excepciones;

        public int AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
        }

        public string TipoRepeticion
        {
            get { return tipoRepeticion; }
            set { tipoRepeticion = value; }
        }
        private int cantidadRepeticiones;

        public int CantidadRepeticiones
        {
            get { return cantidadRepeticiones; }
            set { cantidadRepeticiones = value; }
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

        public string Docente
        {
            get { return docente; }
            set { docente = value; }
        }

        public string JefeCatedra
        {
            get 
            {
                return jefeCatedra; 
            }
            set { jefeCatedra = value; }
        }

        public string Curso
        {
            get { return curso; }
            set { curso = value; }
        }

        public DateTime Inicio
        {
            get { return inicio; }
            set { inicio = value; }
        }

        public DateTime Fin
        {
            get { return fin; }
            set { fin = value; }
        }

        public DateTime FinRepeticion
        {
            get { return finRepeticion; }
            set { finRepeticion = value; }
        }

        public string Aulas
        {
            get { return aulas; }
            set { aulas = value; }
        }

        public string Excepciones
        {
            get { return excepciones; }
            set { excepciones = value; }
        }

        public string Asignatura
        {
            get { return materia; }
            set { materia = value; }
        }
    }
}