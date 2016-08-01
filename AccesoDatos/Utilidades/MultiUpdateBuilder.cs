using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos.Utilidades
{
    public class MultiUpdateBuilder
    {
        List<UpdateBuilder> builders;

        public MultiUpdateBuilder()
        {
            builders = new List<UpdateBuilder>();
        }

        public void agregarBuilder(UpdateBuilder builder)
        {
            builders.Add(builder);
        }

        public string crearMultiQuery()
        {
            StringBuilder query = new StringBuilder();

            for (int i = 0; i < builders.Count; i++)
            {
                UpdateBuilder builder = builders.ElementAt<UpdateBuilder>(i);
                builder.IndiceNumeracion = i;

                query.Append(builder.crearQuery());
            }

            return query.ToString();
        }
    }
}
