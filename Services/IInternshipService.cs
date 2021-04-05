using RazorMvc.Models;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);
        void EditMember(Intern intern);
        IList<Intern> GetMembers();
        void RemoveMember(int index);
    }
}