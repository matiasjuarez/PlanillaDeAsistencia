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
                    "SELECT idAsistencia, group_concat(idAula) aulasId, group_concat(a.nombre) aulasNombre " +
                    "FROM aulasporasistencia axa " +
                    "join aula a on a.id = axa.idAula " +
                    "group by axa.idAsistencia " +
                    ") as aulas " +
                    "ON aulas.idAsistencia = asistencias.asistenciaId";

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

            docente.Nombre = ValidadorValoresNull.getString(reader, "docenteNombre", configuracion.DocenteNoAsignado);
            docente.Id = ValidadorValoresNull.getInt(reader, "docenteId", configuracion.IdDocenteNoAsignado);

            asignatura.Nombre = ValidadorValoresNull.getString(reader, "asignaturaNombre", configuracion.AsignaturaNoAsignada);
            asignatura.Id = ValidadorValoresNull.getInt(reader, "asignaturaId", configuracion.IdAsignaturaNoAsignada);

            encargado.Nombre = ValidadorValoresNull.getString(reader, "encargado", configuracion.EncargadoNoAsignado);
            encargado.Id = ValidadorValoresNull.getInt(reader, "idEncargado", configuracion.IdEncargadoNoAsignado);

            curso.Id = ValidadorValoresNull.getInt(reader, "cursoId", configuracion.IdCursoNoAsignado);
            curso.Nombre = ValidadorValoresNull.getString(reader, "cursoNombre", configuracion.CursoNoAsignado);

            string nombreAulas = ValidadorValoresNull.getString(reader, "aulasNombre", configuracion.AulaNoAsignada);
            string[] listaNombresAulas = nombreAulas.Split(',');

            string idAulas = ValidadorValoresNull.getString(reader, "aulasId", "");
            string[] listaIdsAulas = idAulas.Split(',');

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
            estadoAsistencia.Nombre = ValidadorValoresNull.getString(reader, "estadoAsistenciaNombre", configuracion.EstadoAsistenciaNoAsignado);

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

        public static void insertarNuevaAsistencia(Asistencia asistencia)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();

            StringBuilder consultaBuilder = new StringBuilder();
                        consultaBuilder.Append("INSERT INTO asistencia(eventId, appointmentId, comienzoClaseEsperado, finClaseEsperado, comienzoClaseReal, ");
                        consultaBuilder.Append("finClaseReal, cantidadAlumnos, idDocente, ");
                        consultaBuilder.Append("idAsignatura, idEncargado, idEstadoAsistencia, idCurso) ");
                        consultaBuilder.Append("VALUES(@eventId, @appointmentId, @comienzoClaseEsperado, @finClaseEsperado, @comienzoClaseReal, ");
                        consultaBuilder.Append("@finClaseReal, @cantidadAlumnos, @idDocente, @idAsignatura, ");
                        consultaBuilder.Append("@idEncargado, @idEstadoAsistencia, @idCurso);");
                        consultaBuilder.Append("select last_insert_id();");

            string consultaPrincipal = consultaBuilder.ToString();

            MySqlTransaction transaction = connection.BeginTransaction();

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consultaPrincipal;
            comando.Connection = connection;
            comando.Transaction = transaction;

            comando.Parameters.AddWithValue("@eventId", asistencia.EventId);
            comando.Parameters.AddWithValue("@appointmentId", asistencia.AppointmentId);
            comando.Parameters.AddWithValue("@comienzoClaseEsperado", asistencia.Fecha.Add(asistencia.HoraEntradaEsperada));
            comando.Parameters.AddWithValue("@finClaseEsperado", asistencia.Fecha.Add(asistencia.HoraSalidaEsperada));
            comando.Parameters.AddWithValue("@comienzoClaseReal", asistencia.Fecha.Add(asistencia.HoraEntradaReal));
            comando.Parameters.AddWithValue("@finClaseReal", asistencia.Fecha.Add(asistencia.HoraSalidaReal));
            comando.Parameters.AddWithValue("@cantidadAlumnos", asistencia.CantidadAlumnos);
            comando.Parameters.AddWithValue("@idDocente", asistencia.Docente.Id);
            comando.Parameters.AddWithValue("@idAsignatura", asistencia.Asignatura.Id);

            int valorIdEncargado = configuracion.IdEncargadoNoAsignado;
            if (asistencia.Encargado != null) valorIdEncargado = asistencia.Encargado.Id;
            comando.Parameters.AddWithValue("@idEncargado", valorIdEncargado);

            int valorIdEstadoAsistencia = configuracion.IdEstadoAsistenciaNoAsignado;
            if (asistencia.EstadoAsistencia != null) valorIdEstadoAsistencia = asistencia.EstadoAsistencia.Id;
            comando.Parameters.AddWithValue("@idEstadoAsistencia", valorIdEstadoAsistencia);

            comando.Parameters.AddWithValue("@idCurso", asistencia.Curso.Id);

            try
            {
                int id = Convert.ToInt32(comando.ExecuteScalar());
                transaction.Commit();

                asistencia.Id = id;

                if (asistencia.Aulas != null && asistencia.Aulas.Count > 0)
                {
                    insertarAulasDeAsistencia(asistencia);
                }

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

        private static void insertarAulasDeAsistencia(Asistencia asistencia)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();

            MySqlCommand comando = new MySqlCommand();
            MySqlTransaction transaction = connection.BeginTransaction();
            comando.Connection = connection;
            comando.Transaction = transaction;

            StringBuilder textoInsert = new StringBuilder();
            try
            {
                for (int i = 0; i < asistencia.Aulas.Count; i++)
                {
                    string parametroIdAsistencia = "@idAsistencia" + i;
                    string parametroIdAula = "@idAula" + i;

                    textoInsert.Append("INSERT INTO aulasPorAsistencia ");
                    textoInsert.Append("(idAsistencia, idAula) VALUES(" );
                    textoInsert.Append(parametroIdAsistencia + ", ");
                    textoInsert.Append(parametroIdAula + ");");

                    comando.Parameters.AddWithValue(parametroIdAsistencia, asistencia.Id);
                    comando.Parameters.AddWithValue(parametroIdAula, asistencia.Aulas.ElementAt<Aula>(i).Id);
                }

                comando.CommandText = textoInsert.ToString();
                comando.ExecuteNonQuery();

                transaction.Commit();
            }
            catch(Exception e)
            {
                transaction.Rollback();
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
            MySqlTransaction transaction = connection.BeginTransaction();

            MySqlCommand comando = new MySqlCommand();
            comando.Transaction = transaction;
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
                transaction.Commit();

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
                transaction.Rollback();
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }
        }

        private static void eliminarAulasDeAsistencia(Asistencia asistencia)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlTransaction transaction = gestorConexion.getConexionAbierta().BeginTransaction();

            string query = "DELETE FROM aulasPorAsistencia WHERE idAsistencia=@id;";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = query;
            comando.Connection = gestorConexion.getConexionAbierta();
            comando.Transaction = transaction;

            comando.Parameters.AddWithValue("@id", asistencia.Id);

            try
            {
                comando.ExecuteNonQuery();
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

        public static void eliminarAsistencia(Asistencia asistencia)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlTransaction transaction = gestorConexion.getConexionAbierta().BeginTransaction();

            string consulta = "DELETE FROM asistencia WHERE id=@id;" +
                              "DELETE FROM aulasPorAsistencia WHERE idAsistencia=@id;";

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();
            comando.Transaction = transaction;

            comando.Parameters.AddWithValue("@id", asistencia.Id);

            try
            {
                comando.ExecuteNonQuery();
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