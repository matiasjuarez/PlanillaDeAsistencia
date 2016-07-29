using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using AccesoDatos;

namespace DatosLocales.Contenedores
{
    public class ContenedorCursos : Contenedor<int, Curso>
    {
        public override void refrescarDatos()
        {
            List<Curso> cursos = DAOCursos.obtenerTodosLosCursos();
            foreach (Curso curso in cursos)
            {
                datos.Add(curso.Id, curso);
            }
        }
    }
}
