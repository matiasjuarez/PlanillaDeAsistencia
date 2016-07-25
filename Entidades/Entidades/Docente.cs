using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class Docente : IEquatable<Docente>
    {
        private string nombre;
        private string apellido;
        private int id;

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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Docente() { }

        public Docente(string nombre) {
            Nombre = nombre;
        }

        public bool Equals(Docente other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }
    }
}
