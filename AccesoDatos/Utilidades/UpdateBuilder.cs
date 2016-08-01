using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos
{
    public class UpdateBuilder
    {
        private HashSet<string> campos;
        private string nombreTabla;
        private int indiceNumeracion = 0;
        public int IndiceNumeracion
        {
            get { return indiceNumeracion; }
            set { indiceNumeracion = value; }
        }

        public UpdateBuilder(string nombreTabla)
        {
            campos = new HashSet<string>();
            this.nombreTabla = nombreTabla;
        }

        public void agregarCampo(string nombre)
        {
            campos.Add(nombre);
        }

        public string crearQuery()
        {
            StringBuilder query = new StringBuilder();

            query.Append("UPDATE " + this.nombreTabla + " SET ");

            foreach (string campo in campos)
            {
                query.Append(campo + " = " + crearNombreParametro(campo) + ",");
            }

            string retorno = query.ToString();
            retorno = retorno.Substring(0, retorno.Length - 1);

            return retorno;
        }

        private string crearNombreParametro(string nombreCampo)
        {
            string parametro = "@" + nombreCampo + this.indiceNumeracion;
            return parametro;
        }
    }
}
