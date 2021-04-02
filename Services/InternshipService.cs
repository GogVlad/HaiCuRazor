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

        public string AddMember(string member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void EditMember(int index, string name)
        {
            _internshipClass.Members[index] = name;

        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }
    }
}