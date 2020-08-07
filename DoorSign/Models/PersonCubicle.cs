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

        [Required(ErrorMessage = "Please select how many names on one sign.")]
        public CubeType AmtName { get; set; }

        public enum CubeType
        {
            One,
            Two,
            Three
        }
        
    }
}
