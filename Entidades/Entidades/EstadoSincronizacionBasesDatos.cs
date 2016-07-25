using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public static class EstadoSincronizacionBasesDatos
    {
        // La sincronizacion se realizo sin problemas
        public static readonly int SincronizacionCorrecta = 0;

        // No hay datos en rapla para el dia seleccionado y no se puede hacer sincronizacion
        public static readonly int SincronizacionSinDatos = 1;

        // Hay diferencias entre lo que hay guardado en la base de datos del rapla
        // y lo que hay en la base de datos de la planilla de asistencia
        public static readonly int SincronizacionConDiferencias = 2;

        // Se produjo algun tipo de error cuando se intento sincronizar
        public static readonly int SincronizacionFallida = 3;
    }
}
