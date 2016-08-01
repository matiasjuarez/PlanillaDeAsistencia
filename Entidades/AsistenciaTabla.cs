using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class AsistenciaTabla: IComparable<AsistenciaTabla>
    {
        private Asistencia asistencia;
        public Asistencia obtenerAsistencia()
        {
            return asistencia;
        }

        private string stringPorDefecto = "N/A";
        private string formatoTimespan = @"hh\:mm";

        public AsistenciaTabla(Asistencia asistencia)
        {
            this.asistencia = asistencia;

            FinClaseEsperado = asistencia.HoraSalidaEsperada.ToString(formatoTimespan);
            ComienzoClaseReal = asistencia.HoraEntradaReal.ToString(formatoTimespan);
            FinClaseReal = asistencia.HoraSalidaReal.ToString();

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
                string hora = asistencia.HoraEntradaEsperada.ToString(formatoTimespan);
                return hora;
            }
            set
            {
                string horaFormateada =  formatearHora(value);
                asistencia.HoraEntradaEsperada = TimeSpan.Parse(horaFormateada);
            }
        }

        public String FinClaseEsperado
        {
            get {
                string hora = asistencia.HoraSalidaEsperada.ToString(formatoTimespan);
                return hora;
            }
            set
            {
                string horaFormateada = formatearHora(value);
                asistencia.HoraSalidaEsperada = TimeSpan.Parse(horaFormateada);
            }
        }

        public String ComienzoClaseReal
        {
            get {
                string hora = asistencia.HoraEntradaReal.ToString(formatoTimespan);
                return hora;
            }
            set {
                string horaFormateada = formatearHora(value);
                asistencia.HoraEntradaReal = TimeSpan.Parse(horaFormateada);
            }
        }

        public String FinClaseReal
        {
            get {
                string hora = asistencia.HoraSalidaReal.ToString(formatoTimespan);
                return hora;
            }
            set
            {
                string horaFormateada = formatearHora(value);
                asistencia.HoraSalidaReal = TimeSpan.Parse(horaFormateada);
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

        public string Aula
        {
            get
            {
                string str = "";
                foreach (Aula aula in asistencia.Aulas)
                {
                    str += aula.Nombre + ",";
                }

                if (str.Length > 0)
                {
                    str = str.Remove(str.Length - 1);
                }

                return str;
            }
        }

        public int CompareTo(AsistenciaTabla otraAsistencia)
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
