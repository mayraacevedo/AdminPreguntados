using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Retos
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Retador")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int IdRetador { get; set; }
        [Display(Name = "Retado")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int IdRetado { get; set; }
        [Display(Name = "Vencedor")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int IdVencedor { get; set; }
    }
}