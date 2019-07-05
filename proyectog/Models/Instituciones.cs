using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Instituciones
    {
        [Key]
        public int IdInstitucion { get; set; }
        [StringLength(500, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Display(Name = "Institucion")]
        public string Nombre { get; set; }

        public virtual ICollection<tbl_Administradores> tbl_Administradores { get; set; }
    }
}