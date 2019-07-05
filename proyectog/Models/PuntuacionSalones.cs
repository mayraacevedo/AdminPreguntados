using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_PuntuacionSalones
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Jugador")]
        public int IdJugador { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int Correctas { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int Incorrectas { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Column(TypeName = "float")]
        public float Nota { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(10, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
        public string IdAcceso { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public DateTime FechaCalificacion { get; set; }
    }
}