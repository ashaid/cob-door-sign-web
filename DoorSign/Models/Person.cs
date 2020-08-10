using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSign.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name.")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select a department.")]
        public Departments? Department { get; set; }

        [Required(ErrorMessage = "Please enter a room number.")]
        public int? RoomNumber { get; set; }
        
    }
    public enum Departments
    {
        Department_of_Accounting,
        Department_of_Economics,
        Department_of_Finance,
        SDEIS,
        Rucks_Department_of_Managemnt,
        Department_of_Marketing,
        Department_of_Public_Administration,
        Flores_MBA_Program
    }

}
