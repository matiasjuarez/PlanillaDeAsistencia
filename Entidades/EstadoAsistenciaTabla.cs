using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class EstadoAsistenciaTabla
    {
        private AsistenciaTabla asistenciaTabla;

        public EstadoAsistenciaTabla(AsistenciaTabla asistenciaTabla)
        {
            this.asistenciaTabla = asistenciaTabla;
        }

        public void calcularEstado()
        {
            Asistencia asistencia = this.asistenciaTabla.obtenerAsistencia();

            if (asistencia.estaModificada()) setModificada(true);
            else setModificada(false);

            DateTime fechaHoraActual = DateTime.Now;
            setSinHoraEntradaReal_PostHoraEntradaEsperada(false);
            if (fechaHoraActual >= asistencia.obtenerEntradaEsperada())
            {
                if (asistencia.HoraEntradaReal.Equals(new TimeSpan(0, 0, 0)))
                {
                    setSinHoraEntradaReal_PostHoraEntradaEsperada(true);
                }
            }

            setSinHoraSalidaReal_PostHoraSalidaEsperada(false);
            if (fechaHoraActual >= asistencia.obtenerSalidaEsperada())
            {
                if (asistencia.HoraSalidaReal.Equals(new TimeSpan(0, 0, 0)))
                {
                    setSinHoraSalidaReal_PostHoraSalidaEsperada(true);
                }
            }

            setEsValidaParaGuardarse(true);
            if (asistencia.CantidadAlumnos == 0) setEsValidaParaGuardarse(false);
            if (asistencia.HoraEntradaReal.Equals(new TimeSpan(0, 0, 0))) setEsValidaParaGuardarse(false);
        }

        private bool modificada;
        public bool esModificada()
        {
            return modificada;
        }
        public void setModificada(bool valor)
        {
            modificada = valor;
        }

        private bool sinHoraEntradaReal_PostHoraEntradaEsperada;
        public bool esSinHoraEntradaReal_PostHoraEntradaEsperada()
        {
            return sinHoraEntradaReal_PostHoraEntradaEsperada;
        }
        public void setSinHoraEntradaReal_PostHoraEntradaEsperada(bool valor)
        {
            sinHoraEntradaReal_PostHoraEntradaEsperada = valor;
        }

        private bool sinHoraSalidaReal_PostHoraSalidaEsperada;
        public bool esSinHoraSalidaReal_PostHoraSalidaEsperada()
        {
            return sinHoraSalidaReal_PostHoraSalidaEsperada;
        }
        public void setSinHoraSalidaReal_PostHoraSalidaEsperada(bool valor)
        {
            sinHoraSalidaReal_PostHoraSalidaEsperada = valor;
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

        private bool esValidaParaGuardarse;
        public void setEsValidaParaGuardarse(bool valor)
        {
            esValidaParaGuardarse = valor;
        }
        public bool getEsValidaParaGuardarse()
        {
            return esValidaParaGuardarse;
        }
    }
}
