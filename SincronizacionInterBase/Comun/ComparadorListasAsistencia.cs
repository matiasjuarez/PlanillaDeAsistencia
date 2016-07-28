using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using Entidades.ClasesAuxiliares;

namespace Sincronizacion.Comun
{

    /*
     * Esta clase se utiliza para comparar dos listas de asistencias. Como resultado de esta comparacion
     * se obtienen tres List<Asistencia> que indican que asistencias se deberian agregar, modificar y eliminar de la
     * primera lista para obtener la segunda lista
     * */
    public class ComparadorListasAsistencia
    {
        private List<Asistencia> agregar = new List<Asistencia>();
        public List<Asistencia> Agregar
        {
            get { return agregar; }
        }

        private List<Asistencia> eliminar = new List<Asistencia>();
        public List<Asistencia> Eliminar
        {
            get { return eliminar; }
        }

        private List<Asistencia> modificar = new List<Asistencia>();
        public List<Asistencia> Modificar
        {
            get { return modificar; }
        }

        /*
         * La primera lista es la principal la cual compararemos contra la segunda lista
         * */
        public ComparadorListasAsistencia(List<Asistencia> principal, List<Asistencia> comparacion)
        {
            compararListas(principal, comparacion);
        }

        private void compararListas(List<Asistencia> listaPrincipal, List<Asistencia> listaComparacion)
        {
            if (listaPrincipal == null)
            {
                listaPrincipal = new List<Asistencia>();
            }

            if (listaComparacion == null)
            {
                listaComparacion = new List<Asistencia>();
            }     

            for (int i = 0; i < listaPrincipal.Count; i++)
            {
                // "seEncontroAsistenciaDeComparacion": Esta bandera nos va a permitir identificar aquellas asistenciasPrincipales que tienen
                // una asistenciaDeComparacion. Si resulta que no tiene, esto significa que nuestra asistencia original
                // deberia ser eliminada. Si la tiene, entonces hay que ver si la asistencia original necesita ser modificada
                bool seEncontroAsistenciaDeComparacion = false;
                Asistencia asistenciaPrincipal = listaPrincipal.ElementAt(i);

                for (int j = 0; j < listaComparacion.Count; j++)
                {
                    Asistencia asistenciaComparacion = listaComparacion.ElementAt(j);

                    if (asistenciaPrincipal.EventId == asistenciaComparacion.EventId
                        && asistenciaPrincipal.AppointmentId == asistenciaComparacion.AppointmentId)
                    {
                        seEncontroAsistenciaDeComparacion = true;

                        if (!asistenciaPrincipal.Equals(asistenciaComparacion))
                        {
                            modificar.Add(asistenciaComparacion);
                        }

                        listaComparacion.RemoveAt(j);
                        break;
                    }
                }

                if (!seEncontroAsistenciaDeComparacion)
                {
                    eliminar.Add(asistenciaPrincipal);
                }
            }

            // Si al terminar los ciclos for resulta que la List de asistencias de comparacion todavia tiene elementos,
            // esto significa que hay algunas asistenciasOriginales que no tienen una asistenciaDeComparacion contra la cual
            // compararse (valga la redundancia :] ). Esto se interpreta como que las asistenciasDeComparacion que quedaron
            // en la lista son asistencias que deberian agregarse a la lista principal para parecerse a la lista de comparacion

            if (listaComparacion.Count > 0)
            {
                agregar.AddRange(listaComparacion);
            }
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
