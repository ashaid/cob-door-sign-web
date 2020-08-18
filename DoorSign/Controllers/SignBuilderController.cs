using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoorSign.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoorSign.Controllers
{
 
    public class SignBuilderController : Controller
    {

        private IWebHostEnvironment host;
        public SignBuilderController(IWebHostEnvironment host)
        {
           
            this.host = host;
        }

        [HttpGet]
        public ViewResult Office(int? numEmployees)
        {
            TemplateModelOffice templateModel = new TemplateModelOffice();


            while (numEmployees > 0)
            {
                PersonOffice p = new PersonOffice();
                templateModel.AddEmployeeOffice(p);
                numEmployees -= 1;
            }
            return View("Office",templateModel);
        }

        [HttpGet]
        public ViewResult Cubicle(int? numEmployees)
        {
            TemplateModelCubicle templateModel = new TemplateModelCubicle();

            while (numEmployees > 0)
            {
                PersonCubicle p = new PersonCubicle();
                templateModel.AddEmployeeCubicle(p);
                numEmployees -= 1;
            }
            return View("Cubicle", templateModel);
        }

        [HttpGet]
        public ViewResult Misc(int? numEmployees)
        {
            TemplateModelMisc templateModel = new TemplateModelMisc();

            while (numEmployees > 0)
            {
                PersonMisc p = new PersonMisc();
                templateModel.AddEmployeeMisc(p);
                numEmployees -= 1;
            }
            return View("Misc", templateModel);
        }
        [HttpGet]
        public ViewResult Misc2(int? numEmployees)
        {
            TemplateModelMisc templateModel = new TemplateModelMisc();

            while (numEmployees > 0)
            {
                PersonMisc p = new PersonMisc();
                templateModel.AddEmployeeMisc(p);
                numEmployees -= 1;
            }
            return View("Department", templateModel);
        }
        [HttpGet]
        public ViewResult Misc3(int? numEmployees)
        {
            TemplateModelMisc templateModel = new TemplateModelMisc();

            while (numEmployees > 0)
            {
                PersonMisc p = new PersonMisc();
                templateModel.AddEmployeeMisc(p);
                numEmployees -= 1;
            }
            return View("Elevator", templateModel);
        }

        [HttpPost]
        public ViewResult SavePersonOffice(TemplateModelOffice templateModel)
        {
            if (ModelState.IsValid)
            {
                //Put your code here to use data to generate the tamplate
                // create a pdf from the generated template
                //add the url for the PDF here so they can download it

                SignUtilities util = new SignUtilities(host);
                try
                {
                    string fileName = util.CreateSignOffice(templateModel.EmployeesOffice);
                    TempData["GeneratedFile"] = fileName;
                }
                catch
                {
                    
                }
                return View("Results");
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted error below.");
                return View("Office", templateModel);
            }


        }
        [HttpPost]
        public ViewResult SavePersonCubicle(TemplateModelCubicle templateModel)
        {

            if (ModelState.IsValid)
            {
                SignUtilities util = new SignUtilities(host);
                try
                {
                    string fileName = util.CreateSignCubicle(templateModel.EmployeesCubicle);
                    TempData["GeneratedFile"] = fileName;

                }
                catch
                {

                }

                return View("Results");
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted error below.");
                return View("Cubicle", templateModel);
            }
        }
        [HttpPost]
        public ViewResult SavePersonMisc(TemplateModelMisc templateModel)
        {

            if (ModelState.IsValid)
            {
                SignUtilities util = new SignUtilities(host);
                try
                {
                    string fileName = util.CreateSignMisc(templateModel.EmployeesMisc);
                    TempData["GeneratedFile"] = fileName;

                }
                catch
                {

                }

                return View("Results");
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted error below.");
                return View("Misc", templateModel);
            }
        }
        [HttpPost]
        public ViewResult SavePersonDepartment(TemplateModelMisc templateModel)
        {

            if (ModelState.IsValid)
            {
                SignUtilities util = new SignUtilities(host);
                try
                {
                    string fileName = util.CreateSignDepartment(templateModel.EmployeesMisc);
                    TempData["GeneratedFile"] = fileName;
                }
                catch
                {

                }

                return View("Results");
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted error below.");
                return View("Department", templateModel);
            }
        }
        [HttpPost]
        public ViewResult SavePersonElevator(TemplateModelMisc templateModel)
        {

            if (ModelState.IsValid)
            {
                SignUtilities util = new SignUtilities(host);
                try
                {
                    string fileName = util.CreateSignDepartment(templateModel.EmployeesMisc);
                    TempData["GeneratedFile"] = fileName;
                }
                catch
                {

                }

                return View("Results");
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted error below.");
                return View("Elevator", templateModel);
            }
        }
    }
}