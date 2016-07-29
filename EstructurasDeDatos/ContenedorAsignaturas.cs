using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace EstructurasDeDatos
{
    public class ContenedorAsignaturas : Contenedor<int, Asignatura>
    {
        public override void refrescarDatos()
        {
            List<Asignatura> asignaturas = DAOAsignaturas.obtenerTodasLasAsignaturas();
            foreach (Asignatura asignatura in asignaturas)
            {
                datos.Add(asignatura.Id, asignatura);
            }
        }
    }
}