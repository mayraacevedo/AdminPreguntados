using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_Amigos
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Jugador Principal")]
       
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int IdJugadorPrincipal { get; set; }
        [Display(Name = "Jugador Amigo")]
        
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int IdJugadorAmigo { get; set; }
        
    }
}