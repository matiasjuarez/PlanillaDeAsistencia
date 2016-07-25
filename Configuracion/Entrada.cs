using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion
{
    public class Entrada
    {
        public Entrada() { }

        public Entrada(int id)
        {
            idEntrada = id;
            valor = "";
        }

        public Entrada(int id, string valor)
        {
            this.idEntrada = id;
            this.valor = valor;
        }

        private int idEntrada;
        public int IdEntrada
        {
            get { return idEntrada; }
            set { idEntrada = value; }
        }

        private string valor;
        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}
