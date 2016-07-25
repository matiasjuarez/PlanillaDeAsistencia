using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Windows.Forms;

namespace PlanillaAsistencia
{
    public class Mensaje
    {
        private string mensaje = "";
        private Color color = Color.Black;
        private Label contenedorDelMensaje = null;

        public Mensaje(Label label)
        {
            if (label == null)
            {
                throw new ArgumentNullException("No se puede inicializar el objeto 'Mensaje' porque se ha pasado un label null");
            }

            this.contenedorDelMensaje = label;
        }

        public void setContenedorDelMensaje(Label label)
        {
            if (label != null)
            {
                contenedorDelMensaje = label;
            }
        }

        // El mensaje, el color del mensaje y la cantidad de tiempo que queremos que sea visible
        public void mostrarMensaje(string mensaje, Color color, int milisegundos)
        {
            mostrarMensaje(mensaje, color);

            if (milisegundos < 0)
            {
                milisegundos = 0;
            }

            desvanecerMensaje(milisegundos, contenedorDelMensaje);
        }

        public void mostrarMensaje(string mensaje)
        {
            mostrarMensaje(mensaje, Color.Black);
        }

        public void mostrarUltimoMensaje()
        {
            mostrarMensaje(this.mensaje, this.color);
        }

        public void mostrarMensaje(string mensaje, Color color)
        {
            contenedorDelMensaje.Text = mensaje;
            contenedorDelMensaje.ForeColor = color;

            this.mensaje = mensaje;
            this.color = color;
        }

        private void desvanecerMensaje(int milisegundos, Label mensaje)
        {
            int frecuenciaAnimacion = 10;

            System.Windows.Forms.Timer tmrAnimacion = new System.Windows.Forms.Timer();
            tmrAnimacion.Interval = 1000 / frecuenciaAnimacion;
            tmrAnimacion.Start();

            Color backColor = mensaje.BackColor;
            Color foreColor = mensaje.ForeColor;

            int pasoRojo = (backColor.R - foreColor.R) / ((milisegundos / 1000) * frecuenciaAnimacion);
            int pasoVerde = (backColor.G - foreColor.G) / ((milisegundos / 1000) * frecuenciaAnimacion);
            int pasoAzul = (backColor.B - foreColor.B) / ((milisegundos / 1000) * frecuenciaAnimacion);

            tmrAnimacion.Tick += (o, e) =>
            {
                int nuevoRojo = devolverValorDeColorValido(foreColor.R + pasoRojo);
                int nuevoVerde = devolverValorDeColorValido(foreColor.G + pasoVerde);
                int nuevoAzul = devolverValorDeColorValido(foreColor.B + pasoAzul);

                Color nuevoColor = Color.FromArgb(100, nuevoRojo, nuevoVerde, nuevoAzul);
                foreColor = nuevoColor;
                mensaje.ForeColor = nuevoColor;

                if (sonColoresIguales(mensaje.ForeColor, mensaje.BackColor))
                {
                    mensaje.Text = "";
                    tmrAnimacion.Stop();
                }
            };
        }

        private bool sonColoresIguales(Color c1, Color c2)
        {
            if (c1.R == c2.R && c1.B == c2.B && c1.G == c2.G)
            {
                return true;
            }
            return false;
        }

        private int devolverValorDeColorValido(int valorColor)
        {
            if (valorColor < 0)
            {
                return 0;
            }
            else if (valorColor > 255)
            {
                return 255;
            }
            else
            {
                return valorColor;
            }
        }
    }
}
