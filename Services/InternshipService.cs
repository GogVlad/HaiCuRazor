using RazorMvc.Data;
using RazorMvc.Models;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new ();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public Intern AddMember(Intern member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void EditMember(Intern intern)
        {
            _internshipClass.Members[intern.Id] = intern;

        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }
    }
}