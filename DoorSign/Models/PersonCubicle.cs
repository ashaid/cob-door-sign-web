using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoorSign.Models
{
    public class PersonCubicle : Person
    {
        [Required(ErrorMessage = "Please enter a letter.")]
        public string Letter { get; set; }

        [Required(ErrorMessage = "Please select how many names on one sign.")]
        public CubeTypes CubeType { get; set; }

        public enum CubeTypes
        {
            One,
            Two,
            Three
        }

    }
}
