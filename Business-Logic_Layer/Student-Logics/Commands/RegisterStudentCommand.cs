using Business_Logic_Layer.Interfaces;
using BusinessLogicLayer;
using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Student_Logics.Commands
{
    public class RegisterStudentCommand: IRegister
    {
        private readonly ILogger<RegisterStudentCommand> _logger;
        private readonly SchoolSystemDbContext _SMDbContext;
        public RegisterStudentCommand(SchoolSystemDbContext SMDb, ILogger<RegisterStudentCommand> logger)
        {
            _SMDbContext = SMDb;
            _logger = logger;
        }

        public async Task<AddStudent> Regr(string firstName, string surName, int age, string sex, string country, Guid classarmId)
        {
            _logger.LogInformation("entered the Regr method");
            try
            {
                var totalCountsOfStudents = await _SMDbContext.Students.CountAsync();//tjis line gets the total count of available students...
                var student = new Student(firstName, surName, age, sex, country, totalCountsOfStudents)
                {
                    ClassArmId = classarmId //theres is just this line here bcos other fields are set to private and so can not be used from this Logic class.
                };
                //var std = Student.StudentFactory.Create(firstName, surName, age, sex, country);
                _SMDbContext.Add(student);
                await _SMDbContext.SaveChangesAsync();
                var stdnt = new AddStudent();  //create an instance of the view-model to hold the results to be displayed.
                stdnt.SurName = student.SurName;
                stdnt.Sex = student.Sex;
                stdnt.Country = student.Country;
                return stdnt;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "an error occured");
                throw;
            }
            //return student;
        }
    }
}
