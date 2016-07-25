using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace PlanillaAsistencia
{
    public class TimerActualizacion
    {
        private Timer temporizador;
        /*private IActualizable objetoActualizar;

        public TimerActualizacion(int intervaloTiempoEnSegundos, IActualizable objetoActualizar)
        {
            this.objetoActualizar = objetoActualizar;

            temporizador = new Timer();
            temporizador.Interval = intervaloTiempoEnSegundos * 1000; //El temporizador toma el tiempo en milisegundos...

            temporizador.Tick += timerDeActualizacionElapsed;
            temporizador.Enabled = true;
        }*/

        // Habilita o deshabilite el temporizador
        public void habilitarTemporizador(bool habilitar)
        {
            temporizador.Enabled = habilitar;
        }

        private void timerDeActualizacionElapsed(object sender, EventArgs args)
        {
            //objetoActualizar.actualizar();
        }
    }
}
