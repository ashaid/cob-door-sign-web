using System;
using System.Collections.Generic;
using System.Text;

namespace coblibrary
{
    public class TemplatePersonOffice : TemplatePerson
    {
        public string Title { get; set; }

        public TemplatePersonOffice(string Title, string FirstName, string LastName) : base(FirstName, LastName)
        {
            this.Title = Title;
        }

        public static List<TemplatePersonOffice> Prompt()
        {
            // initialize
            List<TemplatePersonOffice> personList = new List<TemplatePersonOffice>();
            // prompt for number of people to be on the sign
            Console.WriteLine("How many people are in this office? (1-3)");
            string numPeople = Console.ReadLine();
            int x = Int32.Parse(numPeople);

            // create a person for the number of people entered
            for (int i = 0; i < x; i++)
            {
                if (i < 0)
                {
                    string temp = Console.ReadLine();
                }

                // first name, last name, and title prompts
                Console.WriteLine("Enter First Name: ");
                string fName = Console.ReadLine();

                Console.WriteLine("Enter Last Name: ");
                string lName = Console.ReadLine();

                Console.WriteLine("Enter Title: ");
                string title = Console.ReadLine();

                // add person to template person list
                personList.Add(new TemplatePersonOffice(title, fName, lName));
            }

            return personList;
        }

        public static string FindTemplate(List<TemplatePersonOffice> personList)
        {
            int length = personList.Count;
            Templates template = (Templates)length;
            return template.ToString();
        }

        enum Templates
        {
            Office_One_Person_Template = 1,
            Office_Two_People_Template,
            Office_Three_People_Template,

            Office_One_Person_with_Two_Titles_Template = 20,
            Office_One_Person_with_Professorship_Template,
            Office_Two_PhD_Students_Template,
        }
    }
}
