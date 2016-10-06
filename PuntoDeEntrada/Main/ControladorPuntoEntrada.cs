using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AdministracionPersonal.Administracion;
using PuntoDeEntrada;
using AdministracionPersonal.InicioSesion;
using PlanillaAsistencia.Principal;
using SincronizacionInterBase;
using AdministracionPersonal.Password;

namespace PuntoDeEntrada.Main
{
    public class ControladorPuntoEntrada : IObservadorCambioEstadoSesion
    {
        private VentanaPuntoDeEntrada ventanaPuntoDeEntrada;
        public VentanaPuntoDeEntrada PuntoDeEntrada { set { ventanaPuntoDeEntrada = value; } }

        private VentanaSesion ventanaSesion;
        private PlanillaAsistencias ventanaPlanilla;
        private PantallaAdministracionPersonal pantallaAdministracionUsuarios;

        public ControladorPuntoEntrada()
        {
            Sesion.agregarObservadorSesion(this);
        }

        private void configurarVistaSegunRolDeUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                ventanaPuntoDeEntrada.mostrarMenuAccesoAdministracionUsuarios(false);
                ventanaPuntoDeEntrada.mostrarMenuAccesoObjetosPerdidos(false);
                ventanaPuntoDeEntrada.mostrarMenuAccesoPlanillaAsistencia(false);
                ventanaPuntoDeEntrada.mostrarMenuAccesoCambioPassword(false);
                ventanaPuntoDeEntrada.mostrarMenuAccesoEstadisticasRapla(false);
            }
            else if (usuario.Rol.Nombre.ToLower() == "administrador")
            {
                ventanaPuntoDeEntrada.mostrarMenuAccesoPlanillaAsistencia(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoObjetosPerdidos(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoAdministracionUsuarios(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoCambioPassword(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoEstadisticasRapla(true);
            }
            else if (usuario.Rol.Nombre.ToLower() == "becario")
            {
                ventanaPuntoDeEntrada.mostrarMenuAccesoPlanillaAsistencia(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoObjetosPerdidos(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoAdministracionUsuarios(false);
                ventanaPuntoDeEntrada.mostrarMenuAccesoCambioPassword(true);
                ventanaPuntoDeEntrada.mostrarMenuAccesoEstadisticasRapla(false);
            }
        }

        public void observarCierreSesion()
        {
            configurarVistaSegunRolDeUsuario(null);
        }

        public void observarInicioSesion()
        {
            Sesion sesionActual = Sesion.obtenerSesionActual();
            configurarVistaSegunRolDeUsuario(sesionActual.Usuario);
        }

        public VentanaSesion obtenerVentanaSesion()
        {
            if (ventanaSesion == null)
            {
                ventanaSesion = new VentanaSesion();
                ControladorSesion controladorSesion = new ControladorSesion();

                ventanaSesion.Controlador = controladorSesion;
                controladorSesion.VentanaSesion = ventanaSesion;

                controladorSesion.agregarObservadorSesion(this);
            }
            
            return ventanaSesion;
        }

        public CambioPassword obtenerVentanaCambioPassword()
        {
            Sesion sesionActual = Sesion.obtenerSesionActual();

            if (sesionActual == null) return null;

            CambioPassword cambioPassword = new CambioPassword();
            cambioPassword.mostrarUsuario(sesionActual.Usuario);

            return cambioPassword;
        }

        public PlanillaAsistencias obtenerVentanaPlanillaAsistencia()
        {
            if (this.ventanaPlanilla == null)
            {
                this.ventanaPlanilla = new PlanillaAsistencias();

                // Cada vez que abrimos la planilla de asistencia sincronizamos los ultimos dos meses (60 dias) contra la base de datos
                // del rapla
                //ControladorSincronizacionInterBase.sincronizar(DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)), DateTime.Now);
                ControladorSincronizacionInterBase.sincronizar(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
            }

            return this.ventanaPlanilla;
        }

        public PantallaAdministracionPersonal obtenerPantallaAdministracionPersonal()
        {
            if (pantallaAdministracionUsuarios == null)
            {
                pantallaAdministracionUsuarios = new PantallaAdministracionPersonal();
            }

            return pantallaAdministracionUsuarios;
        }

        void IObservadorCambioEstadoSesion.observarCierreSesion()
        {
            this.ventanaPlanilla = null;
            this.pantallaAdministracionUsuarios = null;
            configurarVistaSegunRolDeUsuario(null);
        }

        void IObservadorCambioEstadoSesion.observarInicioSesion()
        {
            Usuario usuario = Sesion.obtenerSesionActual().Usuario;
            configurarVistaSegunRolDeUsuario(usuario);
        }
    }
}