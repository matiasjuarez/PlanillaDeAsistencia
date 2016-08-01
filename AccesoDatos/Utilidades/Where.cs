using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos.Utilidades
{
    public class Where : ItemConsulta
    {
        public override string obtenerQuery()
        {
            StringBuilder query = new StringBuilder();

            query.Append("WHERE ");

            foreach (string campo in campos)
            {
                query.Append(obtenerNombreCampo(campo) + " = " + obtenerNombreParametro(campo));
            }

            return "";
        }
    }
}
