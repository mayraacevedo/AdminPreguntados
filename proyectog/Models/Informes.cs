using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class Informes
    {
        public string Jugador { get; set; }
        [Display(Name = "Tiempo Promedio(Segundos)")]
        public int Tiempo_Promedio { get; set; }
        public string Pregunta { get; set; }
        public int Correctas { get; set; }
        public int Incorrectas { get; set; }

        public decimal Nota { get; set; }
        [Display(Name = "Fecha Calificación")]
        public DateTime Fecha_Calificacion { get; set; }
        

    }
}