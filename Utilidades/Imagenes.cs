using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Utilidades
{
    public class Imagenes
    {
        public static Image obtenerImagenDesdeArregloDeBytes(byte[] bytes)
        {
            Image imagen = null;

            if (bytes != null)
            {
                MemoryStream ms = new MemoryStream(bytes);
                imagen = Image.FromStream(ms);
            }

            return imagen;
        }

        public static byte[] convertirImagenEnArregloDeBytes(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, ImageFormat.Png);

            byte[] imagenArreglo = new byte[ms.Length];

            ms.Position = 0;

            ms.Read(imagenArreglo, 0, imagenArreglo.Length);

            return imagenArreglo;
        }
    }
}