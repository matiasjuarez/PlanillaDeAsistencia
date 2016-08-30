using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Entidades;
using Utilidades;
using Configuracion;

namespace AccesoDatos
{
    public static class DAOAsistencias
    {
        private static Config configuracion = Config.getInstance();

        private static List<Asistencia> obtenerAsistencias(DateTime fechaHoraInicio, DateTime fechaHoraFin)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            
            string consulta = obtenerSentenciaSelectSinRestricciones();
            consulta += " WHERE comienzoClaseEsperado BETWEEN @fechaInicio and @fechaFin";

            MySqlCommand command = new MySqlCommand();
            command.Connection = gestorConexion.getConexionAbierta();
            command.CommandText = consulta;
            command.Parameters.AddWithValue("@fechaInicio", fechaHoraInicio);
            command.Parameters.AddWithValue("@fechaFin", fechaHoraFin);

            MySqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Asistencia asistencia = armarAsistenciaDesdeReader(reader);
                    asistencias.Add(asistencia);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { gestorConexion.cerrarConexion(); }

            return asistencias;
        }

        private static List<Asistencia> obtenerAsistenciasParaFechas(List<DateTime> fechas)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlCommand command = new MySqlCommand();
            command.Connection = gestorConexion.getConexionAbierta();

            StringBuilder restriccionIN = new StringBuilder("IN(");

            for (int i = 0; i < fechas.Count; i++)
            {
                string nuevoParametro = "@fecha" + i;

                restriccionIN.Append(nuevoParametro);
                restriccionIN.Append(",");

                command.Parameters.AddWithValue(nuevoParametro, fechas.ElementAt(i).Date);
            }

            restriccionIN.Remove(restriccionIN.Length - 1, 1);
            restriccionIN.Append(")");

            string consulta = obtenerSentenciaSelectSinRestricciones();
            consulta += " WHERE asistencias.comienzoClaseEsperado ";
            consulta += restriccionIN.ToString();

            command.CommandText = consulta;

            MySqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Asistencia asistencia = armarAsistenciaDesdeReader(reader);
                    asistencias.Add(asistencia);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { gestorConexion.cerrarConexion(); }

