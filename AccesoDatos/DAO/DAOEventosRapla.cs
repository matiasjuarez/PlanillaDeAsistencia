using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Entidades;
using Utilidades;

namespace AccesoDatos
{
    /*
     * Esta clase se encarga de obtener informacion de la base de datos usada por Rapla
     */
    public static class DAOEventosRapla
    {
        /*
        // Este codigo encampsula la funcionalidad basica para obtener los eventos. En la interfaz de la clase
        // colocaremos dos metodos: uno que permite al usuario especificar dos fechas o un dia dado que le interese.
        // Nosotros seremos los responsables de configurar dichos parametros para poder utilizar este metodo para obtener
        // la informacion requerida. Esto permite no repetir codigo
        private static List<Evento> obtenerEventos(DateTime fechaHoraDesde, DateTime fechaHoraHasta)
        {
            List<Evento> eventos = new List<Evento>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionRapla);

            StringBuilder consultaBuilder = new StringBuilder(
                                "SELECT clases.IDClase, profesores.docente, clases.ComienzoClase, clases.FinClase, clases.Aula, clases.Asignatura, " +
                                "jefeCatedra.JefeCatedra, curso.Curso " +
                                "from " +
                                "(select eav.EVENT_ID as IDClase, a.APPOINTMENT_START as ComienzoClase, a.APPOINTMENT_END as FinClase, " +
                                "rav.ATTRIBUTE_VALUE as Aula, c.LABEL as Asignatura " +
                                "from event_attribute_value eav " +
                                "JOIN event_attribute_value eav2 ON eav.EVENT_ID = eav2.EVENT_ID " +
                                "JOIN event_attribute_value eav3 ON eav.EVENT_ID = eav3.EVENT_ID " +
                                "JOIN appointment a ON a.EVENT_ID = eav.EVENT_ID " +
                                "JOIN allocation al ON al.APPOINTMENT_ID = a.ID " +
                                "JOIN rapla.resource_attribute_value rav ON rav.RESOURCE_ID = al.RESOURCE_ID " +
                                "JOIN rapla.category c ON c.ID = eav3.ATTRIBUTE_VALUE " +
                                "WHERE " +
                                "a.APPOINTMENT_START BETWEEN @fechaHoraInicio and @fechaHoraFin " +
                                "group by eav.event_id) as Clases " +
                                "LEFT JOIN " +
                                "(select eav.EVENT_ID as IDClase, eav.ATTRIBUTE_VALUE as Docente " +
                                "from event_attribute_value eav " +
                                "JOIN appointment a ON a.EVENT_ID = eav.EVENT_ID " +
                                "WHERE eav.ATTRIBUTE_KEY like 'docente%' " +
                                "AND " +
                                "a.APPOINTMENT_START BETWEEN @fechaHoraInicio and @fechaHoraFin " +
                                "group by eav.event_id, eav.attribute_key) as Profesores " +
                                "on Profesores.IDClase = Clases.IDClase " +
                                "LEFT JOIN " +
                                "(select eav.EVENT_ID as IDClase, eav.ATTRIBUTE_VALUE as JefeCatedra " + 
                                "from event_attribute_value eav " +
                                "JOIN appointment a ON a.EVENT_ID = eav.EVENT_ID " +
                                "WHERE eav.ATTRIBUTE_KEY like 'jefeCatedra%' " +
                                "AND " +
                                "a.APPOINTMENT_START BETWEEN @fechaHoraInicio and @fechaHoraFin " +
                                "group by eav.event_id, eav.attribute_key) as jefeCatedra " +
                                "on Clases.IDClase = JefeCatedra.IDClase " +
                                "LEFT JOIN " +
                                "(select eav.EVENT_ID as IDClase, eav.ATTRIBUTE_VALUE as Curso " +
                                "from event_attribute_value eav " +
                                "JOIN appointment a ON a.EVENT_ID = eav.EVENT_ID " +
                                "WHERE eav.ATTRIBUTE_KEY like 'curso%' " +
                                "AND " +
                                "a.APPOINTMENT_START BETWEEN @fechaHoraInicio and @fechaHoraFin " +
                                "group by eav.event_id, eav.attribute_key) as curso " +
                                "on Clases.IDClase = curso.IDClase " +
                                "group by IDClase"
                                );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();
            //comando.Parameters.AddWithValue("@algo", valor);

            // Creamos sus parametros.
            // fechaHoraInicio y fechaHoraInicio asi como fechaHoraFin y fechaHoraFin2 son iguales.
            // Se asigna el numero 1 y 2 simplemente porque necesitamos usar el mismo valor en distintos lugares en la consulta.
            MySqlParameter fechaHoraInicioParam = new MySqlParameter();
            fechaHoraInicioParam.ParameterName = "@fechaHoraInicio";
            fechaHoraInicioParam.Value = fechaHoraDesde;

            MySqlParameter fechaHoraFinParam = new MySqlParameter();
            fechaHoraFinParam.ParameterName = "@fechaHoraFin";
            fechaHoraFinParam.Value = fechaHoraHasta;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(fechaHoraInicioParam);
            comando.Parameters.Add(fechaHoraFinParam);

            // Ejecutamos la consulta
            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Evento evento = new Evento();
                    
                    evento.IDEvento = reader.GetInt32("IDClase");
                    

                    evento.Materia = reader.GetString("Asignatura");
                    evento.InicioEsperado = reader.GetDateTime("ComienzoClase");
                    evento.FinEsperado = reader.GetDateTime("FinClase");
                    evento.Aula = reader.GetString("Aula");

                    int docenteOrdinal = reader.GetOrdinal("Docente");
                    if (reader.IsDBNull(docenteOrdinal))
                    {
                        evento.Docente = "Desconocido";
                    }
                    else
                    {
                        evento.Docente = reader.GetString("Docente");
                    }

                    int jefeCatedraOrdinal = reader.GetOrdinal("JefeCatedra");
                    if (reader.IsDBNull(jefeCatedraOrdinal))
                    {
                        evento.JefeCatedra = "Desconocido";
                    }
                    else
                    {
                        evento.JefeCatedra = reader.GetString("JefeCatedra");
                    }


                    int cursoOrdinal = reader.GetOrdinal("Curso");
                    if (reader.IsDBNull(cursoOrdinal))
                    {
                        evento.Curso = "Desconocido";
                    }
                    else
                    {
                        evento.Curso = reader.GetString("Curso");
                    }
                    

                    eventos.Add(evento);
                }
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }

            return eventos;
        }
    

        // Devuelve todos los eventos para el dia pasado como parametro
        public static List<Evento> obtenerEventosDelDia(DateTime fecha){

            // Establecemos dos dates times para poder decirle a la consulta
            // que me busque las asistencias dentro de un dia en particular entre
            // las 00:00:00 y las 23:59:59
            DateTime fechaHoraInicio;
            DateTime fechaHoraFin;

            fechaHoraInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 1);
            fechaHoraFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);

            return obtenerEventos(fechaHoraInicio, fechaHoraFin);
        }*/

