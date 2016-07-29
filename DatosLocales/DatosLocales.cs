using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

using DatosLocales.Contenedores;

namespace DatosLocales
{
    public class DatosLocales
    {
        //**********IMPLEMENTACION SINGLETON
        private static DatosLocales datosSoporte;

        private DatosLocales()
        {
            asignaturas = new ContenedorAsignaturas();
            aulas = new ContenedorAulas();
            asistencias = new ContenedorAsistencias();
            cursos = new ContenedorCursos();
            docentes = new ContenedorDocentes();
            estadosAsistencia = new ContenedorEstadosAsistencia();
        }

        public DatosLocales getInstance()
        {
            if (datosSoporte == null) datosSoporte = new DatosLocales();
            return datosSoporte;
        }

        // GETERS *********************************************************************
        private ContenedorAsignaturas asignaturas;
        public ContenedorAsignaturas Asignaturas
        {
            get { return asignaturas; }
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

        private ContenedorAsistencias asistencias;
        public ContenedorAsistencias Asistencias
        {
            get { return asistencias; }
        }

        public void refrescarContenedor()
        {
            asignaturas.refrescarDatos();
            aulas.refrescarDatos();
            cursos.refrescarDatos();
            docentes.refrescarDatos();
            estadosAsistencia.refrescarDatos();
        }
    }
}