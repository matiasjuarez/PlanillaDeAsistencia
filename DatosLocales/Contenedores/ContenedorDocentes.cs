using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace DatosLocales.Contenedores
{
    public class ContenedorDocentes : Contenedor<int, Docente>
    {
        public override void refrescarDatos()
        {
            List<Docente> docentes = DAODocentes.obtenerTodosLosDocentes();
            foreach (Docente docente in docentes)
            {
                datos.Add(docente.Id, docente);
            }
        }
        
    }
}
