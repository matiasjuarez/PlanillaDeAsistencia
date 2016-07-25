﻿using System;
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
    }
}
