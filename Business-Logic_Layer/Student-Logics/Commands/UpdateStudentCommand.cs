using Business_Logic_Layer.Interfaces;
using Core.Models;
using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Student_Logics.Commands
{
    public class UpdateStudentCommand: IUpdate
    {
        private readonly ILogger<UpdateStudentCommand> _logger;
        private readonly SchoolSystemDbContext _SMDbContext;
        public UpdateStudentCommand(ILogger<UpdateStudentCommand> logger, SchoolSystemDbContext SMDb)
        {
            _logger = logger;
            _SMDbContext = SMDb; 
        }
        public async Task<GetStudents> UpdateS(string id, string firstName, string surName, int age, string country)
        {

            try
            {
                var stdnt = _SMDbContext.Students.Where(v => v.Id == id).Select(student => student).FirstOrDefault();
                if (stdnt.Id != null)
                {
                    // var student = new Student(firstName, surName, age, sex, country, studentNo);
                    stdnt.SetFirstName(firstName);
                }
                else
                {
                    throw new Exception("invalid student Id");
                }
                _SMDbContext.Update(stdnt);
                await _SMDbContext.SaveChangesAsync();
                var student = new GetStudents();
                student.Id = stdnt.Id;
                return student;
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, "error om the logic. check and refresh page");
                throw;
            }
        }
    }
}
