﻿using System;
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
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, IInternshipService intershipService, InternDbContext db)
        {
            this.intershipService = intershipService;
            _logger = logger;
            this.db = db;
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
        public void EditMember(int index, string name)
        {
            Intern intern = new Intern();
            intern.Id = index;
            intern.Name = name;
            intershipService.EditMember(intern);
        }


        public IActionResult Index()
        {
            var interns = db.Interns.ToList();
            return View(interns);
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetMembers());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
