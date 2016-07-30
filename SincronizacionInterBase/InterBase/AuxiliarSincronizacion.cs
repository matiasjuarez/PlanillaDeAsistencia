using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;

namespace Sincronizacion
{
    // Esta clase se encarga de hacer la sincronizacion necesaria entre la base de datos del rapla y
    // la base de datos que guarda la planilla de asistencia
    public class AuxiliarSincronizacionContraEventos
    {
        private static AuxiliarSincronizacionContraEventos auxiliar;

        private AuxiliarSincronizacionContraEventos() { }

        public static AuxiliarSincronizacionContraEventos getInstance()
        {
            if (auxiliar == null)
            {
                auxiliar = new AuxiliarSincronizacionContraEventos();
            }

            return auxiliar;
        }
        // En esta lista iran los docentes que se hayan guardado en la base de datos. Se la usara para
        // evitar hacer una conexion al servidor cada vez que el usuario selecciona un dia distinto
        private List<Docente> docentes = DAODocentes.obtenerTodosLosDocentes();
        private List<Asignatura> asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
        private List<Aula> aulas = DAOAulas.obtenerTodasLasAulas();
        private List<Curso> cursos = DAOCursos.obtenerTodosLosCursos();
        //private static List<Especialidad> especialidades = DAOEspecialidades.obtenerTodasLasEspecialidades();

        public void sincronizarDatosDeSoporte(Evento evento)
        {
            sincronizarEventosContraDocentes(evento);
            sincronizarEventosContraAulas(evento);
            sincronizarEventosContraCursos(evento);
            sincronizarEventosContraAsignaturas(evento);
        }

        public DiccionarioAsistenciasPorFechaSimple obtenerAsistenciasDesdeEventosRapla(DateTime desde, DateTime hasta)
        {
            DiccionarioAsistenciasPorFechaSimple diccionario = new DiccionarioAsistenciasPorFechaSimple();

            List<Evento> eventos = DAOEventosRapla.obtenerEventosEntreFechas(desde, hasta);

            foreach (Evento evento in eventos)
            {
                Asistencia asistencia = convertirEventoEnAsistencia(evento);
                diccionario.agregarAsistencia(asistencia);
            }

            return diccionario;
        }

