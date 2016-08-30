using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Entidades
{
    [Serializable]
    public class Encargado
    {
        private int id;
        private string nombre;
        private string apellido;
        private string telefono;
        private string dni;
        private DateTime fechaNacimiento;
        private string legajo;
        private string mailGeneral;
        private string mailBBS;
        private Image foto;

        public Encargado()
        {
            nombre = "";
            apellido = "";
            telefono = "";
            dni = "";
            legajo = "";
            mailBBS = "";
            mailGeneral = "";
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string NombreCompleto
        {
            get { return Apellido + ", " + Nombre; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        public string Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }

        public string MailGeneral
        {
            get { return mailGeneral; }
            set { mailGeneral = value; }
        }

        public string MailBBS
        {
            get { return mailBBS; }
            set { mailBBS = value; }
        }

        public Image Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        public string getNombreCompleto()
        {
            return Nombre + Apellido;
        }
    }
}
