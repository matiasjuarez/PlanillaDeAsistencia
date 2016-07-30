using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;
using AccesoDatos;
using EstructurasDeDatos;
using Sincronizacion.Comun;

namespace Sincronizacion
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

            ComparadorContenedoresAsistencias comparador = 
                new ComparadorContenedoresAsistencias(asistenciasPlanilla, asistenciasRapla);

            generarCambiosEnBaseDatosPlanilla(comparador);
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

        private void generarCambiosEnBaseDatosPlanilla(ComparadorContenedoresAsistencias comparador)
        {
            List<Asistencia> asistenciasAgregar = comparador.Agregar.obtenerTodasLasAsistencias();
            List<Asistencia> asistenciasModificar = comparador.Modificar.obtenerTodasLasAsistencias();
            List<Asistencia> asistenciasEliminar = comparador.Eliminar.obtenerTodasLasAsistencias();

            foreach (Asistencia asistencia in asistenciasAgregar)
            {
                DAOAsistencias.insertarNuevaAsistencia(asistencia);
            }

            foreach (Asistencia asistencia in asistenciasEliminar)
            {
                DAOAsistencias.eliminarAsistencia(asistencia);
            }

            DAOAsistencias.updateAsistencias(asistenciasModificar);
        }
    }
}