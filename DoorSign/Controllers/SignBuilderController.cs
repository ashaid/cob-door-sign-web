using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoorSign.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace DoorSign.Controllers
{
 
    public class SignBuilderController : Controller
    {
        [HttpGet]
        public ViewResult Office(int? numEmployees)
        {
            TemplateModel templateModel = new TemplateModel();


            while (numEmployees > 0)
            {
                PersonOffice p = new PersonOffice();
                templateModel.AddEmployee(p);
                numEmployees -= 1;
            }
            return View("Office",templateModel);
        }

        public ViewResult Cubicle(int? numEmployees)
        {
            TemplateModel templateModel = new TemplateModel();

            while (numEmployees > 0)
            {
                PersonOffice p = new PersonOffice();
                templateModel.AddEmployee(p);
                numEmployees -= 1;
            }
            return View("Cubicle", templateModel);
        }

        [HttpPost]
        public ViewResult SavePerson(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                //Put your code here to use data to generate the tamplate
                // create a pdf from the generated template
                //add the url for the PDF here so they can download it

                SignUtilities util = new SignUtilities();

                util.CreateSignOffice(templateModel.Employees);

                string filen = @"~\wwwroot\templates\" + "test2.pdf";

                ViewBag.PDFUrl = filen;
                return View("Results");
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted error below.");
                return View("Office", templateModel);
            }


        }
    }
}