using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorMvc.Data;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService intershipService;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, IInternshipService intershipService, MessageService messagepService)
        {
            this.intershipService = intershipService;
            this.messageService = messagepService;
            _logger = logger;
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.RegistrationDateTime = DateTime.Now;
            return intershipService.AddMember(intern);
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
            intershipService.EditMember(intern);
        }


        public IActionResult Index()
        {
            var interns = intershipService.GetMembers();
            return View(interns);
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetMembers());
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
