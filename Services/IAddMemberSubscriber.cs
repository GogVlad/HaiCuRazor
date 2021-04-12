using RazorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Services
{
    public interface IAddMemberSubscriber
    {
        void OnAddMember(Intern member);
    }
}
