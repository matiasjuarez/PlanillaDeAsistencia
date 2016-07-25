using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanillaAsistencia
{
    public interface IObservadorModelo
    {
        void observarCambioDatosModelo();
        void observarVaciadoDeAsistenciasModificadas();
    }
}
