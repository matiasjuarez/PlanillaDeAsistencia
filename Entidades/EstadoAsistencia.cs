using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    // Clase que se usa para almacenar el estado de una asistencia
    [Serializable]
    public class EstadoAsistencia : IEquatable<EstadoAsistencia>
    {
        private int id;
        private string nombre;
        private string abreviacion;
        private string descripcion;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Abreviacion
        {
            get { return abreviacion; }
            set { abreviacion = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public EstadoAsistencia()
        {
            this.abreviacion = "";
            this.descripcion = "";
            this.nombre = "";
        }

        public bool Equals(EstadoAsistencia other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }
    }
}
