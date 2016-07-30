using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace ContenedoresDeDatos
{
    public class DatosLocales
    {
        //**********IMPLEMENTACION SINGLETON
        private static DatosLocales datosLocales;

        private DatosLocales()
        {
            asignaturas = new ContenedorAsignaturas();
            asistencias = new ContenedorAsistencias();
            aulas = new ContenedorAulas();
            cursos = new ContenedorCursos();
            docentes = new ContenedorDocentes();
            estadosAsistencia = new ContenedorEstadosAsistencia();
        }

        public DatosLocales getInstance()
        {
            if (datosLocales == null) datosLocales = new DatosLocales();
            return datosLocales;
        }

        // GETERS *********************************************************************
        private ContenedorAsignaturas asignaturas;
        public ContenedorAsignaturas Asignaturas
        {
            get { return asignaturas; }
        }

        private ContenedorAsistencias asistencias;
        public ContenedorAsistencias Asistencias
        {
            get { return asistencias; }
        }

        private ContenedorAulas aulas;
        public ContenedorAulas Aulas
        {
            get { return aulas; }
        }

        private ContenedorCursos cursos;
        public ContenedorCursos Cursos
        {
            get { return cursos; }
        }

        private ContenedorDocentes docentes;
        public ContenedorDocentes Docentes
        {
            get { return docentes; }
        }

        private ContenedorEstadosAsistencia estadosAsistencia;
        public ContenedorEstadosAsistencia EstadosAsistencia
        {
            get { return estadosAsistencia; }
        }

        public void refrescarDatosSoporte()
        {
            asignaturas.refrescarDatos();
            aulas.refrescarDatos();
            cursos.refrescarDatos();
            docentes.refrescarDatos();
            estadosAsistencia.refrescarDatos();
        }
    }
}