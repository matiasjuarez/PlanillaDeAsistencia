using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizacionInterBase
{
    /*
     * Esta clase se utilizara para llevar un rastro de que valores de un cierto tipo se deberian agregar,
     * eliminar o modificar en la base de datos.
     * */
    public class ContenedorCambios<Valor>
    {
        private List<Valor> agregar = new List<Valor>();
        private List<Valor> modificar = new List<Valor>();
        private List<Valor> eliminar = new List<Valor>();

        public void agregarValor(Valor valor)
        {
            agregar.Add(valor);
        }

        public void agregarValores(List<Valor> valores)
        {
            agregar.AddRange(valores);
        }

        public void modificarValor(Valor valor)
        {
            modificar.Add(valor);
        }

        public void modificarValores(List<Valor> valores)
        {
            modificar.AddRange(valores);
        }

        public void eliminarValor(Valor valor)
        {
            eliminar.Add(valor);
        }

        public void eliminarValores(List<Valor> valores)
        {
            eliminar.AddRange(valores);
        }

        public List<Valor> obtenerValoresAgregar()
        {
            return this.agregar;
        }

        public List<Valor> obtenerValoresModificar()
        {
            return this.modificar;
        }

        public List<Valor> obtenerValoresEliminar()
        {
            return this.eliminar;
        }

        public bool contieneCambios()
        {
            if (agregar.Count != 0 || modificar.Count != 0 || eliminar.Count != 0) return true;
            return false;
        }
    }
}
