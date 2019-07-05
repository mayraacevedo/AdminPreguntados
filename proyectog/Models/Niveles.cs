using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Niveles
    {
        [Key]
        [Display(Name = "Nivel")]
        public int Id_Nivel { get; set; }
        [Display(Name = "Nivel")]
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Column(TypeName = "VARCHAR")]
        public string Nivel { get; set; }
        [Display(Name = "Máximo Puntaje")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int MaximoPuntaje { get; set; }
        [Display(Name = "Puntos Pregunta")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int PuntosPregunta { get; set; }

        public virtual ICollection<tbl_Preguntas> tbl_Pregunta { get; set; }
        public virtual ICollection<tbl_Puntuaciones> tbl_Puntuacion { get; set; }

    }

}