using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class PersonMisc : Person
    {
        public string? Heading { get; set; }

        public string? RText { get; set; }

        public int? RN { get; set; }

    }
}
