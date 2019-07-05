using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Preguntas
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [ForeignKey("tbl_Categorias")]
        public int Id_Categoria { get; set; }
        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [ForeignKey("tbl_Niveles")]
        public int Id_Nivel { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.MultilineText)]
        public string Pregunta { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [Url(ErrorMessage = "La dirección del sitio Web no es válida")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.Url)]
        public string Link { get; set; }
        public int IdAdministrador { get; set; }
        public bool Privada { get; set; }

        public virtual ICollection<tbl_Respuestas> tbl_Respuesta { get; set; }

        public virtual tbl_Categorias tbl_Categorias { get; set; }
        public virtual tbl_Niveles tbl_Niveles { get; set; }
        public virtual ICollection<tbl_PreguntaSalones> Tbl_PreguntaSalones { get; set; }
        public virtual ICollection<tbl_InformesSalones> Tbl_InformesSalones { get; set; }




    }
}