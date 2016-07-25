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
            DefaultEncargadoAsistencia = new Entrada(-1);
            DefaultEstadoAsistencia = new Entrada(4);
        }
        
        public static Config getInstance()
        {
            if (configuracion == null)
            {
                configuracion = new Config();
            }

            return configuracion;
        }

        private Entrada defaultEstadoAsistencia;
        public Entrada DefaultEstadoAsistencia
        {
            get { return defaultEstadoAsistencia; }
            set { defaultEstadoAsistencia = value; }
        }

        private Entrada defaultEncargadoAsistencia;
        public Entrada DefaultEncargadoAsistencia
        {
            get { return defaultEncargadoAsistencia; }
            set { defaultEncargadoAsistencia = value; }
        }
    }
}
