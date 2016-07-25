using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;

namespace PlanillaAsistencia.Sincronizacion
{
    public class ControladorSincronizacionBaseRapla
    {
        public void sincronizarAsistencias(string fechaDesde, string fechaHasta)
        {
            DateTime desde = DateTime.Parse(fechaDesde);
            DateTime hasta = DateTime.Parse(fechaHasta);

            sincronizarAsistencias(desde, hasta);
        }

        public void sincronizarAsistencias(DateTime fechaDesde, DateTime fechaHasta)
        {
            DiccionarioAsistenciasPorFecha asistenciasPlanilla = obtenerAsistenciasBaseDatosPlanilla(fechaDesde, fechaHasta);
            DiccionarioAsistenciasPorFecha asistenciasRapla = obtenerAsistenciasBaseDatosRapla(fechaDesde, fechaHasta);

            List<AsistenciaSincronizacion> asistenciasSincronizacion = compararDiccionariosAsistenciasPlanillaContraRapla(asistenciasPlanilla, asistenciasRapla);

            generarCambiosEnBaseDatosPlanilla(asistenciasSincronizacion);
        }

        private DiccionarioAsistenciasPorFecha obtenerAsistenciasBaseDatosPlanilla(DateTime fechaDesde, DateTime fechaHasta)
        {
            DiccionarioAsistenciasPorFecha diccionarioAsistencias = new DiccionarioAsistenciasPorFecha();

            diccionarioAsistencias.agregarListAsistencias(DAOAsistencias.obtenerAsistenciasEntreFechas(fechaDesde, fechaHasta));

            return diccionarioAsistencias;
        }

        private DiccionarioAsistenciasPorFecha obtenerAsistenciasBaseDatosRapla(DateTime fechaDesde, DateTime fechaHasta)
        {
            AuxiliarSincronizacionContraEventos auxiliar = AuxiliarSincronizacionContraEventos.getInstance();
            return auxiliar.obtenerAsistenciasDesdeEventosRapla(fechaDesde, fechaHasta);
        }

        private List<AsistenciaSincronizacion> compararDiccionariosAsistenciasPlanillaContraRapla(
            DiccionarioAsistenciasPorFecha asistenciasPlanilla, DiccionarioAsistenciasPorFecha asistenciasRapla)
        {
            List<AsistenciaSincronizacion> asistenciasSincronizacion = new List<AsistenciaSincronizacion>();

            List<string> keysPlanilla = asistenciasPlanilla.getFechasAlmacenadasComoString();
            List<string> keysRapla = asistenciasRapla.getFechasAlmacenadasComoString();
            List<string> keysCombinadas = new List<string>();

            keysCombinadas.AddRange(keysPlanilla);

            foreach (string keyRapla in keysRapla)
            {
                if (!keysCombinadas.Contains(keyRapla))
                {
                    keysCombinadas.Add(keyRapla);
                }
            }

            foreach (string key in keysCombinadas)
            {
                AsistenciaSincronizacion asistenciaSincronizacion =
                    compararListasAsistenciasPlanillaContraRapla(asistenciasPlanilla.obtenerAsistenciasParaFecha(key),
                    asistenciasRapla.obtenerAsistenciasParaFecha(key));

                asistenciasSincronizacion.Add(asistenciaSincronizacion);
            }

            return asistenciasSincronizacion;
        }

        private AsistenciaSincronizacion compararListasAsistenciasPlanillaContraRapla(
            List<Asistencia> asistenciasPlanilla, List<Asistencia> asistenciasRapla)
        {
            AsistenciaSincronizacion asistenciaSincronizacion = new AsistenciaSincronizacion();
            asistenciaSincronizacion.compararListasDeAsistencias(asistenciasPlanilla, asistenciasRapla);
            return asistenciaSincronizacion;
        }

        private void generarCambiosEnBaseDatosPlanilla(List<AsistenciaSincronizacion> asistenciasSincronizacion)
        {
            foreach (AsistenciaSincronizacion asistenciaSincro in asistenciasSincronizacion)
            {
                foreach (Asistencia asistencia in asistenciaSincro.Agregar)
                {
                    DAOAsistencias.insertarNuevaAsistencia(asistencia);
                }

                foreach (Asistencia asistencia in asistenciaSincro.Eliminar)
                {
                    DAOAsistencias.eliminarAsistencia(asistencia);
                }

                DAOAsistencias.updateAsistencias(asistenciaSincro.Modificar);
            }
        }
    }
}
