using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using EstructurasDeDatos;

namespace Sincronizacion.Comun
{
    /*
     * Esta clase se utiliza para comparar dos contenedores de asistencias. Como resultado de esta comparacion
     * se obtienen tres ContenedoresAsistencia que indican que asistencias se deberian agregar, modificar y eliminar del
     * primer contenedor para obtener el segundo contenedor
     * */
    public class ComparadorContenedoresAsistencias
    {
        private ContenedorAsistencias agregar = new ContenedorAsistencias();
        public ContenedorAsistencias Agregar
        {
            get { return agregar; }
        }

        private ContenedorAsistencias eliminar = new ContenedorAsistencias();
        public ContenedorAsistencias Eliminar
        {
            get { return eliminar; }
        }

        private ContenedorAsistencias modificar = new ContenedorAsistencias();
        public ContenedorAsistencias Modificar
        {
            get { return modificar; }
        }

        /*
         * El primer contenedor lo comporamos contra el segundo. Lo que hay que agregar, modificar y eliminar se
         * deberia hacer sobre el primer contenedor
         * */
        public ComparadorContenedoresAsistencias(ContenedorAsistencias principal, ContenedorAsistencias comparacion)
        {
            compararContenedores(principal, comparacion);
        }

        private void compararContenedores(ContenedorAsistencias contenedorPrincipal, ContenedorAsistencias contenedorComparacion)
        {
            if (contenedorPrincipal == null)
            {
                contenedorPrincipal = new ContenedorAsistencias();
            }

            if (contenedorComparacion == null)
            {
                contenedorComparacion = new ContenedorAsistencias();
            }

            List<Asistencia> listaPrincipal = contenedorPrincipal.obtenerTodasLasAsistencias();
            List<Asistencia> listaComparacion = contenedorComparacion.obtenerTodasLasAsistencias();

            for (int i = 0; i < listaPrincipal.Count; i++)
            {
                // "seEncontroAsistenciaDeComparacion": Esta bandera nos va a permitir identificar aquellas asistenciasPrincipales que tienen
                // una asistenciaDeComparacion. Si resulta que no tiene, esto significa que nuestra asistencia original
                // deberia ser eliminada. Si la tiene, entonces hay que ver si la asistencia original necesita ser modificada
                bool seEncontroAsistenciaDeComparacion = false;
                Asistencia asistenciaPrincipal = listaPrincipal.ElementAt(i);

                for (int j = 0; j < contenedorComparacion.Count; j++)
                {
                    Asistencia asistenciaComparacion = listaComparacion.ElementAt(j);

                    if (asistenciaPrincipal.EventId == asistenciaComparacion.EventId
                        && asistenciaPrincipal.AppointmentId == asistenciaComparacion.AppointmentId)
                    {
                        seEncontroAsistenciaDeComparacion = true;

                        if (!asistenciaPrincipal.Equals(asistenciaComparacion))
                        {
                            // Esto se hace por si se llega a mandar una Asistencia que sea haya creado
                            // desde un evento (no va a tener id)
                            comprobarValorId(asistenciaPrincipal, asistenciaComparacion);
                            modificar.agregarAsistencia(asistenciaComparacion);
                        }

                        listaComparacion.RemoveAt(j);
                        break;
                    }
                }

                if (!seEncontroAsistenciaDeComparacion)
                {
                    eliminar.agregarAsistencia(asistenciaPrincipal);
                }
            }

            // Si al terminar los ciclos for resulta que la List de asistencias de comparacion todavia tiene elementos,
            // esto significa que hay algunas asistenciasOriginales que no tienen una asistenciaDeComparacion contra la cual
            // compararse (valga la redundancia :] ). Esto se interpreta como que las asistenciasDeComparacion que quedaron
            // en la lista son asistencias que deberian agregarse a la lista principal para parecerse a la lista de comparacion

            if (contenedorComparacion.Count > 0)
            {
                agregar.agregarListaAsistencias(listaComparacion);
            }
        }

        // Este metodo se va a usar para verificar que no se vaya a mandar una asistencia con id 0
        // a la base de datos
        private void comprobarValorId(Asistencia una, Asistencia otra)
        {
            if (una.Id <= 0) una.Id = otra.Id;
            if (otra.Id <= 0) otra.Id = una.Id;
        }

        // Indica si tras haber llamado al metodo compararListasDeAsistencias se ha cargado algo en alguna
        // de las tres listas
        public bool sonListasIguales()
        {
            if (this.agregar.Count != 0 || this.eliminar.Count != 0 || this.modificar.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}
