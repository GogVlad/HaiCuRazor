using System;
using System.Collections.Generic;

namespace RazorMvc.Models
{
    public class InternshipClass
    {
        private readonly List<Intern> _members;

        public InternshipClass()
        {
            _members = new List<Intern>();

            _members = new List<Intern>
            {
                new Intern { Name = "Borys", RegistrationDateTime = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Liova", RegistrationDateTime = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Orest", RegistrationDateTime = DateTime.Parse("2021-03-31") },
            };
        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}
