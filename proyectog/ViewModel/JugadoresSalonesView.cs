using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoG.Models;

namespace ProyectoG.ViewModel
{
    public class JugadoresSalonesView
    {
        public List<tbl_Jugadores> Jugadores;
        public tbl_Salones Salones { get; set; }
        public tbl_Jugadores Jugador { get; set; }

      public int CantJugadores { get; set; }

    }
}