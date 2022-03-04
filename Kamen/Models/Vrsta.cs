using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models
{
    public class Vrsta
    {
        [Key]
        public int Id { get; set; } //properties of Vrsta
        
        [Required]
        public string Naziv { get; set; }
        
        [DisplayName("Redoslijed prikaza")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage="Redoslijed prikaza mora biti veci od 0!")]
        public int RedPrikaz { get; set; } //Red - redoslijed, Prikaz
    }
}
