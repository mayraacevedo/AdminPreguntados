using ProyectoG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoG.ViewModel
{
    public class PreguntasSalonesView
    {
        public IEnumerable<tbl_Respuestas> Respuestas;
        public tbl_Salones Salones { get; set; }
        public List<tbl_Preguntas> Preguntas { get; set; }
        public tbl_Preguntas Pregunta { get; set; }

        
    }
}