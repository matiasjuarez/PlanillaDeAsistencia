using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlanillaAsistencia.Sincronizacion;
using Entidades;
//using System.Windows.Forms;
//using System.Drawing;

using System.Windows.Forms;

using AccesoDatos;

namespace PlanillaAsistencia
{
    // ESTA CLASE SE ENCARGA DE MANEJAR LAS MODIFICACIONES EN LA PARTE GRAFICA DE LA VENTANA
    public class Controlador
    {
        private planillaAsistencia vista;
        private Modelo modelo;
        private ControladorEdicionAsistencia controladorEdicionAsistencia;

        public Controlador(planillaAsistencia planilla, Modelo modelo)
        {
            this.vista = planilla;
            this.modelo = modelo;

            vista.setControlador(this);
            modelo.setControlador(this);

            modelo.inicializar();
            planilla.inicializar();

            //vista.habilitarCampos(false);
            vista.resetearCampos();
        }

        // Se encarga de cargar los datos de la asistencia seleccionada por el usuario tras haber hecho click en
        // alguna fila de alguna grilla. 
        public void manejarSeleccionDeAsistenciaDesdeGrilla()
        {
            if (controladorEdicionAsistencia != null)
            {
                vista.quitarObservadorCamposEditables(controladorEdicionAsistencia);

                /*if (controladorEdicionAsistencia.seModificoAsistencia())
                {
                    modelo.marcarAsistenciaComoModificada(controladorEdicionAsistencia.AsistenciaEnEdicion);
                }*/
            }

            controladorEdicionAsistencia = new ControladorEdicionAsistencia(vista, modelo);
            vista.agregarObservadorCamposEditables(controladorEdicionAsistencia);

            controladorEdicionAsistencia.manejarSeleccionDeAsistenciaDesdeGrilla();
        }

        private bool comprobarSiQuedanAsistenciasModificadas()
        {
            foreach (AsistenciaDual asistenciaD in modelo.getAsistenciasEnMemoria())
            {
                if (asistenciaD.esModificada()) return true;
            }
            return false;
        }

        // Cuando el usuario cambia la fecha seleccionada debe llamar a esta funcion.
        public void manejarCambioFechaSeleccionada(DateTime fechaSeleccionada)
        {
            vista.resetearCampos();
            vista.habilitarCampos(false);

            cargarAsistenciasDeFechaEnGrillasDeVista(fechaSeleccionada);
        }


        // La fecha de las asistencias que deberian ser recargadas
        private void cargarAsistenciasDeFechaEnGrillasDeVista(DateTime fecha)
        {
            vista.habilitarManejadoresDeEventos(false);
            vista.cargarAsistenciasEnGrillas(modelo.getAsistenciasParaFecha(fecha, true));
            vista.habilitarManejadoresDeEventos(true);

            /*List<Asistencia> asistencias = modelo.getAsistenciasModificadas();

            foreach (Asistencia asistencia in asistencias)
            {
                vista.marcarAsistenciaComoModificada(asistencia);
            }*/
        }

        // Ejecuta las acciones PERTINENTES(jaja) al momento de crear la parte visual de la planilla.
        // Esto implica cargar los comboxes y deshabilitar todos los campos de la misma ya que estos
        // se van a habilitar unicamente cuando haya una asistencia seleccionada.
        public void manejarEventoLoad()
        {
            /*vista.cargarComboEstadoAsistencia();
            vista.cargarComboAsignatura();
            vista.cargarComboDocente();
            vista.habilitarCampos(false);
            vista.resetearCampos();*/
        }

        public bool guardarAsistenciasModificadas()
        {
            return controladorEdicionAsistencia.guardarAsistenciasModificadas();
        }

        public List<Asignatura> obtenerAsignaturas()
        {
            return modelo.getAsignaturas();
        }

        public List<Docente> obtenerDocentes()
        {
            return modelo.getDocentes();
        }

        public List<EstadoAsistencia> obtenerEstadosDeAsistencia()
        {
            return modelo.getEstadosAsistencia();
        }

        public void actualizarModelo()
        {
            ControladorSincronizacionModelo controlador = new ControladorSincronizacionModelo(modelo, this);
            controlador.actualizarModelo();

            if (!comprobarSiQuedanAsistenciasModificadas())
            {
                vista.habilitarBotonGuardado(false);
            }

            if (controlador.SeActualizoModelo)
            {
                int idAsistenciaSeleccionada = vista.getIdAsistenciaSeleccionada();
                AsistenciaDual asistenciaDualSeleccionada = modelo.getAsistencia(idAsistenciaSeleccionada);
                vista.cargarDatosDeAsistencia(asistenciaDualSeleccionada.Clonada);

                DateTime fechaSeleccionadaPorUsuario = this.vista.obtenerFechaSeleccionada();
                cargarAsistenciasDeFechaEnGrillasDeVista(fechaSeleccionadaPorUsuario);
                vista.mostrarMensaje("ACTUALIZADO", 2000);
            }
        }
    }
}
