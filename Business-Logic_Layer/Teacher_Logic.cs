using Business_Logic_Layer.Interfaces;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogicLayer
{
    public class Teachers_Logic : ITeacher
    {
        private SchoolSystemDbContext SMDContext;
        public Teachers_Logic(SchoolSystemDbContext SMDb)
        {
            SMDContext = SMDb;
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            List<GetTeacher> teacher = new List<GetTeacher>();
            var teacherFromDb = await SMDContext.Teachers.ToListAsync();
            foreach (var item in teacherFromDb)
            {
                GetTeacher tchr = new GetTeacher();
                tchr.Name = item.Name;
                tchr.Age = item.Age;
                tchr.Sex = item.Sex;
                tchr.Email = item.Email;
                //tchr.StaffNo = item.StaffNo;
                tchr.Country = item.Country;

            }
            return teacherFromDb;
        }

        public async Task<Teacher> RegT(string name, int age, string sex, string email, string country, string staffNo)
        {
            try
            {
               var totalCountsAvailable = await SMDContext.Teachers.CountAsync(); //to know if there is/are any staffNo exiting already, if not it will start from a new number..
                var teacher = new Teacher(totalCountsAvailable)
                {
                    //the following lines allows user input bcos its not set to private
                    Name = name,
                    Age = age,  
                    Sex = sex,  
                    Email = email,  
                    Country = country,  
                };
                SMDContext.Add(teacher);
                await SMDContext.SaveChangesAsync();
                return teacher;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<GetTeacher> GetT(int id)
        {
            var staff = await SMDContext.Teachers.Where(T => T.Id == Guid.NewGuid()).FirstOrDefaultAsync();
            var result = new GetTeacher();
            result.Name = staff.Name;
            result.Age = staff.Age;
            result.Sex = staff.Sex; 
            return result;
        }

        public async Task<Teacher> UpdateT(int id, [FromBody] UpdateTeacher teacher)// or use this (int id, string(property_name)).
        {
            var staff = SMDContext.Teachers.Where(z => z.Id == Guid.NewGuid()).Select(T => T).FirstOrDefault();
            staff.Country = teacher.Country;
            staff.Email = teacher.Email;
            await SMDContext.SaveChangesAsync();
            return staff;
        }

        //another method to pass a different signature instead of the previous...
        public async Task<UpdateTeacher> UpdateT(Guid id, string name)
        {
            var staff = SMDContext.Teachers.Where(z => z.Id == id && z.Name == name).Select(T => T).FirstOrDefault();
            if (staff == null)
            {
                return null;
            }
            staff.Name = name;
            await SMDContext.SaveChangesAsync();
            //mapping the data to the updated result...
            var upd = new UpdateTeacher();
            upd.Email = staff.Email;
            upd.Country = staff.Country;    
            return upd;
        }

        public async Task<bool> DeleteT(Guid id, string name)
        {
            var staff = SMDContext.Teachers.Where(S => S.Id == id && S.Name == name).Select(S => S).FirstOrDefault();
            if (staff == null)
            {
                return false;
            }
            await SMDContext.SaveChangesAsync();
            return true;
        }
    }
}
