using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos.Utilidades
{
    public class Campo
    {
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string valor;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}
