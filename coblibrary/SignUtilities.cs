using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.ExtendedProperties;
 
namespace DoorSign.Models
{
    public class SignUtilities
    {
        void WordReplace(string find, string replace, string newDocument)
        {
            using WordprocessingDocument doc =
                    WordprocessingDocument.Open(@"D:\c sharp\door-sign\" + newDocument, true);
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
            using (var resultDoc = WordprocessingDocument.Create(@"D:\c sharp\door-sign\" + name,
              WordprocessingDocumentType.Document))
            {
                // copy parts from source document to new document
                foreach (var part in mainDoc.Parts)
                    resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
            }
        }


        string FindDepartment()
        {
            // prompt user and list all departments
            Console.WriteLine("Select department by number\n");
            int count = 1;
            foreach (string i in Enum.GetNames(typeof(Departments)))
            {
                System.Console.WriteLine(i + $" ({count})");
                Console.WriteLine();
                count++;
            }

            // convert their answer into enum and return string
            string val = Console.ReadLine();
            int x = Int32.Parse(val);
            x--;
            Departments department = (Departments)x;
            string s = department.ToString();
            s = s.Replace("_", " ");
            return s;
        }

        enum Departments
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


        public void CreateSignOffice(List<Person> personList, string department)
        {
            string templateName = @"D:\c sharp\door-sign\Offices\" + TemplatePersonOffice.FindTemplate(personList) + ".docx";
            string name = "test2.docx";
            CloneDocumentTemplate(templateName, name);

            int count = 1;
            foreach (TemplatePersonOffice person in personList)
            {
                WordReplace("First" + count, person.FirstName, name);
                WordReplace("Last" + count, person.LastName, name);
                WordReplace("Title" + count, person.Title, name);
                count++;
            }
            WordReplace("Department", department, name);
        }

        void CreateSignCubicle(List<TemplatePersonCubicle> personList, string department)
        {
            string templateName = @"D:\c sharp\door-sign\Cubicles\" + TemplatePersonCubicle.FindTemplate(personList) + ".docx";
            string name = "test2.docx";
            CloneDocumentTemplate(templateName, name);

            int count = personList.Count;
            foreach (TemplatePersonCubicle person in personList)
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
            WordReplace("Department", department, name);
        }
    }
}
