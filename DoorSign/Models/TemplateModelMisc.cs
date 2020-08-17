using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class TemplateModelMisc
    {
        public List<PersonMisc> EmployeesMisc { get; set; }

        public TemplateModelMisc()
        {
            EmployeesMisc = new List<PersonMisc>();
        }


        public void AddEmployeeMisc(PersonMisc newPerson)
        {
            EmployeesMisc.Add(newPerson);

        }
    }
}
