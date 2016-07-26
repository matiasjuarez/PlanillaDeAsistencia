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

        public String FinClaseEsperado
        {
            get {
                string hora = asistencia.FinClaseEsperado.ToString("HH:mm");
                return hora;
            }
            set
            {
                string horaFormateada = formatearHora(value);
                asistencia.FinClaseEsperado = DateTime.Parse(horaFormateada);
            }
        }

        public String ComienzoClaseReal
        {
            get {
                string hora = asistencia.ComienzoClaseReal.ToString("HH:mm");
                return hora;
            }
            set {
                string horaFormateada = formatearHora(value);
                asistencia.ComienzoClaseReal = DateTime.Parse(horaFormateada);
            }
        }

        public String FinClaseReal
        {
            get {
                string hora = asistencia.FinClaseReal.ToString("HH:mm");
                return hora;
            }
            set
            {
                string horaFormateada = formatearHora(value);
                asistencia.FinClaseReal = DateTime.Parse(horaFormateada);
            }
        }

        public String NombreProfesor
        {
            get {
                if (asistencia.Docente != null)
                {
                    return asistencia.Docente.Nombre;
                }
                else
                {
                    return stringPorDefecto;
                }
            }
            set { 
                asistencia.Docente.Nombre = value; 
            }
        }

        public String EstadoAsistencia
        {
            get {
                if (asistencia.EstadoAsistencia != null)
                {
                    return asistencia.EstadoAsistencia.Nombre;
                }
                else
                {
                    return stringPorDefecto;
                }
            }
            set { 
                asistencia.EstadoAsistencia.Nombre = value; 
            }
        }

        public int CantidadAlumnos
        {
            get { return asistencia.CantidadAlumnos; }
            set { asistencia.CantidadAlumnos = value; }
        }

        public int IdAsistencia
        {
            get { return asistencia.Id; }
            set { asistencia.Id = value; }
        }

        public string Encargados
        {
            get {
                if (asistencia.Encargado != null) return asistencia.Encargado.Nombre;
                else return stringPorDefecto;
            }
            set {
                asistencia.Encargado.Nombre = value;
            }
        }

        public string Observaciones
        {
            get {
                return asistencia.Observaciones;
            }
            set { 
                asistencia.Observaciones = value; 
            }
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

        private bool modificada;
        public bool esModificada()
        {
            return modificada;
        }
        public void setModificada(bool valor)
        {
            modificada = valor;
        }

        private bool seleccionada;
        public bool esSeleccionada()
        {
            return seleccionada;
        }
        public void setSeleccionada(bool valor)
        {
            seleccionada = valor;
        }
        
    }
}
