using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class TemplateModelOffice
    {
        public List<PersonOffice> EmployeesOffice { get; set; }
        public List<PersonCubicle> EmployeesCubicle { get; set; }

        public TemplateModelOffice()
        {
            EmployeesOffice = new List<PersonOffice>();
        }

        
        public void AddEmployeeOffice(PersonOffice newPerson)
        {
            EmployeesOffice.Add(newPerson);

        }
    }
}
