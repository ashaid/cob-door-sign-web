using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class TemplateModelCubicle
    {
        public List<PersonCubicle> EmployeesCubicle { get; set; }

        public TemplateModelCubicle()
        {
            EmployeesCubicle = new List<PersonCubicle>();
        }

        public void AddEmployeeCubicle(PersonCubicle newPerson)
        {
            EmployeesCubicle.Add(newPerson);

        }
    }
}
