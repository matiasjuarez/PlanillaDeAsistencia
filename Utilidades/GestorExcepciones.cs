using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Utilidades
{
    public static class GestorExcepciones
    {
        public static void mostrarExcepcion(Exception e){
            
            MessageBox.Show("Se produjo el siguiente error: \n" + e.Message + "\n" + e.StackTrace);
        }

        public static void mostrarExcepcion(Exception e, String mensaje)
        {
            MessageBox.Show(mensaje + "\n" + e.Message + "\n" + e.StackTrace);
        }

        public static void mostrarMensajeDeError(String mensaje)
        {
            MessageBox.Show(mensaje);
        }
    }
}
