using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Teacher
    {
        public Teacher()
        {
                
        }
        public Teacher(int staffCount)
        {
            /*if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("firstName can not be null");
            if (Name.Length < 5)
                throw new ArgumentException("first name must be greater than 5");
            Name = name;

            if (string.IsNullOrEmpty(sex))
                throw new ArgumentNullException("");
            if (Sex.Length < 6)
                throw new ArgumentException("sorry but the school needs only female tutors");
            Se
            if (email == null)
                throw new ArgumentNullException("email must be filled out");
            Email = emaix = sex;

            if (age <= 20)
                throw new ArgumentNullException("person below 20 years is not allowed");
            Age = age;
l;  

            if (country == null)
                throw new ArgumentNullException("can not be null");
            Country = country;*/

            StaffNo = this.GenerateStaffNo(staffCount);

        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string StaffNo { get; private set; }
        public string Country { get; set; }

        public static class TeacherFactory
        {
            public static Teacher Create(int staffCount)
            {
                return new Teacher(staffCount);
            }
        }
        public string GenerateStaffNo(int staffCount)
        {
            var staffNo = $"fgc{(staffCount + 1)}";
            return staffNo;
        }
    }
}
