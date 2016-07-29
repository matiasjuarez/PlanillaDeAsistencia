using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace EstructurasDeDatos
{
    public class ContenedorEstadosAsistencia : Contenedor<int, EstadoAsistencia>
    {
        public override void refrescarDatos()
        {
            List<EstadoAsistencia> estadosAsistencia = DAOEstadoAsistencia.obtenerTodosLosEstadosAsistencia();
            foreach (EstadoAsistencia estadoAsistencia in estadosAsistencia)
            {
                datos.Add(estadoAsistencia.Id, estadoAsistencia);
            }
        }
    }
}
