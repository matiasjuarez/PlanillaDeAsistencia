using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;

namespace PlanillaAsistencia.Sincronizacion
{

    // Esta clase se utiliza para sincronizar las asistencias que se tienen en memoria contra las asistencias
    // que hay en la base de datos. La idea es que cada objeto asistenciaSincronizacion trabaje con asistencias
    // de una unica fecha.
    public class AsistenciaSincronizacion
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

        private string fechaDeAsistencias = ""; // Esto representa de que fecha son las asistencias con las que se trabaja

        public string FechaDeAsistencias
        {
            get { return fechaDeAsistencias; }
            set { fechaDeAsistencias = value; }
        }

        // El primer parametro es la lista de asistencias que nos importa. Sobre esa lista de asistencia es que queremos realizar
        // modificaciones para que quede sincronizada con la base de datos. la List asistenciasDeComparacion es el listado
        // de asistencia contra el cual debemos sincronizar la primera lista.
        // Este metodo separara en los List 'agregar', 'eliminar', 'modificar' las asistencias tras haber realizado la correspondiente
        // comparacion.
        public void compararListasDeAsistencias(List<Asistencia> asistenciasOriginales, List<Asistencia> asistenciasDeComparacion)
        {
            agregar.Clear();
            eliminar.Clear();
            modificar.Clear();

            if (asistenciasOriginales == null)
            {
                asistenciasOriginales = new List<Asistencia>();
            }

            if (asistenciasDeComparacion == null)
            {
                asistenciasDeComparacion = new List<Asistencia>();
            }

            Asistencia original;
            Asistencia comparacion;         

            // Esta bandera nos va a permitir identificar aquellas asistenciasOriginales que tienen
            // una asistenciaDeComparacion. Si resulta que no tiene, esto significa que nuestra asistencia original
            // deberia ser eliminada. Si la tiene, entonces hay que ver si la asistencia original necesita ser modificada
            bool seEncontroAsistenciaDeComparacion;

            for (int i = 0; i < asistenciasOriginales.Count; i++ )
            {
                original = asistenciasOriginales[i];
                seEncontroAsistenciaDeComparacion = false;
                for (int j = 0; j < asistenciasDeComparacion.Count; j++)
                {
                    comparacion = asistenciasDeComparacion[j];

                    // Esta parte va recorriendo las asistencias de la lista de comparacion.
                    // Si encuentra una asistencia con el mismo Id que la Asistencia asistenciaOriginal, 
                    // se fija si ambas asistencias tienen los mismos datos, si no es asi, esto significa
                    // que la asistencia que tenemos en memoria ha sido modificada en la base de datos y
                    // debemos actualizarla. Por lo tanto la agregamos a las List de asistencias 'modificar'.
                    // Luego procedemos a eliminar la asistenciaComparacion que acabamos de usar porque
                    // no deberia estar dos veces en memoria una misma asistencia.
                    if (original.EventId == comparacion.EventId && original.AppointmentId == comparacion.AppointmentId)
                    {
                        if (esAsistenciaModificada(original, comparacion))
                        {
                            modificar.Add(comparacion);
                            comparacion.clonarDatosGeneradosPorUsuario(original);
                        }

                        seEncontroAsistenciaDeComparacion = true;
                        asistenciasDeComparacion.RemoveAt(j);
                        break;
                    }
                }

                if (!seEncontroAsistenciaDeComparacion)
                {
                    eliminar.Add(original);
                }
            }

            // Si al terminar los ciclos for resulta que la List de asistencias de comparacion todavia tiene elementos,
            // esto significa que hay algunas asistenciasOriginales que no tienen una asistenciaDeComparacion contra la cual
            // compararse (valga la redundancia :] ). Esto se interpreta como que las asistenciasDeComparacion que quedaron
            // en la lista son asistencias nuevas en la base de datos que todavia no poseemos en el diccionario que esta
            // en memoria

            if (asistenciasDeComparacion.Count > 0)
            {
                agregar.AddRange(asistenciasDeComparacion);
            }
        }

        // Indica si tras haber llamado al metodo compararListasDeAsistencias se ha cargado algo en alguna
        // de las tres listas
        public bool poseeInformacionDeActualizacion()
        {
            if (this.agregar.Count != 0 || this.eliminar.Count != 0 || this.modificar.Count != 0)
            {
                return true;
            }

            return false;
        }

        private bool esAsistenciaModificada(Asistencia original, Asistencia comparacion)
        {
            if (!original.Asignatura.Equals(comparacion.Asignatura)) return true;
            if (!original.Docente.Equals(comparacion.Docente)) return true;
            if (original.ComienzoClaseEsperado != comparacion.ComienzoClaseEsperado) return true;
            if (original.FinClaseEsperado != comparacion.FinClaseEsperado) return true;

            // Se hacen operacion para comprobar si las aulas coinciden
            if (original.Aulas.Count != comparacion.Aulas.Count) return true;

            foreach (Aula aulaO in original.Aulas)
            {
                bool seEncontroAulaIgual = false;
                foreach (Aula aulaC in comparacion.Aulas)
                {
                    if (aulaO.Equals(aulaC))
                    {
                        seEncontroAulaIgual = true;
                        break;
                    }
                }

                if (!seEncontroAulaIgual) return true;
            }

            return false;
        }
    }
}
