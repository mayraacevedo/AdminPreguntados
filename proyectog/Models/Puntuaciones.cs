using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Puntuaciones
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Jugador")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [ForeignKey("tbl_Jugadores")]
        public int IdJugador { get; set; }
        [Display(Name = "Pensamiento Logico")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int PensamientoLogico { get; set; }
        [Display(Name = "Jugador")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int MatematicaBasica { get; set; }
        [Display(Name = "Logica Computacional")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int LogicaComputacional { get; set; }
        [Display(Name = "Fundamentos Programacion")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int FundamentosProgramacion { get; set; }
        [ForeignKey("tbl_Niveles")]
        [Display(Name = "Jugador")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int Nivel { get; set; }

        public virtual tbl_Niveles tbl_Niveles { get; set; }
        public virtual tbl_Jugadores tbl_Jugadores { get; set; }

    }
}