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
            set 
            {
                if (tipoRepeticion == String.Empty) tipoRepeticion = null;
                else tipoRepeticion = value; 
            }
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
            set 
            {
                if (value == null) docente = String.Empty;
                else docente = value; 
            }
        }

        public string JefeCatedra
        {
            get 
            {
                return jefeCatedra; 
            }
            set 
            {
                if (value == null) jefeCatedra = String.Empty;
                else jefeCatedra = value; 
            }
        }

        public string Curso
        {
            get { return curso; }
            set 
            {
                if (value == null) curso = String.Empty;
                else curso = value; 
            }
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
            set 
            {
                if (value == null) aulas = String.Empty;
                else aulas = value; 
            }
        }

        public string Excepciones
        {
            get { return excepciones; }
            set 
            {
                if (value == null) excepciones = String.Empty;
                else excepciones = value; 
            }
        }

        public string Asignatura
        {
            get { return materia; }
            set 
            {
                if (value == null) materia = String.Empty;
                else materia = value; 
            }
        }
    }
}