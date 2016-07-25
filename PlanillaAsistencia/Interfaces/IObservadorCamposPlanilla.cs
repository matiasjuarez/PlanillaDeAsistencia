using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;

namespace PlanillaAsistencia
{
    public interface IObservadorCamposPlanilla
    {
        void observarCambioDocente(Docente docente);
        void observarCambioAsignatura(Asignatura Asignatura);
        void observarCambioHoraRealDeSalida(DateTime horaSalida);
        void observarCambioHoraRealDeEntrada(DateTime horaEntrada);
        void observarCambioEstadoAsistencia(EstadoAsistencia estadoAsistencia);
        void observarCambioObservaciones(string observaciones);
        void observarCambioCantidadAlumnos(int cantidadAlumnos);
    }
}
