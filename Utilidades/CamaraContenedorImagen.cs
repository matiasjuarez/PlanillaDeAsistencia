using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utilidades
{
    public class CamaraContenedorImagen
    {
        private Image imagen;
        private List<IObservadorContenedorImagenCamara> observadores = new List<IObservadorContenedorImagenCamara>();

        public Image Imagen
        {
            get { return imagen; }
            set 
            { 
                imagen = value;
                notificarObservadores(imagen);
            }
        }

        public void agregarObservador(IObservadorContenedorImagenCamara observador)
        {
            observadores.Add(observador);
        }

        public bool quitarObservador(IObservadorContenedorImagenCamara observador)
        {
            return observadores.Remove(observador);
        }

        private void notificarObservadores(Image image)
        {
            foreach (IObservadorContenedorImagenCamara observador in observadores)
            {
                observador.observarCambioImagenContenedor(image);
            }
        }
    }
}
