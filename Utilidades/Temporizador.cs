using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Utilidades
{
    public class Temporizador
    {
        private Timer timer;
        private List<ITemporizable> objetosTemporizados;

        public Temporizador(int intervaloTiempoEnSegundos)
        {
            objetosTemporizados = new List<ITemporizable>();

            timer = new Timer();
            timer.Interval = intervaloTiempoEnSegundos * 1000; //El temporizador toma el tiempo en milisegundos...
            timer.Tick += notificarTick;
        }

        private void notificarTick(object sender, EventArgs args)
        {
            foreach (ITemporizable temporizable in objetosTemporizados)
            {
                temporizable.procesarTick();
            }
        }

        // Habilita o deshabilite el temporizador
        public void habilitar(bool habilitar)
        {
            timer.Enabled = habilitar;
        }

        public void agregarObjetoTemporizable(ITemporizable temporizable)
        {
            objetosTemporizados.Add(temporizable);
        }

        public bool quitarObjetoTemporizable(ITemporizable temporizable)
        {
            return objetosTemporizados.Remove(temporizable);
        }

        public interface ITemporizable
        {
            public void procesarTick();
        }
    }
}
