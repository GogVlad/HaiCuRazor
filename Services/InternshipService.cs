using RazorMvc.Data;
using RazorMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipClass.Members.Single(_ => _.Id == id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public Intern AddMember(Intern member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void EditMember(Intern intern)
        {
            var itemToBeUpdated = _internshipClass.Members.Single(_ => _.Id == intern.Id);
            itemToBeUpdated.Name = intern.Name;

        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }
    }
}