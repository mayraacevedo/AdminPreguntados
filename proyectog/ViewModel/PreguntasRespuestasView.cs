using ProyectoG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.ViewModel
{
    public class PreguntasRespuestasView
    {
        public IEnumerable<tbl_Respuestas> Respuestas;
        public tbl_Preguntas Preguntas { get; set; }
        public tbl_Respuestas Respuesta1 { get; set; }
        public tbl_Respuestas Respuesta2 { get; set; }
        public tbl_Respuestas Respuesta3 { get; set; }



    }
}