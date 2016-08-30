using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class Asignatura : IEquatable<Asignatura>
    {
        private int id;
        private string nombre;
        private Especialidad especialidad;
        private Docente jefeCatedra;

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

        public Especialidad Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }

        public Docente JefeCatedra
        {
            get { return jefeCatedra; }
            set { jefeCatedra = value; }
        }

        public Asignatura()
        {
            id = 0;
            nombre = String.Empty;
            jefeCatedra = new Docente();
            especialidad = new Especialidad();
        }

        public bool Equals(Asignatura other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }
    }
}
