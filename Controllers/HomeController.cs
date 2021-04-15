using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RazorMvc.Hubs;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService internshipService;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, IInternshipService intershipService, IHubContext<MessageHub> hubContext, MessageService messageService)
        {
            this.internshipService = intershipService;
            this.messageService = messageService;
            this.hubContext = hubContext;
            _logger = logger;
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            internshipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern
            {
                Name = memberName,
                RegistrationDateTime = DateTime.Now,
            };
            var newMember = internshipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
            return newMember;
        }

        [HttpPut]
        public void EditMember(int id, string memberName)
        {
            var intern = new Intern
            {
                Id = id,
                Name = memberName,
                RegistrationDateTime = DateTime.Now,
            };
            internshipService.EditMember(intern);
        }

        public IActionResult Index()
        {
            var interns = internshipService.GetMembers();
            return View(interns);
        }

        public IActionResult Privacy()
        {
            return View(internshipService.GetMembers());
        }

        public IActionResult Chat()
        {
            return View(messageService.GetAllMessages());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
