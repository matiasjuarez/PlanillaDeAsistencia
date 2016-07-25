using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public class RestriccionIN: IRestriccion
    {

        // El nombre del campo que se va a restringir en el WHERE
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

        private string _nombreBaseParametros = "defaultParamName";
        string IRestriccion.nombreBaseParametros
        {
            get
            {
                return this._nombreBaseParametros;
            }
            set
            {
                this._nombreBaseParametros = value;
            }
        }

        // Los valores que deberian ir dentro de la clausula IN
        private List<string> valoresIN;
        public List<string> ValoresIN
        {
            get { return valoresIN; }
            set { valoresIN = value; }
        }

        // CONSTRUCTOR
        public RestriccionIN(string campo, List<string> valoresIN) : this(campo, "defaultParamName", valoresIN)
        {
            
        }

        public RestriccionIN(string campo, string nombreBaseParametros, List<string> valoresIN)
        {
            this._campoQueSeRestringe = campo;
            this._nombreBaseParametros = nombreBaseParametros;

            if (valoresIN != null)
            {
                this.valoresIN = valoresIN;
            }
            else
            {
                this.valoresIN = new List<string>();
            }
        }

        public RestriccionIN(string campo, string nombreBaseParametros) : this(campo, nombreBaseParametros, null)
        {

        }

        // Una representacion en formato cadena de la restriccion que se esta armando
        public string obtenerRestriccion()
        {
            int cantidadValoresIn = valoresIN.Count;
            if (cantidadValoresIn == 0)
            {
                return "";
            }

            StringBuilder builder = new StringBuilder();

            builder.Append(this._campoQueSeRestringe);
            builder.Append(" IN (");

            for (int i = 0; i < cantidadValoresIn; i++)
            {
                if (i < (valoresIN.Count - 1))
                {
                    builder.Append(obtenerNombreParaParametro(i) + ",");
                }
                else
                {
                    builder.Append(obtenerNombreParaParametro(i) + ")");
                }
            }

            return builder.ToString();
        }

        // Se recibe como parametro un objeto MySqlCommand y se le agregan los parametros correspondientes.
        // En este caso se agrega un parametro por cada elemento de la lista IN
        public MySqlCommand obtenerComandoParametrizado(MySqlCommand comando)
        {
            int length = this.valoresIN.Count;
            string parametroNombreBase = obtenerNombreBaseParaParametros();

            for (int i = 0; i < length; i++)
            {
                MySqlParameter parametro = new MySqlParameter();
                parametro.ParameterName = obtenerNombreParaParametro(i);
                parametro.Value = this.valoresIN[i];

                comando.Parameters.Add(parametro);
            }

            return comando;
        }

        // Uno el nombre base de los parametros con el indice del parametro y devuelve como resultado
        // el nombre final para un parametro dado.
        private string obtenerNombreParaParametro(int indiceParametro)
        {
            string nombreParametro = obtenerNombreBaseParaParametros();
            nombreParametro += indiceParametro;

            return nombreParametro;
        }

        // Obtiene el nombre base que se usara para nombrar los parametros. El formato que tendra sera: 
        // '@[NombreDelCampo]_parametro_[NumeroDelParametro]
        private string obtenerNombreBaseParaParametros()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("@");
            builder.Append(this._nombreBaseParametros);
            builder.Append("_parametro_");

            return builder.ToString();
        }
    }
}
