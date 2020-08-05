using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class TemplateModel
    {
        public List<PersonOffice> Employees { get; set; }

        public TemplateTypes TemplateType { get; set; }


        public TemplateModel()
        {
            Employees = new List<PersonOffice>();
        }
        public void AddEmployee(PersonOffice newPerson)
        {
            Employees.Add(newPerson);

        }

    }

     public enum TemplateTypes
    {
        Office,
        Cubical
    }



}
