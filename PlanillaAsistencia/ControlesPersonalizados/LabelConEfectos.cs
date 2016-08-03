using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanillaAsistencia.ControlesPersonalizados
{
    public partial class LabelConEfectos : UserControl
    {
        private int duracionMensajes;
        public int DuracionMensajes
        {
            get { return duracionMensajes; }
            set { duracionMensajes = value; }
        }

        private Color colorPorDefecto;
        public Color ColorPorDefecto
        {
            get { return colorPorDefecto; }
            set { colorPorDefecto = value; }
        }

        private MostradorMensaje mostradorMensajes;

        private List<Mensaje> mensajes;

        public LabelConEfectos()
        {
            InitializeComponent();

            this.mostradorMensajes = new MostradorMensaje(this);

            mensajes = new List<Mensaje>();
            colorPorDefecto = Color.Black;
            duracionMensajes = 3000;
            //this.lblTexto.Size = new System.Drawing.Size(this.Size.Width, 0);
            //this.MaximumSize = new System.Drawing.Size(this.Size.Width, 0);
        }

        private void setTexto(string texto)
        {
            this.txt.Text = texto;
        }

        // El mensaje, el color del mensaje y la cantidad de tiempo que queremos que sea visible
        public void mostrarMensaje(string mensaje, Color color, int duracion)
        {
            Mensaje mensajeNuevo = new Mensaje();
            mensajeNuevo.TextoMensaje = mensaje;
            mensajeNuevo.Color = color;
            mensajeNuevo.Duracion = duracion;

            if (duracion < 0)
            {
                duracion = this.duracionMensajes;
            }

            mensajes.Add(mensajeNuevo);

            if (mostradorMensajes.EstaDormido)
            {
                mostradorMensajes.despertar();
            }
        }

        public void mostrarMensaje(string mensaje)
        {
            mostrarMensaje(mensaje, this.colorPorDefecto, this.duracionMensajes);
        }

        public void mostrarMensaje(string mensaje, Color color)
        {
            mostrarMensaje(mensaje, color, this.duracionMensajes);
        }

        private Mensaje obtenerProximoMensaje()
        {
            if (mensajes.Count == 0) return null;

            Mensaje mensaje = mensajes.ElementAt<Mensaje>(0);
            mensajes.Remove(mensaje);
            return mensaje;
        }

        private class MostradorMensaje
        {
            private LabelConEfectos label;
            private Timer timer;

            private bool estaDormido = true;
            public bool EstaDormido
            {
                get { return estaDormido; }
            }

            public void despertar()
            {
                estaDormido = false;
                Mensaje proximoMensaje = label.obtenerProximoMensaje();
                mostrarMensaje(proximoMensaje);
            }

            public MostradorMensaje(LabelConEfectos label)
            {
                this.label = label;
            }

            private void mostrarMensaje(Mensaje mensaje)
            {
                this.label.setTexto(mensaje.TextoMensaje);

                int frecuencia = 30;
                int intervalo = mensaje.Duracion / frecuencia;

                this.timer = new Timer();
                this.timer.Interval = intervalo;
                this.timer.Start();

                Color backColor = label.BackColor;
                Color foreColor = mensaje.Color;

                float valorRojo = foreColor.R;
                float valorVerde = foreColor.G;
                float valorAzul = foreColor.B;

                float pasoRojo = (backColor.R - valorRojo) / frecuencia;
                float pasoVerde = (backColor.G - valorVerde) / frecuencia;
                float pasoAzul = (backColor.B - valorAzul) / frecuencia;

                this.timer.Tick += (o, e) =>
                {
                    valorRojo = procesarNuevoValorParaColor(backColor.R, valorRojo, pasoRojo);
                    valorVerde = procesarNuevoValorParaColor(backColor.G, valorVerde, pasoVerde);
                    valorAzul = procesarNuevoValorParaColor(backColor.B, valorAzul, pasoAzul);

                    Color nuevoColor = Color.FromArgb(100, (byte)valorRojo, (byte)valorVerde, (byte)valorAzul);
                    label.txt.ForeColor = nuevoColor;

                    procesarFinMensaje();
                };
            }

            private void procesarFinMensaje()
            {
                if (sonColoresIguales(label.txt.ForeColor, label.txt.BackColor))
                {
                    label.Text = "";
                    timer.Stop();

                    Mensaje mensaje = label.obtenerProximoMensaje();

                    if (mensaje == null) this.estaDormido = true;
                    else mostrarMensaje(mensaje);
                }
            }

            private bool sonColoresIguales(Color c1, Color c2)
            {
                if (c1.R == c2.R && c1.B == c2.B && c1.G == c2.G)
                {
                    return true;
                }
                return false;
            }

            private float procesarNuevoValorParaColor(byte background, float foreground, float paso)
            {
                float nuevoValor = (foreground + paso);

                if (paso > 0)
                {
                    if (nuevoValor > background)
                    {
                        nuevoValor = background;
                    }
                }
                else
                {
                    if (nuevoValor < background)
                    {
                        nuevoValor = background;
                    }
                }

                return nuevoValor;
            }
        }

        private class Mensaje
        {
            private string textoMensaje;
            public string TextoMensaje
            {
                get { return textoMensaje; }
                set { textoMensaje = value; }
            }

            private Color color;
            public Color Color
            {
                get { return color; }
                set { color = value; }
            }

            private int duracion;
            public int Duracion
            {
                get { return duracion; }
                set { duracion = value; }
            }
        }
    }
}
