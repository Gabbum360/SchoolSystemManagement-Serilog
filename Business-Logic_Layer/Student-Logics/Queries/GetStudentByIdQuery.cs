using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Student_Logics.Commands;
using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Student_Logics.Queries
{
    public class GetStudentByIdQuery: IGetStudent
    {
        private readonly ILogger<GetStudentByIdQuery> _logger;
        private readonly SchoolSystemDbContext _SMDbContext;
        public GetStudentByIdQuery(ILogger<GetStudentByIdQuery> logger, SchoolSystemDbContext SMDb)
        {
            _SMDbContext = SMDb;    
            _logger = logger;   
        }
        public async Task<GetStudents> GetS(string id)
        {
            try
            {
                //the two patterns works but the second esposes.
                /*var pupil = await SMDContext.Students.Where(student => student.Id == id).Select(f => new GetStudents()
                {
                    Id = f.Id,
                    Country = f.Country
                }).FirstOrDefaultAsync();*/
                var pupil = await _SMDbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
                if (string.IsNullOrEmpty(id))
                {
                    return null;
                }
                var stdnt = new GetStudents();
                stdnt.SurName = pupil.SurName;
                stdnt.Sex = pupil.Sex;
                stdnt.Country = pupil.Country;
                return stdnt;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "student id is invalid", id);
                throw;
            }
        }
    }
}
