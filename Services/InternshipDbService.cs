﻿using System;
using System.Collections.Generic;
using System.Linq;
using RazorMvc.Data;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;

        public InternshipDbService(InternDbContext db)
        {
            this.db = db;
        }

        public Intern AddMember(Intern member)
        {
            db.Interns.AddRange(member);
            db.SaveChanges();
            return member;
        }

        public void EditMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id);
            itemToBeUpdated.Name = intern.Name;
            if (intern.RegistrationDateTime == DateTime.MinValue)
            {
                intern.RegistrationDateTime = DateTime.Now;
            }

            db.Interns.Update(itemToBeUpdated);
            db.SaveChanges();
        }

        public Intern GetMemberById(int id)
        {
            var intern = db.Find<Intern>(id);
            db.Entry(intern).Reference(_ => _.Location).Load();
            return intern;
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public void RemoveMember(int id)
        {
            var intern = db.Find<Intern>(id);
            if (intern == null)
            {
                return;
            }

            db.Remove<Intern>(intern);
            db.SaveChanges();
        }
    }
}
