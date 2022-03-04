using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models.ViewModels
{
    public class DetaljiVM
    {
        public DetaljiVM()
        {
            Proizvod = new Proizvod(); // da se ne radi deklaracija
        }                              //      u kontroleru 

        public Proizvod Proizvod { get; set; }

        public bool ImaGaKart { get; set; }
    }
}
