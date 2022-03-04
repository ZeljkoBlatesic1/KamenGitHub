using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models
{
    public class TipApl
    {
        [Key]
        public int Id { get; set; } //properties of Vrsta
        [Required]
        public string Naziv { get; set; }
    }
}
