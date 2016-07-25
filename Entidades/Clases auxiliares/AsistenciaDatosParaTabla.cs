using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class AsistenciaDatosParaTabla: IComparable<AsistenciaDatosParaTabla>
    {
        private string stringPorDefecto = "N/A";
        public AsistenciaDatosParaTabla(Asistencia asistencia)
        {
            if (asistencia.Asignatura != null) NombreAsignatura = asistencia.Asignatura.Nombre;
            else NombreAsignatura = stringPorDefecto;

            ComienzoClaseEsperado = asistencia.ComienzoClaseEsperado.TimeOfDay.ToString();
            FinClaseEsperado = asistencia.FinClaseEsperado.TimeOfDay.ToString();
            ComienzoClaseReal = asistencia.ComienzoClaseReal.TimeOfDay.ToString();
            FinClaseReal = asistencia.FinClaseReal.TimeOfDay.ToString();

            if (asistencia.Docente != null) NombreProfesor = asistencia.Docente.Nombre;
            else NombreProfesor = stringPorDefecto;

            if (asistencia.EstadoAsistencia != null) EstadoAsistencia = asistencia.EstadoAsistencia.Nombre;
            else EstadoAsistencia = stringPorDefecto;

            CantidadAlumnos = asistencia.CantidadAlumnos;

            if (asistencia.Encargado != null) Encargados = asistencia.Encargado.getNombreCompleto();
            else Encargados = stringPorDefecto;

            IdAsistencia = asistencia.Id;
        }

        private String nombreAsignatura;
        public String NombreAsignatura
        {
            get { return nombreAsignatura; }
            set { nombreAsignatura = value; }
        }

        private String comienzoClaseEsperado;
        public String ComienzoClaseEsperado
        {
            get {
                return comienzoClaseEsperado;
            }
            set
            {
                comienzoClaseEsperado = formatearHora(value);
            }
        }

        private String finClaseEsperado;
        public String FinClaseEsperado
        {
            get {
                return finClaseEsperado;
            }
            set
            {
                finClaseEsperado = formatearHora(value);
            }
        }

        private String comienzoClaseReal;
        public String ComienzoClaseReal
        {
            get {
                return comienzoClaseReal;
            }
            set {
                comienzoClaseReal = formatearHora(value);
            }
        }

        private String finClaseReal;
        public String FinClaseReal
        {
            get {
                return finClaseReal;
            }
            set
            {
                finClaseReal = formatearHora(value);
            }
        }

        private String nombreProfesor;
        public String NombreProfesor
        {
            get { return nombreProfesor; }
            set { nombreProfesor = value; }
        }

        private String estadoAsistencia;
        public String EstadoAsistencia
        {
            get { return estadoAsistencia; }
            set { estadoAsistencia = value; }
        }

        private int cantidadAlumnos;
        public int CantidadAlumnos
        {
            get { return cantidadAlumnos; }
            set { cantidadAlumnos = value; }
        }

        private int idAsistencia;
        public int IdAsistencia
        {
            get { return idAsistencia; }
            set { idAsistencia = value; }
        }

        private string encargados;
        public string Encargados
        {
            get { return encargados; }
            set { encargados = value; }
        }

        private string observaciones;
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public int CompareTo(AsistenciaDatosParaTabla otraAsistencia)
        {
            DateTime estaFecha = DateTime.Parse(comienzoClaseEsperado);
            DateTime otraFecha = DateTime.Parse(otraAsistencia.comienzoClaseEsperado);

            return estaFecha.CompareTo(otraFecha);
        }

        private string formatearHora(string fechaSinFormato)
        {
            
            string [] separados = fechaSinFormato.Split(':');

            string fechaConFormato = "";

            try
            {
                if (separados[0] == "")
                {
                    separados[0] = "00";
                }
                if (separados[1] == "")
                {
                    separados[1] = "00";
                }

                fechaConFormato = separados[0] + ":" + separados[1];
            }
            catch
            {
                fechaConFormato = "00:00";
            }
            
            DateTime fechaAuxiliar;

            if(DateTime.TryParse(fechaConFormato, out fechaAuxiliar))
            {
                fechaConFormato = String.Format("{0:HH:mm}", fechaAuxiliar);
            }
            else{
                fechaConFormato = "00:00";
            }

            return fechaConFormato;
        }
    }
}
