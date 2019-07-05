using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Jugadores
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Nombres { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Apellidos { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Usuario { get; set; }

        [StringLength(20, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 5)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Password { get; set; }
        [StringLength(1, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Column(TypeName = "CHAR")]
        public string Genero { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        [EmailAddress(ErrorMessage = "El correo no tiene el formato correcto")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(10, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 10)]
        [DataType(DataType.Date)]
        public string FechaNacimiento { get; set; }
        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", Nombres, Apellidos); } }

        public virtual ICollection<tbl_Puntuaciones> tbl_Puntuacion { get; set; }
        
        public virtual ICollection<tbl_JugadoresSalones> tbl_JugadoresSalones { get; set; }
        public virtual ICollection<tbl_InformesSalones> Tbl_InformesSalones { get; set; }

    }
}