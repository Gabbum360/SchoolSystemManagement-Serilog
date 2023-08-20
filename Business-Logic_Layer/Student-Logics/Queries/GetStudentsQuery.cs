using Business_Logic_Layer.Interfaces;
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
    public class GetStudentsQuery: IGetStudents
    {
        private readonly ILogger _logger;
        private readonly SchoolSystemDbContext _SMDbContext;

        public GetStudentsQuery(ILogger<GetStudentsQuery> logger, SchoolSystemDbContext SMDb)
        {
            _logger = logger;
            _SMDbContext = SMDb;
        }
    
        public async Task<List<GetStudents>> GetStudents()
        {
            try
            {
                List<GetStudents> students = new List<GetStudents>();//created a list of "students" from the "GetStudents" Dto.
                                                                     //which will help pull all Studentss when accessing this endpoint irrespective of the validations on the model.
                var studentsFromDb = await _SMDbContext.Students.ToListAsync();
                foreach (var item in studentsFromDb)
                {
                    GetStudents std = new GetStudents();
                    std.FirstName = item.FirstName;
                    std.Age = item.Age;
                    std.Country = item.Country;
                    students.Add(std);
                }
                return students;
            }
            catch (Exception)
            {
                _logger.LogError("error fetching list");
                throw;
            }
        }
    }
}