            return asistencias;
        }

        // Obtiene la sentencia Select que se debe utilizar para obtener todas las asistencias de la base de datos
        private static string obtenerSentenciaSelectSinRestricciones()
        {
            String consulta = "select asistencias.asistenciaId as Id, asistencias.APID as AppointmentId, asistencias.eventId, " +
                    "asistencias.comienzoClaseEsperado as InicioEsperado, " +
                    "asistencias.finClaseEsperado as FinEsperado, asistencias.comienzoClaseReal as InicioReal, " +
                    "asistencias.finClaseReal as FinReal, asistencias.cantidadAlumnos as Alumnos, " +
                    "asistencias.encargadoNombre as Encargado, asistencias.idEncargado, asistencias.docenteNombre, " +
                    "asistencias.docenteId, asistencias.asignaturaNombre, asistencias.asignaturaId, " +
                    "asistencias.estadoAsistenciaNombre, asistencias.estadoAsistenciaId, " +
                    "asistencias.cursoId, asistencias.cursoNombre, aulas.aulasId, aulas.aulasNombre, asistencias.observaciones FROM ( " +
                    "select asistencia.id as asistenciaId, asistencia.appointmentId as APID, asistencia.eventId as eventId, " +
                    "comienzoClaseEsperado, finClaseEsperado, " +
                    "comienzoClaseReal, finClaseReal, cantidadAlumnos, " +
                    "(encargado.nombre + ' ' + encargado.apellido) as encargadoNombre, idEncargado, " +
                    "docente.nombre as docenteNombre, docente.id as docenteId, " +
                    "asignatura.nombre as asignaturaNombre, asignatura.id as asignaturaId, " +
                    "estadoasistencia.nombre as estadoAsistenciaNombre, estadoasistencia.id as estadoAsistenciaId, " +
                    "curso.id as cursoId, curso.nombre as cursoNombre, observaciones " +
                    "from asistencia " +
                    "left join docente on docente.id = asistencia.idDocente " +
                    "left join asignatura on asignatura.id = asistencia.idAsignatura " +
                    "left join estadoasistencia on estadoasistencia.id = asistencia.idEstadoAsistencia " +
                    "left join curso on curso.id = asistencia.idCurso " +
                    "left join encargado on encargado.id = asistencia.idEncargado " +
                    ") as asistencias " +
                    "LEFT join ( " +
                    "SELECT idAppointmentAsistencia, group_concat(idAula) aulasId, group_concat(a.nombre) aulasNombre " +
                    "FROM aulasporasistencia axa " +
                    "join aula a on a.id = axa.idAula " +
                    "group by axa.idAppointmentAsistencia " +
                    ") as aulas " +
                    "ON aulas.idAppointmentAsistencia = asistencias.APID";

            return consulta;
        }

        // Toma un objeto de tipo MySqlDataReader y teniendo en cuenta la posicion del puntero del mismo
        // arma una asistencia.
        private static Asistencia armarAsistenciaDesdeReader(MySqlDataReader reader)
        {
            Asistencia asistencia = new Asistencia();
            Docente docente = new Docente();
            Asignatura asignatura = new Asignatura();
            Encargado encargado = new Encargado();
            Curso curso = new Curso();
            Especialidad especialidad = new Especialidad();
            EstadoAsistencia estadoAsistencia = new EstadoAsistencia();

            docente.Nombre = ValidadorValoresNull.getString(reader, "docenteNombre", "");
            docente.Id = ValidadorValoresNull.getInt(reader, "docenteId", configuracion.IdDocenteNoAsignado);

            asignatura.Nombre = ValidadorValoresNull.getString(reader, "asignaturaNombre", "");
            asignatura.Id = ValidadorValoresNull.getInt(reader, "asignaturaId", configuracion.IdAsignaturaNoAsignada);

            encargado.Nombre = ValidadorValoresNull.getString(reader, "encargado", "");
            encargado.Id = ValidadorValoresNull.getInt(reader, "idEncargado", configuracion.IdEncargadoNoAsignado);

            curso.Id = ValidadorValoresNull.getInt(reader, "cursoId", configuracion.IdCursoNoAsignado);
            curso.Nombre = ValidadorValoresNull.getString(reader, "cursoNombre", "");

            string nombreAulas = ValidadorValoresNull.getString(reader, "aulasNombre", "");
            string[] listaNombresAulas = new string[0];
            if (nombreAulas != null) listaNombresAulas = nombreAulas.Split(',');

            string idAulas = ValidadorValoresNull.getString(reader, "aulasId", "");
            string[] listaIdsAulas = new string[0];
            if (idAulas != null) listaIdsAulas = idAulas.Split(',');

            for (int i = 0; i < listaIdsAulas.Length; i++)
            {
                Aula nuevaAula = new Aula();
                int idAula;
                if(int.TryParse(listaIdsAulas[i], out idAula))
                {
                    nuevaAula.Id = idAula;
                    nuevaAula.Nombre = listaNombresAulas[i];
                    asistencia.agregarAula(nuevaAula);
                }
            }

            estadoAsistencia.Id = ValidadorValoresNull.getInt(reader, "estadoAsistenciaId", configuracion.IdEstadoAsistenciaNoAsignado);
            estadoAsistencia.Nombre = ValidadorValoresNull.getString(reader, "estadoAsistenciaNombre", "");

            asistencia.Asignatura = asignatura;
            asistencia.Curso = curso;
            asistencia.Docente = docente;
            asistencia.Encargado = encargado;
            asistencia.EstadoAsistencia = estadoAsistencia;
            asistencia.CantidadAlumnos = ValidadorValoresNull.getInt(reader, "alumnos", 0);
            asistencia.Fecha = ValidadorValoresNull.getDateTime(reader, "InicioEsperado");
            asistencia.HoraEntradaEsperada = ValidadorValoresNull.getTimeSpan(reader, "InicioEsperado");
            asistencia.HoraEntradaReal = ValidadorValoresNull.getTimeSpan(reader, "InicioReal");
            asistencia.HoraSalidaEsperada = ValidadorValoresNull.getTimeSpan(reader, "FinEsperado");
            asistencia.HoraSalidaReal = ValidadorValoresNull.getTimeSpan(reader, "FinReal");
            asistencia.Id = reader.GetInt32("Id");
            asistencia.EventId = reader.GetInt32("eventId");
            asistencia.AppointmentId = reader.GetInt32("appointmentId");
            asistencia.Observaciones = ValidadorValoresNull.getString(reader, "observaciones", "");

            return asistencia;
        }

        public static List<Asistencia> obtenerAsistenciasParaListadoDeFechas(List<DateTime> fechas)
        {
            List<string> fechasFormateadas = new List<string>();

            foreach (DateTime fecha in fechas)
            {
                fechasFormateadas.Add(String.Format("{0:yyyy/MM/dd}", fecha));
            }
            return obtenerAsistenciasParaFechas(fechas);
        }

        public static List<Asistencia> obtenerAsistenciasParaUnaSemana(DateTime fecha)
        {
            DateTime inicioDeSemana = fecha.AddDays(-(int)fecha.DayOfWeek);
            inicioDeSemana = new DateTime(inicioDeSemana.Year, inicioDeSemana.Month, inicioDeSemana.Day, 0, 0, 0);

            DateTime finDeSemana = inicioDeSemana.AddDays(6);
            finDeSemana = new DateTime(finDeSemana.Year, finDeSemana.Month, finDeSemana.Day, 23, 59, 59);

            return obtenerAsistencias(inicioDeSemana, finDeSemana);
        }

        // Devuelve todos los eventos para el dia pasado como parametro
        public static List<Asistencia> obtenerAsistenciasDelDia(DateTime fecha)
        {
            // Establecemos dos dates times para poder decirle a la consulta
            // que me busque las asistencias dentro de un dia en particular entre
            // las 00:00:00 y las 23:59:59
            DateTime fechaHoraInicio;
            DateTime fechaHoraFin;

            fechaHoraInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            fechaHoraFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);

            return obtenerAsistencias(fechaHoraInicio, fechaHoraFin);
        }


        // Devuelve todos los eventos que estan en el rango de fechas pasados como parametros
        public static List<Asistencia> obtenerAsistenciasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            DateTime fechaHoraInicio;
            DateTime fechaHoraFin;

            fechaHoraInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
            fechaHoraFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);

            return obtenerAsistencias(fechaHoraInicio, fechaHoraFin);
        }

        public static List<Asistencia> obtenerAsistenciasPorAppointmentId(List<int> ids)
        {
            List<Asistencia> asistencias = new List<Asistencia>();
            if (ids.Count == 0) return asistencias;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlCommand command = new MySqlCommand();
            command.Connection = gestorConexion.getConexionAbierta();

            StringBuilder restriccionIN = new StringBuilder("IN(");

            for (int i = 0; i < ids.Count; i++)
            {
                string nuevoParametro = "@a" + i;

                restriccionIN.Append(nuevoParametro);
                restriccionIN.Append(",");

                command.Parameters.AddWithValue(nuevoParametro, ids.ElementAt(i));
            }

            restriccionIN.Remove(restriccionIN.Length - 1, 1);
            restriccionIN.Append(")");

            string consulta = obtenerSentenciaSelectSinRestricciones();
            consulta += " WHERE asistencias.APID ";
            consulta += restriccionIN.ToString();

            command.CommandText = consulta;

            MySqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Asistencia asistencia = armarAsistenciaDesdeReader(reader);
                    asistencias.Add(asistencia);
                }
            }
            catch (MySqlException e) { GestorExcepciones.mostrarExcepcion(e); }
            finally { gestorConexion.cerrarConexion(); }

            return asistencias;
        }

        public static void insertar(Asistencia asistencia)
        {
            insertar(new List<Asistencia> { asistencia });
        }

        public static void insertar(List<Asistencia> asistencias)
        {
            insertarAsistencias(asistencias);

            insertarAulasDeAsistencias(asistencias);
        }

        private static void insertarAsistencias(List<Asistencia> asistencias)
        {
            if (asistencias.Count == 0) return;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();
            MySqlTransaction transaction = connection.BeginTransaction();
            int i = 0;
            foreach (Asistencia asistencia in asistencias)
            {
                string consulta = "";
                consulta += "INSERT INTO asistencia(eventId, appointmentId, comienzoClaseEsperado, finClaseEsperado, comienzoClaseReal, ";
                consulta += "finClaseReal, cantidadAlumnos, idDocente, ";
                consulta += "idAsignatura, idEncargado, idEstadoAsistencia, idCurso) VALUES";

                string parametroEventId = "@eventId";
                string parametroAppointmentId = "@appointmentId";
                string parametroComienzoClaseEsperado = "@comienzoClaseEsperado";
                string parametroFinClaseEsperado = "@finClaseEsperado";
                string parametroComienzoClaseReal = "@comienzoClaseReal";
                string parametroFinClaseReal = "@finClaseReal";
                string parametroCantidadAlumnos = "@cantidadAlumnos";
                string parametroIdDocente = "@idDocente";
                string parametroIdAsignatura = "@idAsignatura";
                string parametroIdEncargado = "@idEncargado";
                string parametroIdEstadoAsistencia = "@idEstadoAsistencia";
                string parametroIdCurso = "@idCurso";

                consulta += "(";
                consulta += parametroEventId + ",";
                consulta += parametroAppointmentId + ",";
                consulta += parametroComienzoClaseEsperado + ",";
                consulta += parametroFinClaseEsperado + ",";
                consulta += parametroComienzoClaseReal + ",";
                consulta += parametroFinClaseReal + ",";
                consulta += parametroCantidadAlumnos + ",";
                consulta += parametroIdDocente + ",";
                consulta += parametroIdAsignatura + ",";
                consulta += parametroIdEncargado + ",";
                consulta += parametroIdEstadoAsistencia + ",";
                consulta += parametroIdCurso + ");";

                MySqlCommand comando = new MySqlCommand(consulta, connection, transaction);

                comando.Parameters.AddWithValue(parametroEventId, asistencia.EventId);
                comando.Parameters.AddWithValue(parametroAppointmentId, asistencia.AppointmentId);
                comando.Parameters.AddWithValue(parametroComienzoClaseEsperado, asistencia.Fecha.Add(asistencia.HoraEntradaEsperada));
                comando.Parameters.AddWithValue(parametroFinClaseEsperado, asistencia.Fecha.Add(asistencia.HoraSalidaEsperada));
                comando.Parameters.AddWithValue(parametroComienzoClaseReal, asistencia.Fecha.Add(asistencia.HoraEntradaReal));
                comando.Parameters.AddWithValue(parametroFinClaseReal, asistencia.Fecha.Add(asistencia.HoraSalidaReal));
                comando.Parameters.AddWithValue(parametroCantidadAlumnos, asistencia.CantidadAlumnos);
                comando.Parameters.AddWithValue(parametroIdDocente, asistencia.Docente.Id);
                comando.Parameters.AddWithValue(parametroIdAsignatura, asistencia.Asignatura.Id);

                int valorIdEncargado = configuracion.IdEncargadoNoAsignado;
                if (asistencia.Encargado != null) valorIdEncargado = asistencia.Encargado.Id;
                comando.Parameters.AddWithValue(parametroIdEncargado, valorIdEncargado);

                int valorIdEstadoAsistencia = configuracion.IdEstadoAsistenciaNoAsignado;
                if (asistencia.EstadoAsistencia != null) valorIdEstadoAsistencia = asistencia.EstadoAsistencia.Id;
                comando.Parameters.AddWithValue(parametroIdEstadoAsistencia, valorIdEstadoAsistencia);

                comando.Parameters.AddWithValue(parametroIdCurso, asistencia.Curso.Id);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    GestorExcepciones.mostrarExcepcion(e);
                }

                System.Diagnostics.Debug.WriteLine("Consulta numero: " + i); i++;
            }
            
            try { transaction.Commit(); }
            catch (MySqlException e) {
                transaction.Rollback();
                GestorExcepciones.mostrarExcepcion(e); 
            }
            finally { gestorConexion.cerrarConexion(); }
        }

        private static void insertarAulasDeAsistencia(Asistencia asistencia)
        {
            insertarAulasDeAsistencias(new List<Asistencia> { asistencia });
        }

        public static void insertarAulasDeAsistencias(List<Asistencia> asistencias)
        {
            if (asistencias.Count == 0) return;

            Dictionary<int, Asistencia> asistenciaPorAppointment = new Dictionary<int, Asistencia>();
            foreach (Asistencia asistencia in asistencias)
            {
                if (!asistenciaPorAppointment.Keys.Contains(asistencia.AppointmentId))
                {
                    asistenciaPorAppointment.Add(asistencia.AppointmentId, asistencia);
                }
            }

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = connection;

            bool insertar = false;

            string consulta = "INSERT INTO aulasPorAsistencia (idAppointmentAsistencia, idAula) VALUES";

            int i = 0;
            foreach(KeyValuePair<int, Asistencia> entrada in asistenciaPorAppointment)
            {
                //id appointment asistencia
                string parametroIdAsistencia = "@idaa" + i;
                comando.Parameters.AddWithValue(parametroIdAsistencia, entrada.Key);

                for (int j = 0; j < entrada.Value.Aulas.Count; j++)
                {
                    insertar = true;
                    //id aula
                    string parametroIdAula = "@ida" + i + "_" + j;

                    consulta += "(" + parametroIdAsistencia + "," + parametroIdAula + "),";

                    comando.Parameters.AddWithValue(parametroIdAula, entrada.Value.Aulas.ElementAt<Aula>(j).Id);
                }

                i++;
            }

            consulta = consulta.Substring(0, consulta.Length - 1);
            comando.CommandText = consulta;

            try
            {
                eliminarAulasDeAsistencias(asistencias);
                if (insertar) comando.ExecuteNonQuery();  
            }
            catch (Exception e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }

        public static void updateAsistencias(List<Asistencia> asistencias)
        {
            if (asistencias.Count == 0) return;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = connection;

            StringBuilder query = new StringBuilder();

            for (int i = 0; i < asistencias.Count; i++)
            {
                string comienzoClaseEsperado = "@comienzoClaseEsperado" + i;
                string finClaseEsperado = "@finClaseEsperado" + i;
                string comienzoClaseReal = "@comienzoClaseReal" + i;
                string finClaseReal = "@finClaseReal" + i;
                string cantidadAlumnos = "@cantidadAlumnos" + i;
                string idDocente = "@idDocente" + i;
                string idAsignatura = "@idAsignatura" + i;
                string idEncargado = "@idEncargado" + i;
                string idEstadoAsistencia = "@idEstadoAsistencia" + i;
                string idCurso = "@idCurso" + i;
                string observaciones = "@observaciones" + i;
                string idAsistencia = "@idAsistencia" + i;

                query.Append("UPDATE asistencia set ");
                query.Append("comienzoClaseEsperado = " + comienzoClaseEsperado + ", ");
                query.Append("finClaseEsperado = " + finClaseEsperado + ", ");
                query.Append("comienzoClaseReal = " + comienzoClaseReal + ", ");
                query.Append("finClaseReal = " + finClaseReal + ", ");
                query.Append("cantidadAlumnos = " + cantidadAlumnos + ", ");
                query.Append("idDocente = " + idDocente + ", ");
                query.Append("idAsignatura = " + idAsignatura + ", ");
                query.Append("idEncargado = " + idEncargado + ", ");
                query.Append("idEstadoAsistencia = " + idEstadoAsistencia + ", ");
                query.Append("idCurso = " + idCurso + ", ");
                query.Append("observaciones = " + observaciones + " ");
                query.Append("WHERE id = " + idAsistencia + ";");

                Asistencia asistencia = asistencias.ElementAt(i);
                comando.Parameters.Add(new MySqlParameter(comienzoClaseEsperado, asistencia.obtenerEntradaEsperada()));
                comando.Parameters.Add(new MySqlParameter(finClaseEsperado, asistencia.obtenerSalidaEsperada()));
                comando.Parameters.Add(new MySqlParameter(comienzoClaseReal, asistencia.obtenerEntradaReal()));
                comando.Parameters.Add(new MySqlParameter(finClaseReal, asistencia.obtenerSalidaReal()));
                comando.Parameters.Add(new MySqlParameter(cantidadAlumnos, asistencia.CantidadAlumnos));
                comando.Parameters.Add(new MySqlParameter(idDocente, asistencia.Docente.Id));
                comando.Parameters.Add(new MySqlParameter(idAsignatura, asistencia.Asignatura.Id));
                comando.Parameters.Add(new MySqlParameter(idEncargado, asistencia.Encargado.Id));
                comando.Parameters.Add(new MySqlParameter(idEstadoAsistencia, asistencia.EstadoAsistencia.Id));
                comando.Parameters.Add(new MySqlParameter(idCurso, asistencia.Curso.Id));
                comando.Parameters.Add(new MySqlParameter(observaciones, asistencia.Observaciones));
                comando.Parameters.Add(new MySqlParameter(idAsistencia, asistencia.Id));
            }

            comando.CommandText = query.ToString();

            try
            {
                comando.ExecuteNonQuery();

                foreach (Asistencia asistencia in asistencias)
                {
                    eliminarAulasDeAsistencia(asistencia);

                    if (asistencia.Aulas != null && asistencia.Aulas.Count > 0)
                    {
                        insertarAulasDeAsistencia(asistencia);
                    }
                }
            }
            catch (Exception e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }

        private static void eliminarAulasDeAsistencia(Asistencia asistencia)
        {
            eliminarAulasDeAsistencias(new List<Asistencia> { asistencia });
        }

        public static void eliminarAulasDeAsistencias(List<Asistencia> asistencias)
        {
            if (asistencias.Count == 0) return;

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            MySqlCommand comando = new MySqlCommand();

            string query = "DELETE FROM aulasPorAsistencia WHERE idAppointmentAsistencia IN(";

            int i = 0;
            foreach (Asistencia asistencia in asistencias)
            {
                string parametroIdAppointmentAsistencia = "@id" + i;
                query += parametroIdAppointmentAsistencia + ",";

                comando.Parameters.AddWithValue(parametroIdAppointmentAsistencia, asistencia.AppointmentId);

                i++;
            }

            query = query.Substring(0, query.Length - 1);
            query += ")";

            comando.CommandText = query;
            comando.Connection = gestorConexion.getConexionAbierta();

            try
            {
                comando.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }

        public static void eliminarAsistencia(Asistencia asistencia)
        {
            eliminarAsistencias(new List<Asistencia> { asistencia });
        }

        public static void eliminarAsistencias(List<Asistencia> asistencias)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();
            MySqlTransaction transaction = connection.BeginTransaction();

            string consulta = "DELETE FROM asistencia WHERE id=@id;";

            foreach (Asistencia asistencia in asistencias)
            {
                MySqlCommand comando = new MySqlCommand();
                comando.CommandText = consulta;
                comando.Connection = connection;
                comando.Transaction = transaction;

                comando.Parameters.AddWithValue("@id", asistencia.Id);
                //comando.Parameters.AddWithValue("@idAppointment", asistencia.AppointmentId);

                comando.ExecuteNonQuery();
            }
            
            try
            {
                transaction.Commit();
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }
    }
}