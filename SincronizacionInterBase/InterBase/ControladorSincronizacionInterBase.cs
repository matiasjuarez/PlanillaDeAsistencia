using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using Entidades.Clases_auxiliares;

namespace PlanillaAsistencia.Sincronizacion
{
    public class ControladorSincronizacionInterBase
    {
        public void sincronizarAsistencias(string fechaDesde, string fechaHasta)
        {
            DateTime desde = DateTime.Parse(fechaDesde);
            DateTime hasta = DateTime.Parse(fechaHasta);

            sincronizarAsistencias(desde, hasta);
        }

        public void sincronizarAsistencias(DateTime fechaDesde, DateTime fechaHasta)
        {
            ContenedorAsistencias asistenciasPlanilla = obtenerAsistenciasBaseDatosPlanilla(fechaDesde, fechaHasta);
            ContenedorAsistencias asistenciasRapla = obtenerAsistenciasBaseDatosRapla(fechaDesde, fechaHasta);

            List<ComparadorContenedoresAsistencia> asistenciasSincronizacion = compararDiccionariosAsistenciasPlanillaContraRapla(asistenciasPlanilla, asistenciasRapla);

            generarCambiosEnBaseDatosPlanilla(asistenciasSincronizacion);
        }

        private ContenedorAsistencias obtenerAsistenciasBaseDatosPlanilla(DateTime fechaDesde, DateTime fechaHasta)
        {
            ContenedorAsistencias asistencias = new ContenedorAsistencias();

            asistencias.agregarListaAsistencias(DAOAsistencias.obtenerAsistenciasEntreFechas(fechaDesde, fechaHasta));

            return asistencias;
        }

        private ContenedorAsistencias obtenerAsistenciasBaseDatosRapla(DateTime fechaDesde, DateTime fechaHasta)
        {
            ContenedorAsistencias asistencias = new ContenedorAsistencias();

            List<Evento> eventos = DAOEventosRapla.obtenerEventosEntreFechas(fechaDesde, fechaHasta);

            foreach (Evento evento in eventos)
            {
                Asistencia asistencia = evento.convertirEnAsistencia();
                asistencias.agregarAsistencia(asistencia);
            }

            return asistencias;
        }

        private List<ComparadorContenedoresAsistencia> compararAsistenciasPlanillaContraRapla(
            ContenedorAsistencias asistenciasPlanilla, ContenedorAsistencias asistenciasRapla)
        {
            List<ComparadorContenedoresAsistencia> asistenciasSincronizacion = new List<ComparadorContenedoresAsistencia>();

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
                ComparadorContenedoresAsistencia asistenciaSincronizacion =
                    compararListasAsistenciasPlanillaContraRapla(asistenciasPlanilla.obtenerAsistenciasParaFecha(key),
                    asistenciasRapla.obtenerAsistenciasParaFecha(key));

                asistenciasSincronizacion.Add(asistenciaSincronizacion);
            }

            return asistenciasSincronizacion;
        }

        private ComparadorContenedoresAsistencia compararListasAsistenciasPlanillaContraRapla(
            List<Asistencia> asistenciasPlanilla, List<Asistencia> asistenciasRapla)
        {
            ComparadorContenedoresAsistencia asistenciaSincronizacion = new ComparadorContenedoresAsistencia();
            asistenciaSincronizacion.compararListasDeAsistencias(asistenciasPlanilla, asistenciasRapla);
            return asistenciaSincronizacion;
        }

        private void generarCambiosEnBaseDatosPlanilla(List<ComparadorContenedoresAsistencia> asistenciasSincronizacion)
        {
            foreach (ComparadorContenedoresAsistencia asistenciaSincro in asistenciasSincronizacion)
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
