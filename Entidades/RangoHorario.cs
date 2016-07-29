using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class RangoHorario
    {
        private TimeSpan horaInicio;
        private TimeSpan horaFin;

        public RangoHorario() { }

        public RangoHorario(TimeSpan horaInicio, TimeSpan horaFin)
        {
            HoraInicio = horaInicio;
            HoraFin = horaFin;
        }

        public RangoHorario(String horaInicio, String horaFin)
        {
            setHoraInicio(horaInicio);
            setHoraFin(horaFin);
        }

        public void setHoraInicio(String horaInicio)
        {
            HoraInicio = TimeSpan.Parse(horaInicio);
        }

        public void setHoraFin(String horaFin)
        {
            HoraFin = TimeSpan.Parse(horaFin);
        }

        public TimeSpan HoraInicio
        {
            get { return horaInicio; }
            set { horaInicio = value; }
        }

        public TimeSpan HoraFin
        {
            get { return horaFin; }
            set { horaFin = value; }
        }

        // Se fija si una hora dada esta dentro del rango horario. Se debe tener en cuenta
        // que consideramos el limite inferior del rango como perteneciente al rango, pero
        // el limite superior se considera fuera del rango
        public Boolean estaDentroDelRangoHorario(TimeSpan hora)
        {
            if (hora >= HoraInicio && hora < HoraFin)
            {
                return true;
            }
            return false;
        }
    }
}
