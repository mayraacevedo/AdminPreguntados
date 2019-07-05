using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoG.Models
{
    public class tbl_JugadoresSalones
    {
        public int Id { get; set; }
        public int IdJugador { get; set; }
        public int IdSalon { get; set; }

        public virtual tbl_Salones tbl_Salones { get; set; }
        public virtual tbl_Jugadores tbl_Jugadores { get; set; }

    }
}