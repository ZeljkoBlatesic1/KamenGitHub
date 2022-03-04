using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Proizvod> Proizvodi { get; set; }
        public IEnumerable<Vrsta> Vrste { get; set; }
    }
}
