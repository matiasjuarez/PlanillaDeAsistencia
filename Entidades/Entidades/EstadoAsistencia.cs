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
        public const int Ausente = 0;
        public const int AusenteConAviso = 1;
        public const int Presente = 2;
        public const int SinEspecificar = 3;

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
        }

        /*public EstadoAsistencia(int tipoEstado)
        {
            switch (tipoEstado)
            {
                case EstadoAsistencia.Ausente:
                    Nombre = "Ausente";
                    Abreviacion = "A";
                    break;

                case EstadoAsistencia.AusenteConAviso:
                    Nombre = "Ausente con aviso";
                    Abreviacion = "A C/A";
                    break;

                case EstadoAsistencia.Presente:
                    Nombre = "Presente";
                    Abreviacion = "P";
                    break;

                case EstadoAsistencia.SinEspecificar:
                    Nombre = "Sin especificar";
                    Abreviacion = "S/E";
                    break;

                default:
                    Nombre = "Sin especificar";
                    Abreviacion = "S/E";
                    break;
            }
        }*/

        public bool Equals(EstadoAsistencia other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }


       /* public static bool operator ==(EstadoAsistencia d1, EstadoAsistencia d2)
        {
            if (d1 == null || d2 == null)
            {
                return Object.Equals(d1, d2);
            }

            return d1.Equals(d2);
        }

        public static bool operator !=(EstadoAsistencia d1, EstadoAsistencia d2)
        {
            if (d1 == null || d2 == null)
            {
                return !Object.Equals(d1, d2);
            }

            return !d1.Equals(d2);
        }*/
    }
}
