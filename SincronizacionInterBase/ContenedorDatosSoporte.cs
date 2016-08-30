using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using ContenedoresDeDatos;
using Configuracion;
namespace SincronizacionInterBase
{
    // Esta clase se encarga de hacer la sincronizacion necesaria entre la base de datos del rapla y
    // la base de datos que guarda la planilla de asistencia
    public class ContenedorDatosSoporte
    {
        private static Config configuracion = Config.getInstance();

        private static ContenedorDatosSoporte contenedor;

        private ContenedorDocentes docentesPlanilla;
        private ContenedorAsignaturas asignaturasPlanilla;
        private ContenedorAulas aulasPlanilla;
        private ContenedorCursos cursosPlanilla;

        private ContenedorDatosSoporte() 
        {
            docentesPlanilla = new ContenedorDocentes();
            asignaturasPlanilla = new ContenedorAsignaturas();
            aulasPlanilla = new ContenedorAulas();
            cursosPlanilla = new ContenedorCursos();

            docentesPlanilla.refrescarDatos();
            asignaturasPlanilla.refrescarDatos();
            aulasPlanilla.refrescarDatos();
            cursosPlanilla.refrescarDatos();
        }

        public static ContenedorDatosSoporte getInstance()
        {
            if (contenedor == null)
            {
                contenedor = new ContenedorDatosSoporte();
            }

            return contenedor;
        }

        public List<Docente> obtenerDocentes()
        {
            return docentesPlanilla.obtenerDatos();
        }

        public List<Asignatura> obtenerAsignaturas()
        {
            return asignaturasPlanilla.obtenerDatos();
        }

        public List<Aula> obtenerAulas()
        {
            return aulasPlanilla.obtenerDatos();
        }

        public List<Curso> obtenerCursos()
        {
            return cursosPlanilla.obtenerDatos();
        }

        public void sincronizar(List<Appointment> eventos)
        {
            sincronizarDocentes(eventos);
            sincronizarAulas(eventos);
            sincronizarCursos(eventos);
            sincronizarAsignaturas(eventos);
        }

        private void sincronizarAsignaturas(List<Appointment> appointments)
        {
            HashSet<string> nombresAsignaturasNuevas = new HashSet<string>();

            HashSet<string> nombresAsignaturas = new HashSet<string>();
            foreach (Asignatura asignatura in asignaturasPlanilla.obtenerDatos())
            {
                nombresAsignaturas.Add(asignatura.Nombre);
            }

            foreach (Appointment appointment in appointments)
            {
                string asignaturaRapla = appointment.Asignatura;
                if (asignaturaRapla == null) continue;

                if (!nombresAsignaturas.Contains(asignaturaRapla)) nombresAsignaturasNuevas.Add(asignaturaRapla);
            }

            if (nombresAsignaturasNuevas.Count > 0)
            {
                List<Asignatura> asignaturas = new List<Asignatura>();

                foreach (string nombreAsignatura in nombresAsignaturasNuevas.ToList<string>())
                {
                    Asignatura asignaturaNueva = new Asignatura();
                    asignaturaNueva.Nombre = nombreAsignatura;
                    asignaturas.Add(asignaturaNueva);
                }
                DAOAsignaturas.insertarAsignaturas(asignaturas);

                asignaturasPlanilla.refrescarDatos();
            }

            sincronizarJefesCatedraAsignaturas(appointments);
        }

        private void sincronizarJefesCatedraAsignaturas(List<Appointment> appointments)
        {
            Dictionary<string, Appointment> appointmentsMasRecientesPorMateria = obtenerAppointmentMasRecientePorMateria(appointments);

            List<Asignatura> asignaturasModificadas = new List<Asignatura>();

            foreach (Asignatura asignatura in asignaturasPlanilla.obtenerDatos())
            {
                Appointment appointmentDelDiccionario = null;
                if (appointmentsMasRecientesPorMateria.TryGetValue(asignatura.Nombre, out appointmentDelDiccionario))
                {
                    if (appointmentDelDiccionario.JefeCatedra == null) continue;

                    foreach (Docente docente in docentesPlanilla.obtenerDatos())
                    {
                        if (docente.Nombre == appointmentDelDiccionario.JefeCatedra)
                        {
                            if (asignatura.JefeCatedra == null || asignatura.JefeCatedra.Nombre != docente.Nombre)
                            {
                                asignatura.JefeCatedra = docente;
                                asignaturasModificadas.Add(asignatura);
                            }

                            break;
                        }
                    }
                }
            }

            if (asignaturasModificadas.Count != 0)
            {
                DAOAsignaturas.actualizarAsignaturas(asignaturasModificadas);
            }
        }

        private Dictionary<string, Appointment> obtenerAppointmentMasRecientePorMateria(List<Appointment> appointments)
        {
            Dictionary<string, Appointment> appointmentsPorMateria = new Dictionary<string, Appointment>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Asignatura == null)
                {
                    continue;
                }

