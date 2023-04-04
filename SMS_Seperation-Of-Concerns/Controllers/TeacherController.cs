using Business_Logic_Layer.Interfaces;
using BusinessLogicLayer;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Controllers
{
    public class TeachersController : ControllerBase
    {
        private ITeacher _teacher;
        public TeachersController(ITeacher teacher)
        {
            _teacher = teacher;
        }

        [HttpGet("get-all-teachers")]
        public async Task<IActionResult> GetallTeachers()
        {
            /*  List<GetTeachers> teachers = new List<GetTeachers>();   //created a list of "teachers" from the "Getteachers" class.
                                                                     //which will help pull all eachers when accessing this endpoint irrespective of the validations on the model.
              var staffFromDb = await SMDContext.Teachers.ToListAsync();
              foreach (var item in staffFromDb)
              {
                  GetTeachers staff = new GetTeachers(); 
                  staff.Id = item.Id;
                  staff.Name = item.Name; 
                  staff.Age = item.Age;   
                  staff.Email = item.Email;
                  staff.Country = item.Country;
                  staff.Sex = item.Sex;
                  staff.StaffNo = item.StaffNo; 
              }*/
            var teachers = await _teacher.GetTeachers();
            return Ok(teachers);
        }

        [HttpPost("add-new-teacher-to-database_validation")]
        public async Task<IActionResult> CreateNewStaff([FromBody] Teacher teacher)
        {
            /*if (ModelState.IsValid)
            {
                var newStaff = new Teacher()
                {
                    Id = teacher.Id,   
                    Name = teacher.Name,
                    Sex = teacher.Sex,
                    Age = teacher.Age,
                    Email = teacher.Email,
                    Country = teacher.Country,
                   // StaffNo = teacher.StaffNo,  
                };
                SMDContext.Add(newStaff);
                await SMDContext.SaveChangesAsync();
            }*/
            try
            {
                var newTeacher = await _teacher.RegT(teacher.Name, teacher.Age, teacher.Sex, teacher.Email, teacher.Country, teacher.StaffNo);
                return Ok(newTeacher);
            }
            catch (Exception)
            {
                Console.WriteLine("check here");
                throw;
            }
        }

        /* [HttpPost("regiser-new-teacher-from-codeBase")]
         public async Task<IActionResult> RegisterNewStaff()
         {
             var newStaff = new Teacher()
             {
                 Id = 0,
                 Name = "",
                 Sex = "",
                 Age = 0,
                 Email = "",
                 Country ="",
                 StaffNo = 0
             };
             SMDContext.Add(newStaff);
             await SMDContext.SaveChangesAsync();
             return Ok(newStaff);
         }*/

        [HttpGet("get-one-teacher/{id}")]
        public async Task<IActionResult> GetOneStaff(int id)
        {
            var staff = await _teacher.GetT(id);
            return Ok(staff);
        }

        [HttpPut("update-record/{id}")]
        public async Task<IActionResult> EditRecord(int id, [FromBody] UpdateTeacher staff) //the right practice without the normal model but viewmodel.
        {
            var sted = await _teacher.UpdateT(id, staff); //staff here will only give you the properties available on the viewModel.
            return Ok(sted);
        }


        [HttpPatch("update-only-Country/{id}")]
        public async Task<IActionResult> Update_Teacher(int id, UpdateTeacher model) //using your model here is not a good practice as it exposes your model to the outside world.
        {
            var staff = await _teacher.UpdateT(id, model);   //teacher here gives access to all properties in the Model which is a wrong practice.
            return Ok(staff);
        }

        [HttpDelete("delete-teacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var staff = await _teacher.DeleteT(id);
            return Ok("Deleted successfully");
        }

    }

}
