using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class Aula : IEquatable<Aula>
    {
        private int id;
        private string nombre;
        private string descripcion;

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

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public bool Equals(Aula other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        /*public static bool operator ==(Aula d1, Aula d2)
        {
            if (d1 == null || d2 == null)
            {
                return Object.Equals(d1, d2);
            }

            return d1.Equals(d2);
        }

        public static bool operator !=(Aula d1, Aula d2)
        {
            if (d1 == null || d2 == null)
            {
                return !Object.Equals(d1, d2);
            }

            return !d1.Equals(d2);
        }*/
    }
}
