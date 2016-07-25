using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public class restriccionBetween: IRestriccion
    {
        private string _campoQueSeRestringe;

        string IRestriccion.campoQueSeRestringe
        {
            get
            {
                return this._campoQueSeRestringe;
            }
            set
            {
                this._campoQueSeRestringe = value;
            }
        }



        string IRestriccion.obtenerRestriccion()
        {
            throw new NotImplementedException();
        }

        public MySqlCommand obtenerComandoParametrizado(MySqlCommand comando)
        {
            return null;
        }


        public string nombreBaseParametros
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
