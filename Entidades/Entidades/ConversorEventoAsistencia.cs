using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ConversorEventoAsistencia
    {
        public static Asistencia convertirEventoEnAsistencia(Evento evento)
        {
            Asistencia asistencia = new Asistencia();

            asistencia.AppointmentId = evento.AppointmentId;
            asistencia.EventId = evento.IDEvento;

            asistencia.ComienzoClaseEsperado = evento.InicioEsperado.TimeOfDay;
            asistencia.FinClaseEsperado = evento.FinEsperado.TimeOfDay;
            asistencia.DiaDeAsistencia = evento.InicioEsperado.Date;

            asistencia.Docente = new Docente(evento.Docente);

            asistencia.Asignatura = new Asignatura();
            asistencia.Asignatura.Nombre = evento.Materia;
            
            string[] aulasNombres = evento.Aula.Split(',');
            foreach (string aulaNombre in aulasNombres)
            {
                Aula aula = new Aula();
                aula.Nombre = aulaNombre;
                asistencia.agregarAula(aula);
            }            

            asistencia.Curso = new Curso();
            asistencia.Curso.Nombre = evento.Curso;
            
            return asistencia;
        }
    }
}
