using Business_Logic_Layer.Interfaces;
using BusinessLogicLayer;
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
    public class DeleteStudentByIdCommand: IDelete
    {
        private readonly ILogger<DeleteStudentByIdCommand> _logger;
        private readonly SchoolSystemDbContext _SMDbContext;
        //private IStudent _student;
        public DeleteStudentByIdCommand(ILogger<DeleteStudentByIdCommand> logger, SchoolSystemDbContext SMDb/*, IStudent stdd*/)
        {
            _SMDbContext = SMDb;
            _logger = logger;
            //_student = stdd;
        }
        public async Task<bool> DeleteS(string id)
        {
            try
            {
                var pupil = _SMDbContext.Students.Where(D => D.Id == id).Select(Student => Student).FirstOrDefault();
                if (pupil == null)
                {
                    return false;
                }

                _SMDbContext.Remove(pupil);
                await _SMDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                _logger.LogWarning("hey! this task was unable to be completed, please check and retify the issue first");
                throw;
            }
        }
    }
}
