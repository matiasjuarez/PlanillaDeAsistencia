using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    interface IRestriccion
    {
        string campoQueSeRestringe {get; set;}
        string nombreBaseParametros { get; set; }

        string obtenerRestriccion();
        MySqlCommand obtenerComandoParametrizado(MySqlCommand comando);
    }
}
