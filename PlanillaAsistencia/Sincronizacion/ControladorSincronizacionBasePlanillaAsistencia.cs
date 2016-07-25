using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;

namespace PlanillaAsistencia
{
    public class ControladorSincronizacionModelo
    {
        private Modelo modelo;

        public ControladorSincronizacionModelo(Modelo modelo)
        {
            this.modelo = modelo;
        }

        public void actualizarModelo()
        {
            List<DateTime> fechasCargadasEnDiccionario = modelo.getFechasDeAsistenciaCargadasEnDiccionario();

            if (fechasCargadasEnDiccionario.Count > 0)
            {
                List<Asistencia> asistenciasBaseDatos = DAOAsistencias.obtenerAsistenciasParaListadoDeFechas(fechasCargadasEnDiccionario);

                bool requiereActualizar = modeloRequiereActualizarse(asistenciasBaseDatos);

                if (requiereActualizar)
                {
                    verificarAsistenciasEnListaDeModificadas(asistenciasBaseDatos);

                    if (modelo.getAsistenciasModificadas().Count == 0)
                    {
                        //modelo.notificarVaciadoAsistenciasModificadas();
                    }
                    modelo.limpiarAsistenciasDelModelo();

                    foreach (Asistencia asistenciaBaseDatos in asistenciasBaseDatos)
                    {
                        modelo.agregarAsistenciaEnDiccionarios(asistenciaBaseDatos);
                    }

                   // modelo.notificarCambiosEnModelo();
                }
            }
        }

        private bool modeloRequiereActualizarse(List<Asistencia> asistenciasBaseDatos)
        {
            if (asistenciasBaseDatos.Count != modelo.getAsistenciasEnMemoria().Count)
            {
                return true;
            }

            List<Asistencia> asistenciasEnMemoria = new List<Asistencia>();
            asistenciasEnMemoria.AddRange(modelo.getAsistenciasEnMemoria());

            foreach (Asistencia asistenciaBaseDatos in asistenciasBaseDatos)
            {
                foreach (Asistencia asistenciaMemoria in asistenciasEnMemoria)
                {
                    if (asistenciaBaseDatos.Id == asistenciaMemoria.Id)
                    {
                        if (!asistenciaBaseDatos.poseeLosMismosDatosQueEstaAsistencia(asistenciaMemoria))
                        {
                            return true;
                        }
                        else
                        {
                            asistenciasEnMemoria.Remove(asistenciaMemoria);
                            break;
                        }
                    }
                }
            }

            return false;
        }

        // La idea es que las asistencias que figuran en la lista de modificadas en el modelo se quiten si es necesario.
        // Se debe quitar una asistencia modificada cuando dicha asistencia no figure en la lista de asistencias traidas
        // de la base de datos ya que esto significa que dicha asistencia fue eliminada de la base de datos. 
        //En caso de que si figure en la lista, se debe comprobar que la asistencia correspondiente
        // que figura en la lista de asistencias de la base de datos y la asistencia correspondiente que figura en la lista
        // de asistencias que estan en memoria sean iguales. Si no son iguales, significa que alguien ha modificado la asistencia
        // en cuestion y la quitaremos del listado de asistencias modificadas para darle validez a la actualizacion que ha hecho el
        // otro usuario
        private void verificarAsistenciasEnListaDeModificadas(List<Asistencia> asistenciasBaseDatos)
        {
            Dictionary<string, List<Asistencia>> diccionarioAsistenciasPorFechaBaseDatos = 
                separarListadoDeAsistenciasPorFecha(asistenciasBaseDatos);

            foreach (Asistencia asistenciaModificada in modelo.getAsistenciasModificadas())
            {
                string fechaKeyAsistenciaModificada = asistenciaModificada.ComienzoClaseEsperado.Date.ToString("d");

                List<Asistencia> asistenciasDeFechaBaseDatos = diccionarioAsistenciasPorFechaBaseDatos[fechaKeyAsistenciaModificada];
                List<Asistencia> asistenciasDeFechaMemoria = modelo.getAsistenciasParaFecha(asistenciaModificada.ComienzoClaseEsperado, false);

                bool asistenciaFiguraEnBaseDatos = false;

                foreach (Asistencia asistenciaDeFechaBaseDatos in asistenciasDeFechaBaseDatos)
                {
                    if (asistenciaDeFechaBaseDatos.Id == asistenciaModificada.Id)
                    {
                        asistenciaFiguraEnBaseDatos = true;

                        foreach (Asistencia asistenciaDeFechaMemoria in asistenciasDeFechaMemoria)
                        {
                            if (asistenciaDeFechaBaseDatos.Id == asistenciaDeFechaMemoria.Id)
                            {
                                if(!asistenciaDeFechaBaseDatos.poseeLosMismosDatosQueEstaAsistencia(asistenciaDeFechaMemoria)){
                                    modelo.quitarAsistenciaModificada(asistenciaModificada);
                                }
                                break;
                            }
                        }

                        break;
                    }
                }

                if (!asistenciaFiguraEnBaseDatos)
                {
                    modelo.quitarAsistenciaModificada(asistenciaModificada);
                }
            }
        }

        // Se le pasa por parametro una lista de asistencias y devuelve un diccionario en el que se han separado
        // las asistencias en una lista de asistencia por cada fecha
        private Dictionary<string, List<Asistencia>> separarListadoDeAsistenciasPorFecha(List<Asistencia> asistencias)
        {
            Dictionary<string, List<Asistencia>> diccionario = new Dictionary<string, List<Asistencia>>();
            string fechaDeAsistenciaActual = "";
            List<Asistencia> asistenciasParaFecha;

            // De cada asistencia se obtiene la fecha y de esa fecha se obtiene la fecha sin la hora.
            // Ese valor sin la hora es el que se usa como clave en el diccionario. Si el diccionario
            // ya contiene ese dia, se agrega la asistencia analizada a la lista guardada e identificada
            // por el dia correspondiente. Si no lo contiene, se agrega una nueva entrada al diccionario
            // cuya clave sera la fecha sin hora de la asistencia y el valor sera una lista para guardar
            // todas las asistencias que tengan esa fecha.
            foreach (Asistencia asistencia in asistencias)
            {
                // Se obtiene la fecha sin la hora...
                fechaDeAsistenciaActual = asistencia.ComienzoClaseEsperado.Date.ToString("d");

                if (diccionario.ContainsKey(fechaDeAsistenciaActual))
                {
                    // Si el diccionario tiene una lista para esa clava(esa fecha) la obtenemos del diccionario
                    asistenciasParaFecha = diccionario[fechaDeAsistenciaActual];
                }
                else
                {
                    // Si el diccionario no tiene guardada esa clave, creamos una entrada nueva
                    asistenciasParaFecha = new List<Asistencia>();
                    diccionario[fechaDeAsistenciaActual] = asistenciasParaFecha;
                }

                asistenciasParaFecha.Add(asistencia);
            }

            return diccionario;
        }
    }
}
