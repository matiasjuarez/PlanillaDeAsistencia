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

            StringBuilder consultaBuilder = new StringBuilder(
                    obtenerSentenciaSelectSinRestriccion() +
                    " where asistencias.comienzoClaseEsperado BETWEEN @fechaInicio and @fechaFin"
             );

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            MySqlParameter fechaInicioParam = new MySqlParameter();
            fechaInicioParam.ParameterName = "@fechaInicio";
            fechaInicioParam.Value = fechaHoraInicio;

            MySqlParameter fechaFinParam = new MySqlParameter();
            fechaFinParam.ParameterName = "@fechaFin";
            fechaFinParam.Value = fechaHoraFin;


            // Agregamos los parametros a la consulta
            comando.Parameters.Add(fechaInicioParam);
            comando.Parameters.Add(fechaFinParam);

            // Ejecutamos la consulta
            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Asistencia asistencia = armarAsistenciaConEntradaDeReader(reader);
                    asistencias.Add(asistencia);
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

            return asistencias;
        }

        private static List<Asistencia> obtenerAsistencias(RestriccionIN restriccion)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);

            StringBuilder consultaBuilder = new StringBuilder();
            consultaBuilder.Append(obtenerSentenciaSelectSinRestriccion());
            consultaBuilder.Append(" WHERE ");
            consultaBuilder.Append(restriccion.obtenerRestriccion());

            string consulta = consultaBuilder.ToString();

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consulta;
            comando.Connection = gestorConexion.getConexionAbierta();

            // Creamos sus parametros
            restriccion.obtenerComandoParametrizado(comando);

            // Ejecutamos la consulta
            MySqlDataReader reader = comando.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Asistencia asistencia = armarAsistenciaConEntradaDeReader(reader);
                    asistencias.Add(asistencia);
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

            return asistencias;
        }

        // Obtiene la sentencia Select que se debe utilizar para obtener todas las asistencias de la base de datos
        private static string obtenerSentenciaSelectSinRestriccion()
        {
            StringBuilder consultaBuilder = new StringBuilder(
                    "select asistencias.asistenciaId as Id, asistencias.APID as AppointmentId, asistencias.eventId, " +
                    "asistencias.comienzoClaseEsperado as InicioEsperado, " +
                    "asistencias.finClaseEsperado as FinEsperado, asistencias.comienzoClaseReal as InicioReal, " +
                    "asistencias.finClaseReal as FinReal, asistencias.cantidadAlumnos as Alumnos, " +
                    "asistencias.encargadoNombre as Encargado, asistencias.docenteNombre, " +
                    "asistencias.docenteId, asistencias.asignaturaNombre, asistencias.asignaturaId, " +
                    "asistencias.estadoAsistenciaNombre, asistencias.estadoAsistenciaId, " +
                    "asistencias.cursoId, asistencias.cursoNombre, aulas.aulasId, aulas.aulasNombre FROM ( " +
                    "select asistencia.id as asistenciaId, asistencia.appointmentId as APID, asistencia.eventId as eventId, " +
                    "comienzoClaseEsperado, finClaseEsperado, " +
                    "comienzoClaseReal, finClaseReal, cantidadAlumnos, " +
                    "(encargado.nombre + ' ' + encargado.apellido) as encargadoNombre, " +
                    "docente.nombre as docenteNombre, docente.id as docenteId, " +
                    "asignatura.nombre as asignaturaNombre, asignatura.id as asignaturaId, " +
                    "estadoasistencia.nombre as estadoAsistenciaNombre, estadoasistencia.id as estadoAsistenciaId, " +
                    "curso.id as cursoId, curso.nombre as cursoNombre " +
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
                    "ON aulas.idAsistencia = asistencias.asistenciaId "
             );

            return consultaBuilder.ToString();
        }

        // Toma un objeto de tipo MySqlDataReader y teniendo en cuenta la posicion del puntero del mismo
        // arma una asistencia.
        private static Asistencia armarAsistenciaConEntradaDeReader(MySqlDataReader reader)
        {
            Asistencia asistencia = new Asistencia();
            Docente docente = new Docente();
            Asignatura asignatura = new Asignatura();
            Encargado encargado = new Encargado();
            Curso curso = new Curso();
            Especialidad especialidad = new Especialidad();
            EstadoAsistencia estadoAsistencia = new EstadoAsistencia();

            docente.Nombre = ValidadorValoresNull.getString(reader, "docenteNombre");
            docente.Id = ValidadorValoresNull.getInt(reader, "docenteId");

            asignatura.Nombre = ValidadorValoresNull.getString(reader, "asignaturaNombre");
            asignatura.Id = ValidadorValoresNull.getInt(reader, "asignaturaId");

            encargado.Nombre = ValidadorValoresNull.getString(reader, "encargado");

            curso.Id = ValidadorValoresNull.getInt(reader, "cursoId");
            curso.Nombre = ValidadorValoresNull.getString(reader, "cursoNombre");

            string nombreAulas = ValidadorValoresNull.getString(reader, "aulasNombre");
            string[] listaNombresAulas = nombreAulas.Split(',');

            string idAulas = ValidadorValoresNull.getString(reader, "aulasId");
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

            estadoAsistencia.Id = ValidadorValoresNull.getInt(reader, "estadoAsistenciaId");
            estadoAsistencia.Nombre = ValidadorValoresNull.getString(reader, "estadoAsistenciaNombre");

            asistencia.Asignatura = asignatura;
            asistencia.Curso = curso;
            asistencia.Docente = docente;
            asistencia.Encargado = encargado;
            asistencia.EstadoAsistencia = estadoAsistencia;
            asistencia.CantidadAlumnos = ValidadorValoresNull.getInt(reader, "alumnos");
            asistencia.ComienzoClaseEsperado = ValidadorValoresNull.getDateTime(reader, "InicioEsperado");
            asistencia.ComienzoClaseReal = ValidadorValoresNull.getDateTime(reader, "InicioReal");
            asistencia.FinClaseEsperado = ValidadorValoresNull.getDateTime(reader, "FinEsperado");
            asistencia.FinClaseReal = ValidadorValoresNull.getDateTime(reader, "FinReal");
            asistencia.Id = ValidadorValoresNull.getInt(reader, "Id");
            asistencia.EventId = ValidadorValoresNull.getInt(reader, "eventId");
            asistencia.AppointmentId = ValidadorValoresNull.getInt(reader, "appointmentId");

            return asistencia;
        }

        public static List<Asistencia> obtenerAsistenciasParaListadoDeFechas(List<DateTime> fechas)
        {
            List<string> fechasFormateadas = new List<string>();

            foreach (DateTime fecha in fechas)
            {
                fechasFormateadas.Add(String.Format("{0:yyyy/MM/dd}", fecha));
            }

            RestriccionIN restriccion = new RestriccionIN("DATE(asistencias.comienzoClaseEsperado)", fechasFormateadas);
            return obtenerAsistencias(restriccion);
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

            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = consultaPrincipal;
            MySqlTransaction transaction = connection.BeginTransaction();
            comando.Transaction = transaction;
            comando.Connection = connection;

            // Creamos sus parametros
            MySqlParameter eventId = new MySqlParameter();
            eventId.ParameterName = "@eventId";
            eventId.Value = asistencia.EventId;

            MySqlParameter appointmentId = new MySqlParameter();
            appointmentId.ParameterName = "@appointmentId";
            appointmentId.Value = asistencia.AppointmentId;

            MySqlParameter comienzoClaseEsperadoParam = new MySqlParameter();
            comienzoClaseEsperadoParam.ParameterName = "@comienzoClaseEsperado";
            comienzoClaseEsperadoParam.Value = asistencia.ComienzoClaseEsperado;

            MySqlParameter finClaseEsperadoParam = new MySqlParameter();
            finClaseEsperadoParam.ParameterName = "@finClaseEsperado";
            finClaseEsperadoParam.Value = asistencia.FinClaseEsperado;

            MySqlParameter comienzoClaseRealParam = new MySqlParameter();
            comienzoClaseRealParam.ParameterName = "@comienzoClaseReal";
            comienzoClaseRealParam.Value = asistencia.ComienzoClaseReal;

            MySqlParameter finClaseRealParam = new MySqlParameter();
            finClaseRealParam.ParameterName = "@finClaseReal";
            finClaseRealParam.Value = asistencia.FinClaseReal;

            MySqlParameter cantidadAlumnosParam = new MySqlParameter();
            cantidadAlumnosParam.ParameterName = "@cantidadAlumnos";
            cantidadAlumnosParam.Value = asistencia.CantidadAlumnos;

            MySqlParameter idDocenteParam = new MySqlParameter();
            idDocenteParam.ParameterName = "@idDocente";
            idDocenteParam.Value = asistencia.Docente.Id;

            MySqlParameter idAsignaturaParam = new MySqlParameter();
            idAsignaturaParam.ParameterName = "@idAsignatura";
            idAsignaturaParam.Value = asistencia.Asignatura.Id;

            MySqlParameter idEncargadoParam = new MySqlParameter();
            idEncargadoParam.ParameterName = "@idEncargado";
            idEncargadoParam.Value = configuracion.DefaultEncargadoAsistencia.IdEntrada;
            if(asistencia.Encargado != null) idEncargadoParam.Value = asistencia.Encargado.Id;

            MySqlParameter idEstadoAsistenciaParam = new MySqlParameter();
            idEstadoAsistenciaParam.ParameterName = "@idEstadoAsistencia";
            idEstadoAsistenciaParam.Value = configuracion.DefaultEstadoAsistencia.IdEntrada;
            if(asistencia.EstadoAsistencia != null) idEstadoAsistenciaParam.Value = asistencia.EstadoAsistencia.Id;

            MySqlParameter idCursoParam = new MySqlParameter();
            idCursoParam.ParameterName = "@idCurso";
            idCursoParam.Value = asistencia.Curso.Id;

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(comienzoClaseEsperadoParam);
            comando.Parameters.Add(finClaseEsperadoParam);
            comando.Parameters.Add(comienzoClaseRealParam);
            comando.Parameters.Add(finClaseRealParam);
            comando.Parameters.Add(cantidadAlumnosParam);
            comando.Parameters.Add(idDocenteParam);
            comando.Parameters.Add(idAsignaturaParam);
            comando.Parameters.Add(idEncargadoParam);
            comando.Parameters.Add(idEstadoAsistenciaParam);
            comando.Parameters.Add(idCursoParam);
            comando.Parameters.Add(appointmentId);
            comando.Parameters.Add(eventId);

            try
            {
                int id = Convert.ToInt32(comando.ExecuteScalar());
                asistencia.Id = id;
                bool seInsertaronAulas = insertarAulasDeAsistencia(asistencia, comando);

                if (seInsertaronAulas)
                {
                    transaction.Commit();
                }
                else
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (MySqlException e)
                    {
                        GestorExcepciones.mostrarExcepcion(e);
                    }
                }
            }
            catch (MySqlException e)
            {
                comando.Transaction.Rollback();
                GestorExcepciones.mostrarExcepcion(e);
            }
            finally
            {
                gestorConexion.cerrarConexion();
            }

        }

        private static bool insertarAulasDeAsistencia(Asistencia asistencia, MySqlCommand comando)
        {
            if (asistencia.Aulas.Count > 1)
            {
                int asd = 2;
            }
            if (comando == null)
            {
                comando = new MySqlCommand();
            }

            comando.Parameters.AddWithValue("@idAsistenciaa", "");
            comando.Parameters.AddWithValue("@idAula", "");

            try
            {
                foreach (Aula aula in asistencia.Aulas)
                {
                    string insertAula = "INSERT INTO aulasPorAsistencia " +
                         "(idAsistencia, idAula) VALUES(@idAsistenciaa, @idAula)";

                    comando.CommandText = insertAula;
                    comando.Parameters["@idAsistenciaa"].Value = asistencia.Id;
                    comando.Parameters["@idAula"].Value = aula.Id;

                    comando.ExecuteNonQuery();
                }
                return true;
            }
            catch(Exception e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                return false;
            }
        }

        public static bool updateAsistencias(List<Asistencia> asistencias)
        {
            GestorConexion gestorConexion = new GestorConexion(GestorConexion.ConexionPlanillaAsistencia);
            MySqlConnection connection = gestorConexion.getConexionAbierta();
            MySqlTransaction transaction = connection.BeginTransaction();

            StringBuilder consultaBuilder = new StringBuilder(
                        "UPDATE asistencia set " +
                        "comienzoClaseEsperado = @comienzoClaseEsperado, " +
                        "finClaseEsperado = @finClaseEsperado, " +
                        "comienzoClaseReal = @comienzoClaseReal, " +
                        "finClaseReal = @finClaseReal, " +
                        "cantidadAlumnos = @cantidadAlumnos, " +
                        "idDocente = @idDocente, " +
                        "idAsignatura = @idAsignatura, " +
                        "idEncargado = @idEncargado, " +
                        "idEstadoAsistencia = @idEstadoAsistencia, " +
                        "idCurso = @idCurso, " +
                        "observaciones = @observaciones " +
                        "WHERE id = @idAsistencia"
                        );

            string consulta = consultaBuilder.ToString();


            // Creamos el comando sql
            MySqlCommand comando = new MySqlCommand();
            comando.Transaction = transaction;
            comando.CommandText = consulta;
            comando.Connection = connection;

            // Creamos sus parametros
            MySqlParameter comienzoClaseEsperadoParam = new MySqlParameter();
            comienzoClaseEsperadoParam.ParameterName = "@comienzoClaseEsperado";

            MySqlParameter finClaseEsperadoParam = new MySqlParameter();
            finClaseEsperadoParam.ParameterName = "@finClaseEsperado";

            MySqlParameter comienzoClaseRealParam = new MySqlParameter();
            comienzoClaseRealParam.ParameterName = "@comienzoClaseReal";

            MySqlParameter finClaseRealParam = new MySqlParameter();
            finClaseRealParam.ParameterName = "@finClaseReal";

            MySqlParameter cantidadAlumnosParam = new MySqlParameter();
            cantidadAlumnosParam.ParameterName = "@cantidadAlumnos";

            MySqlParameter idDocenteParam = new MySqlParameter();
            idDocenteParam.ParameterName = "@idDocente";

            MySqlParameter idAsignaturaParam = new MySqlParameter();
            idAsignaturaParam.ParameterName = "@idAsignatura";

            MySqlParameter idEncargadoParam = new MySqlParameter();
            idEncargadoParam.ParameterName = "@idEncargado";

            MySqlParameter idEstadoAsistenciaParam = new MySqlParameter();
            idEstadoAsistenciaParam.ParameterName = "@idEstadoAsistencia";

            MySqlParameter idCursoParam = new MySqlParameter();
            idCursoParam.ParameterName = "@idCurso";

            MySqlParameter idAsistenciaParam = new MySqlParameter();
            idAsistenciaParam.ParameterName = "@idAsistencia";

            MySqlParameter observacionesParam = new MySqlParameter();
            observacionesParam.ParameterName = "@observaciones";

            // Agregamos los parametros a la consulta
            comando.Parameters.Add(comienzoClaseEsperadoParam);
            comando.Parameters.Add(finClaseEsperadoParam);
            comando.Parameters.Add(comienzoClaseRealParam);
            comando.Parameters.Add(finClaseRealParam);
            comando.Parameters.Add(cantidadAlumnosParam);
            comando.Parameters.Add(idDocenteParam);
            comando.Parameters.Add(idAsignaturaParam);
            comando.Parameters.Add(idEncargadoParam);
            comando.Parameters.Add(idEstadoAsistenciaParam);
            comando.Parameters.Add(idCursoParam);
            comando.Parameters.Add(idAsistenciaParam);
            comando.Parameters.Add(observacionesParam);

            try
            {
                // Ejecutamos la consulta
                for (int i = 0; i < asistencias.Count; i++)
                {
                    Asistencia asistencia = asistencias.ElementAt(i);

                    comienzoClaseEsperadoParam.Value = asistencia.ComienzoClaseEsperado;
                    finClaseEsperadoParam.Value = asistencia.FinClaseEsperado;
                    comienzoClaseRealParam.Value = asistencia.ComienzoClaseReal;
                    finClaseRealParam.Value = asistencia.FinClaseReal;
                    cantidadAlumnosParam.Value = asistencia.CantidadAlumnos;
                    idDocenteParam.Value = asistencia.Docente.Id;
                    idAsignaturaParam.Value = asistencia.Asignatura.Id;

                    idEncargadoParam.Value = configuracion.DefaultEncargadoAsistencia.IdEntrada;
                    if(asistencia.Encargado != null) idEncargadoParam.Value = asistencia.Encargado.Id;

                    idEstadoAsistenciaParam.Value = asistencia.EstadoAsistencia.Id;
                    idCursoParam.Value = asistencia.Curso.Id;
                    idAsistenciaParam.Value = asistencia.Id;
                    observacionesParam.Value = asistencia.Observaciones;

                    comando.ExecuteNonQuery();

                    string queryBorrarAulas = "DELETE FROM AulasPorAsistencia WHERE idAsistencia=@idAsistenciaEliminar";
                    comando.CommandText = queryBorrarAulas;
                    comando.Parameters.AddWithValue("@idAsistenciaEliminar", asistencia.Id);

                    comando.ExecuteNonQuery();

                    insertarAulasDeAsistencia(asistencia, comando);
                }

                transaction.Commit();
                return true;
            }
            catch (MySqlException e)
            {
                GestorExcepciones.mostrarExcepcion(e);
                transaction.Rollback();
                return false;
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

            // Creamos sus parametros
            MySqlParameter idParam = new MySqlParameter();
            idParam.ParameterName = "@id";
            idParam.Value = asistencia.Id;

            comando.Parameters.Add(idParam);

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
