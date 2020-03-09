using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemBackend.Web.Models
{
    public class UnitViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 50 caracteres ni menos de 3")]
        public string name { get; set; }
    }
}
