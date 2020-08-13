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
        public string? Letter { get; set; }

        public CubeTypes CubeType { get; set; }

        public enum CubeTypes
        {
            One,
            Two,
            Three
        }

    }
}
