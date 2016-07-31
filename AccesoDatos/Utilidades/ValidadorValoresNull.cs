using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public abstract class ValidadorValoresNull
    {
        public const string VALOR_POR_DEFECTO_STRING = "N/A";
        public const int VALOR_POR_DEFECTO_INT = -1;
        private static DateTime VALOR_POR_DEFECTO_DATETIME = new DateTime(1, 1, 1);

        public static string getString(MySqlDataReader reader, string nombreColumna)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getString(reader, indiceColumna);
        }

        public static string getString(MySqlDataReader reader, int indiceColumna)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetString(indiceColumna);
            else
                return VALOR_POR_DEFECTO_STRING;
        }

        public static int getInt(MySqlDataReader reader, string nombreColumna)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getInt(reader, indiceColumna);
        }

        public static int getInt(MySqlDataReader reader, int indiceColumna)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetInt32(indiceColumna);
            else
                return VALOR_POR_DEFECTO_INT;
        }

        public static bool getBoolean(MySqlDataReader reader, string nombreColumna)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getBoolean(reader, indiceColumna);
        }

        public static bool getBoolean(MySqlDataReader reader, int indiceColumna)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetBoolean(indiceColumna);
            else
                return false;
        }

        public static DateTime getDateTime(MySqlDataReader reader, string nombreColumna)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getDateTime(reader, indiceColumna);
        }

        public static DateTime getDateTime(MySqlDataReader reader, int indiceColumna)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetDateTime(indiceColumna);
            else
                return VALOR_POR_DEFECTO_DATETIME;
        }

        public static TimeSpan getTimeSpan(MySqlDataReader reader, int indiceColumna)
        {
            if (!reader.IsDBNull(indiceColumna))
            {
                DateTime fecha = reader.GetDateTime(indiceColumna);
                return fecha.TimeOfDay;
            }
            else return new TimeSpan(0, 0, 0);
        }

        public static TimeSpan getTimeSpan(MySqlDataReader reader, string nombreColumna)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getTimeSpan(reader, indiceColumna);
        }

        public static decimal getDecimal(MySqlDataReader reader, string nombreColumna, decimal valorPorDefecto)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getDecimal(reader, indiceColumna, valorPorDefecto);
        }

        public static decimal getDecimal(MySqlDataReader reader, int indiceColumna, decimal valorPorDefecto)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetDecimal(indiceColumna);
            else
                return valorPorDefecto;
        }
        
    }
}