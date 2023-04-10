using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Student
    {
        private Student()
        {

        }
        public Student(string firstname, string surname, int age, string sex, string country, int studentCount)
        {
            //validation on OOP...
            if (string.IsNullOrEmpty(firstname))  
                throw new ArgumentNullException("firstName can not be null");
            if (firstname.Length < 5)
                throw new ArgumentException("first name must be greater than 5");
            FirstName = firstname;

            //FirstName = AddFirstname(firstname); this method is being called if firstname is set to have its own method.

            if (string.IsNullOrEmpty(surname))
                throw new ArgumentNullException("");
            if (surname.Length < 7)
                throw new ArgumentException("must be greater than 7");
            SurName = surname;

            if (age <= 5)
                throw new ArgumentNullException("age must be 6years and above");
            Age = age;

            if (string.IsNullOrEmpty(sex))
                throw new ArgumentNullException("Sex can not be empty");
            Sex = sex;

            if (country == null)
                throw new ArgumentNullException("can not be null");
            Country = country;

            StudentNo = GenerateStudentNo(studentCount);

            
        }
        public string Id { get; set; }
        public string SurName { get; private set; }
        public string FirstName { get; private set; }
        public int Age { get; private set; }
        public string Sex { get; private set; }
        public Guid ClassArmId { get; set; }
        public string Country { get; private set; }
        public string StudentNo { get; private set; }


        public Student SetFirstName(string firstname)
        {
            this.FirstName = firstname;
            return this;
        }
        public static class StudentFactory
        {
            public static Student Create(string firstname, string surname, int age, string sex, string country, int studentCount)
            {
                return new Student(firstname,surname,age, sex, country, studentCount);
            }
        }
        private string GenerateStudentNo(int studentCount)
        {
            
            var studentNo = $"fgc{studentCount +1}";  
            return studentNo;
        }

        /*private string AddFirstname(string firstname)
        {
            if(string.IsNullOrEmpty(firstname))
                FirstName = firstname;
            return firstname;
        }*/
        /*public Student SetClassArmId(string classArmId)
        {
            if (classArmId == default) return this;
            ClassArmId = classArmId;    
            return this;
        }*/
    }
}
