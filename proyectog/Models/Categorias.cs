using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Categorias
    {
        [Key]
        [Display(Name = "Categoria")]
        public int Id { get; set; }
        [Display(Name ="Categoria")]
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Column(TypeName = "VARCHAR")]
        public string Categoria { get; set; }

        public virtual ICollection<tbl_Preguntas> tbl_Pregunta { get; set; }






    }
}