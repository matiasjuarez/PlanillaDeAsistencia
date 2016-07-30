using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace ContenedoresDeDatos
{
    public class ContenedorEstadosAsistencia : Contenedor<int, EstadoAsistencia>
    {
        public override void refrescarDatos()
        {
            limpiarContenedor();

            List<EstadoAsistencia> estadosAsistencia = DAOEstadoAsistencia.obtenerTodosLosEstadosAsistencia();
            foreach (EstadoAsistencia estadoAsistencia in estadosAsistencia)
            {
                datos.Add(estadoAsistencia.Id, estadoAsistencia);
            }
        }
    }
}
