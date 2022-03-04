using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Models
{
    public class AppUser : IdentityUser // preko IdenUser se poveze na AspNetUsers
    {
        public string FullName { get; set; }
    }
}
