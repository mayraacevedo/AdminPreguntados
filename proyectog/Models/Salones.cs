using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Salones
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Administrador")]
        public int IdAdministrador { get; set; }
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Fecha Creacion")]
        public DateTime FechaCreacion { get; set; }

        [RegularExpression("/^[0-9a-zA-Z]+$/", ErrorMessage = "Sólo se permiten números y letras.")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Acceso")]
        [StringLength(4, ErrorMessage = "El campo {0} debe ser entre {1} y {2} caracteres", MinimumLength = 1)]
       
        public string IdAcceso { get; set; }
        public int PuntosPregunta { get; set; }
       
        public virtual ICollection<tbl_JugadoresSalones> tbl_JugadoresSalones { get; set; }
        public virtual ICollection<tbl_PreguntaSalones> tbl_PreguntaSalones { get; set; }
        public virtual ICollection<tbl_InformesSalones> tbl_InformesSalones { get; set; }


    }
}