        private static List<Evento> obtenerEventosDesde2016(DateTime fechaInicio, DateTime fechaFin){
            List<Evento> eventos = new List<Evento>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionRapla);

            string consulta = obtenerSentenciaSelect2016();

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter fechaInicioParam = new MySqlParameter();
            fechaInicioParam.ParameterName = "@fechaInicio";
            fechaInicioParam.Value = fechaInicio;

            MySqlParameter fechaFinParam = new MySqlParameter();
            fechaFinParam.ParameterName = "@fechaFin";
            fechaFinParam.Value = fechaFin;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(fechaInicioParam);
            comando.Parameters.Add(fechaFinParam);

            MySqlDataReader reader = comando.ExecuteReader();

            return armarEventosDesdeReader(reader);
        }

        private static string obtenerSentenciaSelect2016()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT EVENTO, SinAulas.IDAP, Inicio, Fin, Materia, esExamen, esParcial, ");
            builder.Append("Docente, JefeCatedra, Curso, Aulas FROM( ");
            builder.Append("SELECT evento.ID as EVENTO, ap.ID IDAP, ap.APPOINTMENT_START Inicio, ap.APPOINTMENT_END Fin, ");
            builder.Append("IF(cMat.LABEL <> 'EXAMEN' and cMat.LABEL <> 'PARCIAL',cMat.LABEL, (select label from category as c2 where c2.ID = cMat.PARENT_ID)) as Materia, ");
            builder.Append("IF(cMat.LABEL IS NULL or cMat.LABEL<> 'EXAMEN', False, True) as esExamen, ");
            builder.Append("IF(cMat.LABEL IS NULL or cMat.LABEL<> 'PARCIAL', False, True) as esParcial, ");
            builder.Append("IF(eavDoc.ATTRIBUTE_VALUE REGEXP '^[0-9]+$', cDoc.Label, eavDoc.ATTRIBUTE_VALUE) as Docente, ");
            builder.Append("IF(eavJef.ATTRIBUTE_VALUE REGEXP '^[0-9]+$', cJef.Label, eavJef.ATTRIBUTE_VALUE) as JefeCatedra, ");
            builder.Append("eavCur.ATTRIBUTE_VALUE as curso ");
            builder.Append("FROM event as evento ");
            builder.Append("left join appointment ap on ap.EVENT_ID = evento.ID ");
            builder.Append("left join event_attribute_value as eavDoc on eavDoc.ATTRIBUTE_KEY like 'docen%' and eavDoc.EVENT_ID = evento.ID ");
            builder.Append("left join category cDoc on cDoc.ID = eavDoc.ATTRIBUTE_VALUE ");
            builder.Append("left join event_attribute_value as eavMat on eavMat.ATTRIBUTE_KEY like 'espec%' and eavMat.EVENT_ID = evento.ID ");
            builder.Append("left join category cMat on cMat.ID = eavMat.ATTRIBUTE_VALUE ");
            builder.Append("left join event_attribute_value as eavJef on eavJef.ATTRIBUTE_KEY like 'jefe%' and eavJef.EVENT_ID = evento.ID ");
            builder.Append("left join category cJef on cJef.ID = eavJef.ATTRIBUTE_VALUE ");
            builder.Append("left join event_attribute_value as eavCur on eavCur.ATTRIBUTE_KEY like 'curso%' and eavCur.EVENT_ID = evento.ID ");
            builder.Append(") as SinAulas ");
            builder.Append("LEFT join ");
            builder.Append("(SELECT APPOINTMENT_ID as IDAP, GROUP_CONCAT(rav.ATTRIBUTE_VALUE separator ',') as Aulas ");
            builder.Append("from allocation as al ");
            builder.Append("left join resource_attribute_value rav on rav.RESOURCE_ID = al.RESOURCE_ID ");
            builder.Append("where rav.ATTRIBUTE_KEY = 'name' ");
            builder.Append("group by al.APPOINTMENT_ID) as Aulas ");
            builder.Append("ON SinAulas.IDAP = Aulas.IDAP ");
            builder.Append("WHERE Inicio BETWEEN @fechaInicio and @fechaFin ");
            builder.Append("group by IDAP");

