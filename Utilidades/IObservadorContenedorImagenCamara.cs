using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utilidades
{
    public interface IObservadorContenedorImagenCamara
    {
        void observarCambioImagenContenedor(Image imagen);
    }
}