        private Asistencia convertirEventoEnAsistencia(Evento evento)
        {
            Asistencia asistencia = new Asistencia();

            asistencia.AppointmentId = evento.AppointmentId;
            asistencia.EventId = evento.IDEvento;

            asistencia.ComienzoClaseEsperado = evento.InicioEsperado.TimeOfDay;
            asistencia.FinClaseEsperado = evento.FinEsperado.TimeOfDay;

            while (true)
            {
                bool docenteAsignado = false;
                foreach (Docente docente in docentes)
                {
                    if (evento.Docente == docente.Nombre)
                    {
                        asistencia.Docente = docente;
                        docenteAsignado = true;
                        break;
                    }
                }

                if (!docenteAsignado)
                {
                    sincronizarEventosContraDocentes(evento);
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                bool asignaturaAsignada = false;
                foreach (Asignatura asignatura in asignaturas)
                {
                    if (evento.Materia == asignatura.Nombre)
                    {
                        asistencia.Asignatura = asignatura;
                        asignaturaAsignada = true;
                        break;
                    }
                }

                if (!asignaturaAsignada)
                {
                    sincronizarEventosContraAsignaturas(evento);
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                string[] aulasNombres = evento.Aula.Split(',');

                foreach (string aulaNombre in aulasNombres)
                {
                    bool aulaAsignada = false;
                    foreach (Aula aula in aulas)
                    {
                        if (aulaNombre == aula.Nombre)
                        {
                            asistencia.agregarAula(aula);
                            aulaAsignada = true;
                            break;
                        }
                    }

                    if (!aulaAsignada)
                    {
                        sincronizarEventosContraAulas(evento);
                    }
                }

                if (asistencia.Aulas != null && aulasNombres.Length == asistencia.Aulas.Count)
                {
                    break;
                }
            }


            while (true)
            {
                bool cursoAsignado = false;
                foreach (Curso curso in cursos)
                {
                    if (evento.Curso == curso.Nombre)
                    {
                        asistencia.Curso = curso;
                        cursoAsignado = true;
                        break;
                    }
                }

                if (!cursoAsignado)
                {
                    sincronizarEventosContraCursos(evento);
                }
                else
                {
                    break;
                }
            }

            return asistencia;
        }

        /*
         * Se fija si la asignatura que figura en el evento existe en la base de datos de la planilla de asistencias.
         * Si no existe, entonces ingresa una nueva asignatura en la base de datos. El evento se toma de la base de datos del rapla
         */
        private void sincronizarEventosContraAsignaturas(Evento evento)
        {
            Asignatura asignaturaTesteo = new Asignatura();
            asignaturaTesteo.Nombre = evento.Materia;

            foreach (Docente docente in docentes)
            {
                if (evento.JefeCatedra == docente.Nombre)
                {
                    asignaturaTesteo.JefeCatedra = docente;
                    break;
                }
            }

            bool sincronizado = false;
            bool seEncontroAsignatura = false;

            foreach (Asignatura asignatura in asignaturas)
            {
                if (asignatura.Nombre == asignaturaTesteo.Nombre)
                {
                    asignaturaTesteo.Id = asignatura.Id;
                    seEncontroAsignatura = true;
                    if (asignatura.JefeCatedra.Nombre == asignaturaTesteo.JefeCatedra.Nombre)
                    {
                        sincronizado = true;
                        break;
                    }
                }
            }

            if (!seEncontroAsignatura)
            {
                DAOAsignaturas.insertarNuevaAsignatura(asignaturaTesteo);
                asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
            }

            if (seEncontroAsignatura && !sincronizado)
            {
                DAOAsignaturas.actualizarAsignatura(asignaturaTesteo);
                asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
            }

            //return asignatura;
        }

        /*
         * Se fija si el aula que figura en el evento existe en la base de datos de la planilla de asistencias.
         * Si no existe, entonces ingresa una nueva aula en la base de datos. El evento se toma de la base de datos del rapla
         */
        private void sincronizarEventosContraAulas(Evento evento)
        {
            string[] aulasString = evento.Aula.Split(',');
            
            foreach (string aulaString in aulasString)
            {
                Aula aulaTesteo = new Aula();
                aulaTesteo.Nombre = aulaString;
                bool existeAula = false;

                foreach (Aula aula in aulas)
                {
                    if (aulaTesteo.Nombre == aula.Nombre)
                    {
                        existeAula = true;
                        break;
                    }
                }

                if (!existeAula)
                {
                    DAOAulas.insertarNuevaAula(aulaTesteo);
                    aulas = DAOAulas.obtenerTodasLasAulas();
                }
            }            
        }

        /*
         * Se fija si el docente que figura en el evento existe en la base de datos de la planilla de asistencias.
         * Tambien se fija si el docente que figura como jefe de catedra existe en la base de datos.
         * Si no existe, entonces ingresa un nuevo docente en la base de datos. El evento se toma de la base de datos del rapla.
         */
        private void sincronizarEventosContraDocentes(Evento evento)
        {
            Docente docente = new Docente(evento.Docente);

            Docente jefeCatedra = new Docente(evento.JefeCatedra);

            bool existeDocente = false;
            bool existeJefe = false;

            foreach (Docente doc in docentes)
            {
                if (doc.Nombre == docente.Nombre)
                {
                    existeDocente = true;
                }
                if (doc.Nombre == jefeCatedra.Nombre)
                {
                    existeJefe = true;
                }

                if (existeJefe && existeDocente)
                {
                    break;
                }
            }

            if (!existeDocente)
            {
                DAODocentes.insertarNuevoDocente(docente);
            }


            if (!existeJefe && docente.Nombre != jefeCatedra.Nombre)
            {
                DAODocentes.insertarNuevoDocente(jefeCatedra);
            }

            if (!existeDocente || !existeJefe)
            {
                docentes = DAODocentes.obtenerTodosLosDocentes();
            }
        }

        private void sincronizarEventosContraCursos(Evento evento)
        {
            Curso cursoTesteo = new Curso();
            cursoTesteo.Nombre = evento.Curso;

            bool seEncontroCurso = false;
            foreach (Curso curso in cursos)
            {
                if (cursoTesteo.Nombre == curso.Nombre)
                {
                    seEncontroCurso = true;
                    break;
                }
            }

            if (!seEncontroCurso)
            {
                DAOCursos.insertarCurso(cursoTesteo);
                cursos = DAOCursos.obtenerTodosLosCursos();
            }
        }
    }
}