            return builder.ToString();
        }

        private static List<Evento> armarEventosDesdeReader(MySqlDataReader reader)
        {
            List<Evento> eventos = new List<Evento>();

            while (reader.Read())
            {
                Evento evento = new Evento();

                evento.IDEvento = ValidadorValoresNull.getInt(reader, "EVENTO");
                evento.AppointmentId = ValidadorValoresNull.getInt(reader, "IDAP");
                evento.Materia = ValidadorValoresNull.getString(reader, "Materia");
                evento.InicioEsperado = ValidadorValoresNull.getDateTime(reader, "Inicio");
                evento.FinEsperado = ValidadorValoresNull.getDateTime(reader, "Fin");
                evento.Aula = ValidadorValoresNull.getString(reader, "Aulas");
                evento.Docente = ValidadorValoresNull.getString(reader, "Docente");
                evento.JefeCatedra = ValidadorValoresNull.getString(reader, "JefeCatedra");
                evento.Curso = ValidadorValoresNull.getString(reader, "Curso");

                int esExamen = ValidadorValoresNull.getInt(reader, "esExamen");
                int esParcial = ValidadorValoresNull.getInt(reader, "esParcial");

                if (esExamen == 1)evento.EsExamen = true;
                else evento.EsExamen = false;

                if (esParcial == 1) evento.EsParcial = true;
                else evento.EsParcial = false;
                
                eventos.Add(evento);
            }

            return eventos;
        }

        public static List<Evento> obtenerEventosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            DateTime fechaHoraInicio;
            DateTime fechaHoraFin;

            DateTime fechaMinima = new DateTime(2016, 1, 1, 0, 0, 1);

            if (fechaInicio < fechaMinima)
            {
                fechaInicio = fechaMinima;
            }
            if (fechaFin < fechaMinima)
            {
                fechaFin = fechaMinima;
            }

            fechaHoraInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
            fechaHoraFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);

            return obtenerEventosDesde2016(fechaHoraInicio, fechaHoraFin);
        }
    }
}
