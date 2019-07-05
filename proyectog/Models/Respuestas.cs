using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Respuestas
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Pregunta")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [ForeignKey("tbl_Preguntas")]
        public int Id_Pregunta { get; set; }
        [StringLength(200, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Column(TypeName = "varchar")]
        public string Respuesta { get; set; }
    
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public Boolean Respuesta_Correcta { get; set; }

        public virtual tbl_Preguntas tbl_Preguntas { get; set; }

    }
}