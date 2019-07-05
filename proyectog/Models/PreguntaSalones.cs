using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_PreguntaSalones
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("tbl_Salones")]
        public int IdSalon { get; set; }
        [ForeignKey("tbl_Preguntas")]
        public int IdPregunta { get; set; }

        public virtual tbl_Salones tbl_Salones { get; set; }
        public virtual tbl_Preguntas tbl_Preguntas { get; set; }

    }
}