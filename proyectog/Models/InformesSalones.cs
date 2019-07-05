using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_InformesSalones
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("tbl_Jugadores")] 
        public int IdJugador { get; set; }
        [ForeignKey("tbl_Preguntas")] 
        public int IdPregunta { get; set; }
        [ForeignKey("tbl_Salones")] 
        public int IdSalon { get; set; }
        public decimal Tiempo { get; set; }
        public Boolean Corecta { get; set; }

        public virtual tbl_Jugadores tbl_Jugadores { get; set; }
        public virtual tbl_Preguntas tbl_Preguntas { get; set; }
        public virtual tbl_Salones tbl_Salones { get; set; }

    }
}