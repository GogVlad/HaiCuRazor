using System.Collections.Generic;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);

        void EditMember(Intern intern);

        IList<Intern> GetMembers();

        void RemoveMember(int index);


        Intern GetMemberById(int id);
    }
}