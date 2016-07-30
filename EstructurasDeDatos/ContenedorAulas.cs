using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace ContenedoresDeDatos
{
    public class ContenedorAulas:Contenedor<int, Aula>
    {
        public override void refrescarDatos()
        {
            limpiarContenedor();

            List<Aula> aulas = DAOAulas.obtenerTodasLasAulas();
            foreach (Aula aula in aulas)
            {
                datos.Add(aula.Id, aula);
            }
        }
    }
}
