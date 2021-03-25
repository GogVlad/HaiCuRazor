using System.Collections.Generic;

namespace RazorMvc.Models
{
    public class InternshipClass
    {
        private readonly List<string> _members;

        public InternshipClass()
        {
            _members = new List<string>
            {
                "Ale",
                "Peter",
                "Radu",
            };
        }

        public IList<string> Members
        {
            get { return _members; }
        }
    }
}
