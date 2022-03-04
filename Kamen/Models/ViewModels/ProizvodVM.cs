using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models.ViewModels
{
    public class ProizvodVM
    {
        public Proizvod Proizvod { get; set; }
        public IEnumerable<SelectListItem> VrstaSelectList { get; set; }
        public IEnumerable<SelectListItem> TipAplSelectList { get; set; }
    }
}
