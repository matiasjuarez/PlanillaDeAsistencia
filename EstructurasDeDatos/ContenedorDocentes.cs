using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace ContenedoresDeDatos
{
    public class ContenedorDocentes : Contenedor<int, Docente>
    {
        public override void refrescarDatos()
        {
            limpiarContenedor();

            List<Docente> docentes = DAODocentes.obtenerTodosLosDocentes();
            foreach (Docente docente in docentes)
            {
                datos.Add(docente.Id, docente);
            }
        }
        
    }
}
