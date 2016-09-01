using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Utilidades
{
    public static class GestorExcepciones
    {
        public static void mostrarExcepcion(Exception e){

            string mensaje = "Se produjo el siguiente error: ";
            mostrarExcepcion(e, mensaje);
        }

        public static void mostrarExcepcion(Exception e, String mensaje)
        {
            string str = mensaje + "\n";
            str += e.Message + "\n";
            str += e.StackTrace;

            mostrarMensajeDeError(str);
        }

        public static void mostrarMensajeDeError(String mensaje)
        {
            Debug.Write(mensaje);
            MessageBox.Show(mensaje);
        }
    }
}
