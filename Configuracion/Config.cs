using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion
{
    public class Config
    {
        private static Config configuracion;

        private Config() 
        {

        }
        
        public static Config getInstance()
        {
            if (configuracion == null)
            {
                configuracion = new Config();
            }

            return configuracion;
        }

        public string DocenteNoAsignado
        {
            get { return "No asignado"; }
        }

        public int IdDocenteNoAsignado
        {
            get { return -1; }
        }

        public string EncargadoNoAsignado{
            get { return "No asignado"; }
        }

        public int IdEncargadoNoAsignado
        {
            get { return -1; }
        }

        public string AsignaturaNoAsignada
        {
            get { return "No asignada"; }
        }

        public int IdAsignaturaNoAsignada
        {
            get { return -1; }
        }

        public string AulaNoAsignada
        {
            get { return "No asignada"; }
        }

        public string CursoNoAsignado
        {
            get { return "No asignado"; }
        }

        public int IdCursoNoAsignado
        {
            get { return -1; }
        }

        public string EstadoAsistenciaNoAsignado
        {
            get { return "No asignado"; }
        }

        public int IdEstadoAsistenciaNoAsignado
        {
            get { return -1; }
        }

        public DateTime ValorParaFechaNula
        {
            get { return new DateTime(1, 1, 1); }
        }

        public TimeSpan ValorParaHoraNula
        {
            get { return new TimeSpan(0, 0, 0); }
        }

    }
}
