using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

using EstructurasDeDatos;

namespace DatosLocales
{
    public class DatosSoporte
    {
        //**********IMPLEMENTACION SINGLETON
        private static DatosSoporte datosLocales;

        private DatosSoporte()
        {
            asignaturas = new ContenedorAsignaturas();
            aulas = new ContenedorAulas();
            cursos = new ContenedorCursos();
            docentes = new ContenedorDocentes();
            estadosAsistencia = new ContenedorEstadosAsistencia();
        }

        public DatosSoporte getInstance()
        {
            if (datosLocales == null) datosLocales = new DatosSoporte();
            return datosLocales;
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