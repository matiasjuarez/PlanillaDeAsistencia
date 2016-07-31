using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades;

namespace PlanillaAsistencia
{
    public interface IObservadorTripleGrilla
    {
        void recibirNotificacionFilaSeleccionada(AsistenciaTabla asistencia);
    }
}
