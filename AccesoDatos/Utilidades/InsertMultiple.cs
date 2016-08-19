using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;
using Utilidades;

namespace AccesoDatos
{
    public class InsertMultiple
    {
        private List<Insert> inserts = new List<Insert>();

        public void agregarInsert(Insert insert)
        {
            inserts.Add(insert);
        }

        public string crearSentencia()
        {
            string sentencia = "";

            int i = 0;
            foreach (Insert insert in inserts)
            {
                foreach (MySqlParameter parametro in insert.Parametros)
                {
                    parametro.ParameterName += i;
                }

                i++;

                sentencia += insert.crearSentencia() + ";";
            }

            return sentencia;
        }

        public MySqlCommand crearCommand()
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = crearSentencia();

            foreach (Insert insert in inserts)
            {
                foreach (MySqlParameter parameter in insert.Parametros)
                {
                    command.Parameters.Add(parameter);
                }
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
                GestorExcepciones.mostrarExcepcion(ex, "Hubo un problema al intentar hacer un INSERT multiple");
                return -1;
            }
        }
    }
}