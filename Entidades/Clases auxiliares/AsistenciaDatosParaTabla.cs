using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class AsistenciaDatosParaTabla: IComparable<AsistenciaDatosParaTabla>
    {
        private string stringPorDefecto = "N/A";
        private Asistencia asistencia;

        public AsistenciaDatosParaTabla(Asistencia asistencia)
        {
            this.asistencia = asistencia;

            

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

        public String NombreAsignatura
        {
            get {
                if (asistencia.Asignatura != null) return asistencia.Asignatura.Nombre;
                else return stringPorDefecto;
            }
            set {
                asistencia.Asignatura.Nombre = value;
            }
        }

        public String ComienzoClaseEsperado
        {
            get {
                string hora = asistencia.ComienzoClaseEsperado.ToString("HH:mm");
                return hora;
            }
            set
            {
                string horaFormateada =  formatearHora(value);
                asistencia.ComienzoClaseEsperado = DateTime.Parse(horaFormateada);
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
            DateTime estaFecha = DateTime.Parse(ComienzoClaseEsperado);
            DateTime otraFecha = DateTime.Parse(otraAsistencia.ComienzoClaseEsperado);

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

        private bool estaModificada;
        public bool EstaModificada
        {
            get { return estaModificada; }
            set { estaModificada = value; }
        }

        private bool estaSeleccionada;
        public bool EstaSeleccionada
        {
            get { return estaSeleccionada; }
            set { estaSeleccionada = value; }
        }
        
    }
}
