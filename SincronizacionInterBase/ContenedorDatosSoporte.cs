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

        public void sincronizar(List<Evento> eventos)
        {
            sincronizarDocentes(eventos);
            sincronizarAulas(eventos);
            sincronizarCursos(eventos);
            sincronizarAsignaturas(eventos);
        }

        private void sincronizarAsignaturas(List<Evento> eventos)
        {
            ContenedorCambios<Asignatura> cambios = new ContenedorCambios<Asignatura>();
            HashSet<string> nombresAsignaturasNuevas = new HashSet<string>();

            foreach (Evento evento in eventos)
            {
                string asignaturaRapla = evento.Asignatura;
                if (asignaturaRapla == configuracion.AsignaturaNoAsignada)
                {
                    // Esto lo hacemos para no guardar una entrada que diga "asignatura no asignada"
                    continue;
                }

                bool seEncontroAsignatura = false;
                foreach (Asignatura asignaturaPlanilla in asignaturasPlanilla.obtenerDatos())
                {
                    if (asignaturaRapla == asignaturaPlanilla.Nombre)
                    {
                        seEncontroAsignatura = true;

                        /*
                         * Nos fijamos si el jefe de catedra que figura en la planilla para esta asignatura
                         * es el mismo jefe de catedra que el que trae el evento. Si no lo es, tenemos que 
                         * hacer un update en la asignatura correspondiente
                         * */
                        if (evento.JefeCatedra != configuracion.DocenteNoAsignado)
                        {
                            if (asignaturaPlanilla.JefeCatedra == null) asignaturaPlanilla.JefeCatedra = new Docente();

                            asignaturaPlanilla.JefeCatedra.Nombre = evento.JefeCatedra;
                            cambios.modificarValor(asignaturaPlanilla);
                        }

                        break;
                    }
                }

                if (!seEncontroAsignatura)
                {
                    if (!nombresAsignaturasNuevas.Contains(asignaturaRapla))
                    {
                        nombresAsignaturasNuevas.Add(asignaturaRapla);
                        Asignatura nuevaAsignatura = new Asignatura();
                        nuevaAsignatura.Nombre = asignaturaRapla;

                        foreach (Docente docente in docentesPlanilla.obtenerDatos())
                        {
                            if(docente.Nombre == evento.JefeCatedra){
                                nuevaAsignatura.JefeCatedra = docente;
                            }
                        }

                        cambios.agregarValor(nuevaAsignatura);
                    }
                }
            }

            if (cambios.contieneCambios())
            {
                DAOAsignaturas.insertarAsignaturas(cambios.obtenerValoresAgregar());
                DAOAsignaturas.actualizarAsignaturas(cambios.obtenerValoresModificar());
                asignaturasPlanilla.refrescarDatos();
            }
        }

        /*
         * Se fija si el aula que figura en el evento existe en la base de datos de la planilla de asistencias.
         * Si no existe, entonces ingresa una nueva aula en la base de datos. El evento se toma de la base de datos del rapla
         */
        private void sincronizarAulas(List<Evento> eventos)
        {
            HashSet<string> nombresAulasNuevas = new HashSet<string>();

            foreach (Evento evento in eventos)
            {
                if (evento.Aulas == configuracion.AulaNoAsignada)
                {
                    continue;
                }

                string[] aulasRaplaNombre = evento.Aulas.Split(',');

                foreach (string aulaRaplaNombre in aulasRaplaNombre)
                {
                    bool existeAula = false;

                    foreach (Aula aulaPlanilla in aulasPlanilla.obtenerDatos())
                    {
                        if (aulaRaplaNombre == aulaPlanilla.Nombre)
                        {
                            existeAula = true;
                            break;
                        }
                    }

                    if (!existeAula)
                    {
                        nombresAulasNuevas.Add(aulaRaplaNombre);
                    }
                }
            }

            if (nombresAulasNuevas.Count > 0)
            {
                List<string> nombresAulas = nombresAulasNuevas.ToList<string>();
                List<Aula> aulasNuevas = new List<Aula>();

                foreach(string nombreAula in nombresAulas)
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
        private void sincronizarDocentes(List<Evento> eventos)
        {
            HashSet<string> nombresDocentesNuevos = new HashSet<string>();

            foreach (Evento evento in eventos)
            {
                string docenteRapla = evento.Docente;
                string jefeCatedraRapla = evento.JefeCatedra;

                bool existeDocente = false;
                bool existeJefe = false;

                foreach (Docente docentePlanilla in docentesPlanilla.obtenerDatos())
                {
                    if (docentePlanilla.Nombre == docenteRapla)
                    {
                        existeDocente = true;
                    }
                    if (docentePlanilla.Nombre == jefeCatedraRapla)
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
                    if (docenteRapla != configuracion.DocenteNoAsignado)
                    {
                        nombresDocentesNuevos.Add(docenteRapla);
                    }
                }

                if (!existeJefe)
                {
                    if (jefeCatedraRapla != configuracion.DocenteNoAsignado)
                    {
                        nombresDocentesNuevos.Add(jefeCatedraRapla);
                    }
                }
            }

            if (nombresDocentesNuevos.Count > 0)
            {
                List<string> nombresDocentes = nombresDocentesNuevos.ToList<string>();
                List<Docente> docentesNuevos = new List<Docente>();

                foreach (string nombreDocente in nombresDocentes)
                {
                    Docente docenteNuevo = new Docente(nombreDocente);
                    docentesNuevos.Add(docenteNuevo);
                }

                DAODocentes.insertarDocentes(docentesNuevos);
                docentesPlanilla.refrescarDatos();
            }
        }

        private void sincronizarCursos(List<Evento> eventos)
        {
            HashSet<string> nombresCursosNuevos = new HashSet<string>();

            foreach (Evento evento in eventos)
            {
                string cursoRapla = evento.Curso;
                if (cursoRapla == configuracion.CursoNoAsignado)
                {
                    continue;
                }

                bool seEncontroCurso = false;

                foreach (Curso cursoPlanilla in cursosPlanilla.obtenerDatos())
                {
                    if (cursoRapla == cursoPlanilla.Nombre)
                    {
                        seEncontroCurso = true;
                        break;
                    }
                }

                if (!seEncontroCurso)
                {
                    nombresCursosNuevos.Add(cursoRapla);
                }
            }

            if (nombresCursosNuevos.Count > 0)
            {
                List<string> nombresCursos = nombresCursosNuevos.ToList<string>();
                List<Curso> cursosNuevos = new List<Curso>();

                foreach (string nombreCurso in nombresCursos)
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