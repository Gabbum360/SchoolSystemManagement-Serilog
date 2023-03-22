using Business_Logic_Layer.Interfaces;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class SchoolClass_Logic : ISchoolClass
    {
        private SchoolSystemDbContext SMDContext;
        public SchoolClass_Logic(SchoolSystemDbContext SMDb)
        {
            SMDContext = SMDb;    
        }

        public async Task<List<Class>> GetClasses()
        {
            List<GetClass> classes = new List<GetClass>();
            var listFromDb = await SMDContext.Classes.ToListAsync();
            foreach (var item in listFromDb)
            {
                GetClass cl = new GetClass();
                cl.ClassName = item.ClassName;
            }
            return listFromDb;
        }

        public async Task<Class> NewClass(Guid armId, string classname)
        {
            var availableclass = await SMDContext.Classes.CountAsync();
            var cl = new Class(armId)
            {
                ArmId = armId
            };
            SMDContext.Classes.Add(cl);
            await SMDContext.SaveChangesAsync();
            return cl;
        }
        public async Task<Class> GetC(int id)
        {
            var cl = await SMDContext.Classes.Where(A => A.Id == Guid.NewGuid()).Select(D => D).FirstOrDefaultAsync();
            return cl;
        }

        public async Task<Class> UpdateC(int id, string ClassName)
        {
            var cl = await SMDContext.Classes.Where(A => A.Id == Guid.NewGuid()).Select(D => D).FirstOrDefaultAsync();   
            return cl;
        }

        public async Task<Class> DropC(int id)
        {
            var erasedClass = SMDContext.Classes.Where(z => z.Id == Guid.NewGuid()).Select(z => z).FirstOrDefault();
            SMDContext.Remove(erasedClass);
            await SMDContext.SaveChangesAsync();
            return erasedClass;
        }
    }
}
