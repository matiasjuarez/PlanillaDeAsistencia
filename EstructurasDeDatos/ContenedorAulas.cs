using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace EstructurasDeDatos
{
    public class ContenedorAulas:Contenedor<int, Aula>
    {
        public override void refrescarDatos()
        {
            List<Aula> aulas = DAOAulas.obtenerTodasLasAulas();
            foreach (Aula aula in aulas)
            {
                datos.Add(aula.Id, aula);
            }
        }
    }
}
