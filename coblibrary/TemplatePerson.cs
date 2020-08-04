using System;
using System.Collections.Generic;
using System.Text;

namespace coblibrary
{
    class TemplatePerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public TemplatePerson(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
    }
}
