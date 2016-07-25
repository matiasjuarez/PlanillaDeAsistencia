using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class Especialidad
    {
        private int id;
        private string nombre;
        // La letra que representa la especialidad. Por ejemplo, la K es de sistemas
        private string letra;

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

        public string Letra
        {
            get { return letra; }
            set { letra = value; }
        }
    }
}
