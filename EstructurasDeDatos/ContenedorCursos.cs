using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace ContenedoresDeDatos
{
    public class ContenedorCursos : Contenedor<int, Curso>
    {
        public override void refrescarDatos()
        {
            limpiarContenedor();

            List<Curso> cursos = DAOCursos.obtenerTodosLosCursos();
            foreach (Curso curso in cursos)
            {
                datos.Add(curso.Id, curso);
            }
        }
    }
}
