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


        string FindDepartmentOffice(List<PersonOffice> personList)
        {
            string s = personList[0].Department.ToString();
            s = s.Replace("_", " ");
            return s;
        }

        string FindDepartmentCubicle(List<PersonCubicle> personList)
        {
            string s = personList[0].Department.ToString();
            s = s.Replace("_", " ");
            return s;
        }


        public string FindTemplateOffice(List<PersonOffice> personList)
        {
            int length = personList.Count;
            TemplatesOffice template = (TemplatesOffice)length;
            return template.ToString();
        }
        enum TemplatesOffice
        {
            Office_One_Person_Template = 1,
            Office_Two_People_Template,
            Office_Three_People_Template,

            Office_One_Person_with_Two_Titles_Template = 20,
            Office_One_Person_with_Professorship_Template,
            Office_Two_PhD_Students_Template,
        }

        public string CreateSignOffice(List<PersonOffice> personList)
        {
            string templateName = "";
            if (!(personList[0].Professorship == null))
            {
                if (!(personList[0].SecondTitle == null))
                {
                    templateName = "/wwwroot/template/Offices/Office_One_Person_with_Two_Titles_Template.docx";
                }
                else
                {
                    templateName = "/wwwroot/template/Offices/Office_One_Person_with_Professorship_Template.docx";
                }
                
            }
            else
            {
                templateName = "/wwwroot/template/Offices/" + FindTemplateOffice(personList) + ".docx";
            }
            
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

            WordReplace("Department", FindDepartmentOffice(personList), name);
            WordReplace("RoomNumber", personList[0].RoomNumber.ToString(), name);

            WordReplace("Professorship", personList[0].Professorship, name);
            WordReplace("SecondTitle", personList[0].SecondTitle, name);

            return name;
        }

        public string CreateSignCubicle(List<PersonCubicle> personList)
        {
            string templateName = "";
            Console.WriteLine(personList[0].AmtName.ToString());
            switch (personList[0].AmtName)
            {
                case PersonCubicle.CubeType.One:
                    templateName = "/wwwroot/template/Cubicles/Cubicle_One_Person_Template.docx";
                    break;
                case PersonCubicle.CubeType.Two:
                    templateName = "/wwwroot/template/Cubicles/Cubicle_Two_People_Template.docx";
                    Console.WriteLine("hi");
                    break;
                case PersonCubicle.CubeType.Three:
                    templateName = "/wwwroot/template/Cubicles/Cubicle_Three_People_Template.docx";
                    break;
            }
            string name = personList[0].RoomNumber.ToString() + "_Cubicle.docx";
            CloneDocumentTemplate(templateName, name);

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
            WordReplace("Department", FindDepartmentCubicle(personList), name);
            WordReplace("RoomNumber", personList[0].RoomNumber.ToString(), name);

            return name;
        }
    }
}
