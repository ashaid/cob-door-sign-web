using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class TemplateModel
    {
        public List<Person> Employees { get; set; }

        public TemplateTypes TemplateType { get; set; }


        public TemplateModel()
        {
            Employees = new List<Person>();
        }
        public void AddEmployee(Person newPerson)
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
