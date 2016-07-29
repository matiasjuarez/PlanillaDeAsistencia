using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class Curso : IEquatable<Curso>
    {
        private int id;
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public bool Equals(Curso other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id == other.Id;
        }
    }
}