                Appointment appointmentDelDiccionario = null;
                if (appointmentsPorMateria.TryGetValue(appointment.Asignatura, out appointmentDelDiccionario))
                {
                    // Si se cumple esta condicion, significa que el appointment que esta en el diccionario
                    // tiene una fecha mas vieja que el appointmennt que estamos analizando ahora mismo. Lo que queremos
                    // es obtener al appointment mas nuevo de una materia ya que supuestamente ese appointment deberia
                    // tener al actual jefe de catedra
                    if (appointmentDelDiccionario.Inicio < appointment.Inicio && appointment.JefeCatedra != null)
                    {
                        appointmentsPorMateria[appointment.Asignatura] = appointment;
                    }
                }
                else
                {
                    appointmentsPorMateria.Add(appointment.Asignatura, appointment);
                }
            }

            return appointmentsPorMateria;
        }

        /*
         * Se fija si el aula que figura en el evento existe en la base de datos de la planilla de asistencias.
         * Si no existe, entonces ingresa una nueva aula en la base de datos. El evento se toma de la base de datos del rapla
         */
        private void sincronizarAulas(List<Appointment> eventos)
        {
            HashSet<string> nombresAulasNuevas = new HashSet<string>();

            HashSet<string> nombresAulas = new HashSet<string>();
            foreach (Aula aula in aulasPlanilla.obtenerDatos())
            {
                nombresAulas.Add(aula.Nombre);
            }

            foreach (Appointment evento in eventos)
            {
                if (evento.Aulas == null || evento.Aulas == "") continue;

                string[] aulasRaplaNombre = evento.Aulas.Split(',');

                foreach (string aulaRaplaNombre in aulasRaplaNombre)
                {
                    if (!nombresAulas.Contains(aulaRaplaNombre)) nombresAulasNuevas.Add(aulaRaplaNombre);
                }
            }

            if (nombresAulasNuevas.Count > 0)
            {
                List<Aula> aulasNuevas = new List<Aula>();

                foreach(string nombreAula in nombresAulasNuevas.ToList<string>())
                {
                    Aula aulaNueva = new Aula();
                    aulaNueva.Nombre = nombreAula;
                    aulasNuevas.Add(aulaNueva);
                }

                DAOAulas.insertarAulas(aulasNuevas);
                aulasPlanilla.refrescarDatos();
            }            
        }

        /*
         * Se fija si el docente que figura en el evento existe en la base de datos de la planilla de asistencias.
         * Tambien se fija si el docente que figura como jefe de catedra existe en la base de datos.
         * Si no existe, entonces ingresa un nuevo docente en la base de datos. El evento se toma de la base de datos del rapla.
         */
        private void sincronizarDocentes(List<Appointment> appointments)
        {
            HashSet<string> nombresDocentesNuevos = new HashSet<string>();

            HashSet<string> nombresDocentes = new HashSet<string>();
            foreach (Docente docente in docentesPlanilla.obtenerDatos()) 
            {
                nombresDocentes.Add(docente.Nombre);
            }

            foreach (Appointment appointment in appointments)
            {
                string docenteRapla = appointment.Docente;
                string jefeCatedraRapla = appointment.JefeCatedra;

                if (!nombresDocentes.Contains(docenteRapla) && docenteRapla != null)
                {
                    nombresDocentesNuevos.Add(docenteRapla);
                }
                    
                if (!nombresDocentes.Contains(jefeCatedraRapla) && jefeCatedraRapla != null)
                {
                    nombresDocentesNuevos.Add(jefeCatedraRapla);
                }
            }

            if (nombresDocentesNuevos.Count > 0)
            {
                List<Docente> docentesNuevos = new List<Docente>();

                foreach (string nombreDocente in nombresDocentesNuevos.ToList<string>())
                {
                    Docente docenteNuevo = new Docente(nombreDocente);
                    docentesNuevos.Add(docenteNuevo);
                }

                DAODocentes.insertarDocentes(docentesNuevos);
                docentesPlanilla.refrescarDatos();
            }
        }

        private void sincronizarCursos(List<Appointment> eventos)
        {
            HashSet<string> nombresCursosNuevos = new HashSet<string>();

            HashSet<string> nombresCursos = new HashSet<string>();
            foreach (Curso curso in cursosPlanilla.obtenerDatos())
            {
                nombresCursos.Add(curso.Nombre);
            }

            foreach (Appointment evento in eventos)
            {
                string cursoRapla = evento.Curso;

                if (cursoRapla == null) continue;

                if (!nombresCursos.Contains(cursoRapla)) nombresCursosNuevos.Add(cursoRapla);
            }

            if (nombresCursosNuevos.Count > 0)
            {
                List<Curso> cursosNuevos = new List<Curso>();

                foreach (string nombreCurso in nombresCursosNuevos.ToList<string>())
                {
                    Curso cursoNuevo = new Curso();
                    cursoNuevo.Nombre = nombreCurso;
                    cursosNuevos.Add(cursoNuevo);
                }

                DAOCursos.insertarCursos(cursosNuevos);
                cursosPlanilla.refrescarDatos();
            }
        }
    }
}