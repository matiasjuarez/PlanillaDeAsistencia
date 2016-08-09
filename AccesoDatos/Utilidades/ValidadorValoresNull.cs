using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public abstract class ValidadorValoresNull
    {
        private static Configuracion.Config configuracion = Configuracion.Config.getInstance();

        public static string getString(MySqlDataReader reader, string nombreColumna, string valorParaNulo)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getString(reader, indiceColumna, valorParaNulo);
        }

        public static string getString(MySqlDataReader reader, int indiceColumna, string valorParaNulo)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetString(indiceColumna);
            else
                return valorParaNulo;
        }

        public static int getInt(MySqlDataReader reader, string nombreColumna, int valorParaNulo)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getInt(reader, indiceColumna, valorParaNulo);
        }

        public static int getInt(MySqlDataReader reader, int indiceColumna, int valorParaNulo)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetInt32(indiceColumna);
            else
                return valorParaNulo;
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
                return configuracion.ValorParaFechaNula;
        }

        public static TimeSpan getTimeSpan(MySqlDataReader reader, int indiceColumna)
        {
            if (!reader.IsDBNull(indiceColumna))
            {
                DateTime fecha = reader.GetDateTime(indiceColumna);
                return fecha.TimeOfDay;
            }
            else return configuracion.ValorParaHoraNula;
        }

        public static TimeSpan getTimeSpan(MySqlDataReader reader, string nombreColumna)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getTimeSpan(reader, indiceColumna);
        }

        public static decimal getDecimal(MySqlDataReader reader, string nombreColumna, decimal valorParaNulo)
        {
            int indiceColumna = reader.GetOrdinal(nombreColumna);
            return getDecimal(reader, indiceColumna, valorParaNulo);
        }

        public static decimal getDecimal(MySqlDataReader reader, int indiceColumna, decimal valorParaNulo)
        {
            if (!reader.IsDBNull(indiceColumna))
                return reader.GetDecimal(indiceColumna);
            else
                return valorParaNulo;
        }
        
    }
}