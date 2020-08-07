using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.IO;

namespace DoorSign.Models
{
    public class SignUtilities
    {

        void WordReplace(string find, string replace, string newDocument)
        {
            using WordprocessingDocument doc =
                    WordprocessingDocument.Open(@"C:\Users\antho\Desktop\DoorSign\DoorSign\wwwroot\templates\" + newDocument, true);
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
            using (var mainDoc = WordprocessingDocument.Open(templateName, false))
            using (var resultDoc = WordprocessingDocument.Create(@"C:\Users\antho\Desktop\DoorSign\DoorSign\wwwroot\templates\" + name,
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
                templateName = @"C:\Users\antho\Desktop\DoorSign\DoorSign\wwwroot\templates\Offices\Office_One_Person_with_Professorship_Template.docx";
            }
            else
            {
                templateName = @"C:\Users\antho\Desktop\DoorSign\DoorSign\wwwroot\templates\Offices\" + FindTemplateOffice(personList) + ".docx";
            }
            
            string name = "test2.docx";
            CloneDocumentTemplate(templateName, name);

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

            string fileName = "test2.docx";
            return fileName;
        }

        public string  CreateSignCubicle(List<PersonCubicle> personList)
        {
            string templateName = "";
            switch (personList[0].AmtName)
            {
                case PersonCubicle.CubeType.One:
                    templateName = @"~\templates\Cubicles\Cubicle_One_Person_Template.docx";
                    break;
                case PersonCubicle.CubeType.Two:
                    templateName = @"~\templates\Cubicles\Cubicle_Two_People_Template.docx";
                    break;
                case PersonCubicle.CubeType.Three:
                    templateName = @"~\templates\Cubicles\Cubicle_Three_People_Template.docx";
                    break;
            }
            string name = "test2.docx";
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

            string fileName = "test2.pdf";
            return fileName;
        }
    }
}
