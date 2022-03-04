using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models
{
    public class Proizvod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Display(Name = "Kratak opis")]
        public string KratOpis { get; set; }

        public string Opis { get; set; }

        [Range(1, 1200)]
        public double Cijena { get; set; }

        public string Slika { get; set; }

        [Display(Name ="Tip vrste")]
        public int VrstaId { get; set; }
        [ForeignKey("VrstaId")]
        public virtual Vrsta Vrsta { get; set; } // EF code for mapping btwn Przd i Vrst 
        //create Id column of Vrsta, wich is FK in relation

        [Display(Name = "Tip aplikacije")]
        public int TipAplId { get; set; }
        [ForeignKey("TipAplId")]
        public virtual TipApl TipApl { get; set; }
    }
}
