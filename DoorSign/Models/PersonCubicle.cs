using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DoorSign.Models
{
    public class PersonCubicle : Person
    {
        [Required(ErrorMessage = "Please enter a letter.")]
        public string Letter { get; set; }
    }
}
