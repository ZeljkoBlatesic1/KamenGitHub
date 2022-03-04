using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models.ViewModels
{
    public class PrzdKorVM
    {
        // 2. constructor
        public PrzdKorVM() 
        {
            PrzdLista = new List<Proizvod>(); // Da ne izbacuje error 
        }

        // 1. AppUser, PrzdList
        public AppUser AppUser { get; set; }
        public IEnumerable<Proizvod> PrzdLista  { get; set; }
    }
}
