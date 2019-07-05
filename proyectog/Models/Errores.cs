using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Errores
    {
        [Key]
        public int IdError { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Error { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public string Procedimiento { get; set; }
        public DateTime FechaHora { get; set; }

    }
}