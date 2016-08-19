using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql;
using MySql.Data.MySqlClient;
using Utilidades;

namespace AccesoDatos
{
    public class Insert
    {
        private List<string> campos = new List<string>();
        private List<MySqlParameter> parametros = new List<MySqlParameter>();
        private string nombreTabla;

        public List<MySqlParameter> Parametros
        {
            get { return parametros; }
        }

        public void agregarCampo(string campo)
        {
            campos.Add(campo);
        }

        public void agregarParametro(MySqlParameter parametro)
        {
            parametros.Add(parametro);
        }

        public void setNombreTabla(string nombreTabla)
        {
            this.nombreTabla = nombreTabla;
        }

        public void agregarParametro(string nombre, object valor)
        {
            MySqlParameter parametro = new MySqlParameter(nombre, valor);
            agregarParametro(parametro);
        }

        public string crearSentencia()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO ");
            builder.Append(this.nombreTabla);
            builder.Append(obtenerListaCampos(this.campos));
            builder.Append(" VALUES ");
            builder.Append(obtenerListaParametros(this.parametros));

            return builder.ToString();
        }

        private string obtenerListaCampos(List<string> campos)
        {
            if (campos.Count == 0) return "";

            StringBuilder listaCampos = new StringBuilder("(");
            foreach (string campo in campos)
            {
                listaCampos.Append(campo);
                listaCampos.Append(",");
            }
            listaCampos.Remove(listaCampos.Length - 1, 1);

            listaCampos.Append(")");

            return listaCampos.ToString();
        }

        private string obtenerListaParametros(List<MySqlParameter> parametros)
        {
            if (parametros.Count == 0) return "";

            StringBuilder listaParametros = new StringBuilder("(");
            foreach (MySqlParameter parametro in parametros)
            {
                listaParametros.Append(parametro.ParameterName);
                listaParametros.Append(",");
            }

            listaParametros.Remove(listaParametros.Length - 1, 1);

            listaParametros.Append(")");

            return listaParametros.ToString();
        }

        public MySqlCommand crearCommand()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = crearSentencia();

            foreach (MySqlParameter parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.ParameterName, parametro.Value);
            }

            return command;
        }

        public int ejecutar(MySqlConnection conexion)
        {
            MySqlCommand command = crearCommand();
            command.Connection = conexion;

            try
            {
                return command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                GestorExcepciones.mostrarExcepcion(ex, "Hubo un problema al ejecutar un INSERT");
                return -1;
            }
        }
    }
}