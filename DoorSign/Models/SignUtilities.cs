using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace DoorSign.Models
{
    public class SignUtilities
    {
        private IWebHostEnvironment host;
        public SignUtilities(IWebHostEnvironment host)
        {
            this.host = host;
        }
        void WordReplace(string find, string replace, string newDocument)
        {

            string path = host.ContentRootFileProvider.GetFileInfo("/wwwroot/template/built/").PhysicalPath;
            using WordprocessingDocument doc =
                    WordprocessingDocument.Open(path + newDocument, true);
            {
                var document = doc.MainDocumentPart.Document.Body;

                foreach (var text in document.Descendants<Text>())
                {
                    if (text.Text.Contains(find))
                    {
                        text.Text = text.Text.Replace(find, replace);
                        Console.WriteLine("found: " + find);
                        Console.WriteLine("replaced with: " + replace);
                    }
                }

            }

        }


        void CloneDocumentTemplate(string templateName, string name)
        {
            string path = host.ContentRootFileProvider.GetFileInfo("/wwwroot/template/built/").PhysicalPath;
            using (var mainDoc = WordprocessingDocument.Open(templateName, false))
            using (var resultDoc = WordprocessingDocument.Create(path + name,
              WordprocessingDocumentType.Document))
            {
                // copy parts from source document to new document
                foreach (var part in mainDoc.Parts)
                    resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
            }
        }

        string FindDepartment (string DepartmentNumber)
        {
            string s = "";
            switch (DepartmentNumber)
            {
                case "0":
                    s = "Please select a department";
                    break;
                case "1":
                    s = "Department of Accounting";
                    break;
                case "2":
                    s = "Department of Economics";
                    break;
                case "3":
                    s = "Department of Finance";
                    break;
                case "4":
                    s = "Department of Public Administration";
                    break;
                case "5":
                    s = "Flores MBA Program";
                    break;
                case "6":
                    s = "Flores MBA Program";
                    break;
                case "7":
                    s = "Rucks Department of Management";
                    break;
                case "8":
                    s = "Stephenson Department of Entrepreneurship & Information Systems";
                    break;
                case "9":
                    s = "Please select an institute";
                    break;
                case "10":
                    s = "Center for Internal Auditing";
                    break;
                case "11":
                    s = "Professional Sales Institute";
                    break;
                case "12":
                    s = "Real Estate Research Institute";
                    break;
                case "13":
                    s = "Stephenson Entrepreneurship Institute";
                    break;
                case "14":
                    s = "Please select a valid other choice";
                    break;
                case "15":
                    s = "Economics & Policy Research Group";
                    break;
                case "16":
                    s = "Executive Education";
                    break;
                case "17":
                    s = "External Relations";
                    break;
                case "18":
                    s = "Information Technology Group";
                    break;
                case "19":
                    s = "Office of Advancement";
                    break;
                case "20":
                    s = "Office of Business Student Success";
                    break;
                case "21":
                    s = "Office of Research & Graduate Programs";
                    break;
                case "22":
                    s = "Office of the Dean";
                    break;

            }
            return s;
        }
        public string FindTemplateOffice(List<PersonOffice> personList)
        {
            string templateName = "";

            if (!(personList[0].Professorship == null))
            {
                if (!(personList[0].SecondTitle == null))
                {
                    templateName = "/wwwroot/template/Offices/Office_One_Person_with_Two_Departments.docx";
                }
                else
                {
                    templateName = "/wwwroot/template/Offices/Office_One_Person_with_Chair_Template.docx";
                }

            }

            else if (!(personList[0].SecondTitle == null))
            {
                templateName = "/wwwroot/template/Offices/Office_One_Person_with_Two_Titles_Template.docx";
            }

            else
            {
                int length = personList.Count;
                TemplatesOffice template = (TemplatesOffice)length;
                templateName = "/wwwroot/template/Offices/" + template.ToString() + ".docx";
            }

            return templateName;
        }
        enum TemplatesOffice
        {
            Office_One_Person_Template = 1,
            Office_Two_People_Template,
            Office_Three_People_Template,
        }

        public string CreateSignOffice(List<PersonOffice> personList)
        {

            string templateName = FindTemplateOffice(personList);
            string name = personList[0].RoomNumber.ToString() + "_Office.docx";

            string path = host.ContentRootFileProvider.GetFileInfo(templateName).PhysicalPath;
            CloneDocumentTemplate(path, name);

            int count = 1;
            foreach (Person person in personList)
            {
                WordReplace("First" + count, person.FirstName, name);
                WordReplace("Last" + count, person.LastName, name);
                WordReplace("Title" + count, person.Title, name);
                count++;
            }

            WordReplace("Department", FindDepartment(personList[0].Department), name);
            WordReplace("RoomNumber", personList[0].RoomNumber.ToString(), name);

            WordReplace("Chair", personList[0].Professorship, name);
            WordReplace("SecondTitle", personList[0].SecondTitle, name);

            return name;
        }

        public string CreateSignCubicle(List<PersonCubicle> personList)
        {
            string templateName = "";
            switch(personList[0].CubeType)
            {
                case PersonCubicle.CubeTypes.One:
                    templateName = "/wwwroot/template/Cubicles/Cubicle_One_Person_Template.docx";
                    break;
                case PersonCubicle.CubeTypes.Two:
                    templateName = "/wwwroot/template/Cubicles/Cubicle_Two_People_Template.docx";
                    break;
                case PersonCubicle.CubeTypes.Three:
                    templateName = "/wwwroot/template/Cubicles/Cubicle_Three_People_Template.docx";
                    break;
            }
            string name = personList[0].RoomNumber.ToString() + "_Cubicle.docx";
            string path = host.ContentRootFileProvider.GetFileInfo(templateName).PhysicalPath;
            CloneDocumentTemplate(path, name);

            int count = personList.Count;
            foreach (PersonCubicle person in personList)
            {
                Console.WriteLine(count);
                if (count > 9)
                {
                    WordReplace(count + "First", personList[count - 1].FirstName, name);
                    WordReplace(count + "Last", personList[count - 1].LastName, name);
                    WordReplace(count + "Title", personList[count - 1].Title, name);
                    WordReplace(count + "L", personList[count - 1].Letter, name);
                }
                else
                {
                    WordReplace("First" + count, personList[count - 1].FirstName, name);
                    WordReplace("Last" + count, personList[count - 1].LastName, name);
                    WordReplace("Title" + count, personList[count - 1].Title, name);
                    WordReplace("L" + count, personList[count - 1].Letter, name);
                }

                count--;
            }
            WordReplace("Department", FindDepartment(personList[0].Department), name);
            WordReplace("RoomNumber", personList[0].RoomNumber.ToString(), name);

            return name;
        }
    }
}
