using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContenedoresDeDatos
{
    public abstract class Contenedor<Clave, Valor>
    {
        protected Dictionary<Clave, Valor> datos = new Dictionary<Clave,Valor>();

        public Valor obtenerDato(Clave clave)
        {
            Valor valor;

            datos.TryGetValue(clave, out valor);

            return valor;
        }

        public List<Valor> obtenerDatos()
        {
            List<Valor> valores = new List<Valor>(datos.Values);
            return valores;
        }

        public void guardarDato(Clave clave, Valor valor)
        {
            if(datos.ContainsKey(clave))
            {
                datos[clave] = valor;
            }
            else
            {
                datos.Add(clave, valor);
            }
        }

        public bool eliminarDato(Clave clave)
        {
            return datos.Remove(clave);
        }

        public int Count
        {
            get
            {
                return datos.Count;
            }
        }

        public void limpiarContenedor()
        {
            datos.Clear();
        }

        public bool existeClave(Clave clave)
        {
            return datos.ContainsKey(clave);
        }

        public abstract void refrescarDatos();
    }
}