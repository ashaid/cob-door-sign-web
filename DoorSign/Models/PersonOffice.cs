using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class PersonOffice : Person
    {
        public string Professorship { get; set; }
        
        public string SecondTitle { get; set; }
    }
}
