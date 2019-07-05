using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Administradores
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Nombre { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Apellido { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 10)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [ForeignKey("tbl_Instituciones")]
        public int IdInstitucion { get; set; }
        [StringLength(200, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Column(TypeName = "varchar(MAX)")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [StringLength(20, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Rol { get; set; }
        [NotMapped]
        public Boolean Estado { get; set; }
        public virtual tbl_Instituciones tbl_Instituciones{ get; set;}
    
    }
}