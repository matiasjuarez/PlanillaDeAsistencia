using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos.Utilidades
{
    public abstract class ItemConsulta
    {
        protected HashSet<string> campos = new HashSet<string>();
        private int numeracion;
        public int Numeracion
        {
            get { return numeracion; }
            set { numeracion = value; }
        }

        public void agregarCampo(string campo)
        {
            campos.Add(campo);
        }

        protected string obtenerNombreCampo(string campo)
        {
            string nombre = campo + numeracion;
            return nombre;
        }

        protected string obtenerNombreParametro(string campo)
        {
            string nombre = "@" + campo + numeracion;
            return nombre;
        }

        public abstract string obtenerQuery();
    }
}
