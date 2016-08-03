using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class CondicionAsistencia
    {
        private Asistencia asistencia;

        public CondicionAsistencia(Asistencia asistenciaTabla)
        {
            this.asistencia = asistenciaTabla;
        }

        public void calcularCondicion()
        {
            calcularModificada(asistencia);
            calcularSinHoraEntradaReal_PostHoraEntradaEsperada(asistencia);
            calcularSinHoraSalidaReal_PostHoraSalidaEsperada(asistencia);
            calcularValidaParaGuardarse(asistencia);
        }

        private bool modificada;
        public bool esModificada()
        {
            return modificada;
        }
        private void calcularModificada(Asistencia asistencia)
        {
            if (asistencia.estaModificada()) modificada = true;
            else modificada = false;
        }

        private bool sinHoraEntradaReal_PostHoraEntradaEsperada;
        public bool esSinHoraEntradaReal_PostHoraEntradaEsperada()
        {
            return sinHoraEntradaReal_PostHoraEntradaEsperada;
        }
        private void calcularSinHoraEntradaReal_PostHoraEntradaEsperada(Asistencia asistencia)
        {
            DateTime fechaHoraActual = DateTime.Now;
            sinHoraEntradaReal_PostHoraEntradaEsperada = false;

            if (fechaHoraActual >= asistencia.obtenerEntradaEsperada())
            {
                if (asistencia.HoraEntradaReal.Equals(new TimeSpan(0, 0, 0)))
                {
                    sinHoraEntradaReal_PostHoraEntradaEsperada = true;
                }
            }
        }

        private bool sinHoraSalidaReal_PostHoraSalidaEsperada;
        public bool esSinHoraSalidaReal_PostHoraSalidaEsperada()
        {
            return sinHoraSalidaReal_PostHoraSalidaEsperada;
        }
        private void calcularSinHoraSalidaReal_PostHoraSalidaEsperada(Asistencia asistencia)
        {
            DateTime fechaHoraActual = DateTime.Now;
            sinHoraSalidaReal_PostHoraSalidaEsperada = false;

            if (fechaHoraActual >= asistencia.obtenerSalidaEsperada())
            {
                if (asistencia.HoraSalidaReal.Equals(new TimeSpan(0, 0, 0)))
                {
                    sinHoraSalidaReal_PostHoraSalidaEsperada = true;
                }
            }
        }

        private bool seleccionada;
        public bool esSeleccionada()
        {
            return seleccionada;
        }
        public void setSeleccionada(bool valor)
        {
            seleccionada = valor;
        }

        private bool validaParaGuardarse;
        public bool esValidaParaGuardarse()
        {
            return validaParaGuardarse;
        }
        private void calcularValidaParaGuardarse(Asistencia asistencia)
        {
            validaParaGuardarse = true;
            if (asistencia.CantidadAlumnos == 0) validaParaGuardarse = false;
            if (asistencia.HoraEntradaReal.Equals(new TimeSpan(0, 0, 0))) validaParaGuardarse = false;
        }
    }
}
