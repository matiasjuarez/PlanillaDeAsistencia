using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class RolUsuario
    {
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public RolUsuario() { }

        public RolUsuario(string nombreRol)
        {
            nombre = nombreRol;
        }
    }
}
