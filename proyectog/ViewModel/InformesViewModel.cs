using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoG.Models;

namespace ProyectoG.ViewModel
{
    public class InformesViewModel
    {
        public string Tipo_Informe { get; set; }
       public List<Informes> Informes { get; set; }
        public Informes Informe { get; set; }
        public List<tbl_Salones> Salones { get; set; }
    }
}