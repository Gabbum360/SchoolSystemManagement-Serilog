using Business_Logic_Layer.Interfaces;
using BusinessLogicLayer;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SMS_Seperation_Of_Concerns.NewFolder;
using Infrastructure.Interfaces;

namespace SMS_Seperation_Of_Concerns.Controllers
{
    // [Authorize]
    //[EnableCors("CorsPolicy")]
    [Route("api/controllers")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        //private IStudent _students;    //injected the interface(IStudents and gave it an "object instance".
        private IDelete _delete;
        private IUpdate _update;
        private IRegister _register;
        private IGetStudent _GetStudent;
        private IGetStudents _GetStudents;
        private ILogger<StudentController> _logger;
        private IHttpService _httpService;
        //private IRepository<Student> _repository;
        public StudentController(/*IStudent students, */
            ILogger<StudentController> logger/*, IRepository<Student> repository*/,
            IDelete delete, IGetStudents getStudents,
            IUpdate update, IRegister register, IGetStudent getStudent)
        {
            //_students = students;   //initialised here.
            _logger = logger;
            _delete = delete;
            // _repository = repository;
            _GetStudent = getStudent;   
            _GetStudents = getStudents; 
            _register = register;   
            _update = update;
        }

        string baseUri = "https://localhost:44353/";

        [HttpGet("okay")]
        public async Task<GetStudents> ConsumeApi()
        {
            var readytoCon = await _httpService.GetListOfStudents();
            return readytoCon;
        }
/*
        [HttpGet("okay")]
        public async Task<List<Student>> ConsumeRetrieve()
        {
            List<Student> students = new List<Student>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage res = await client.GetAsync("https://localhost:44353/get-list-of-student");
            if (res.IsSuccessStatusCode)
            {
                var stdRes = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Student>>(stdRes);
            }
            return students;
        }*/
        /// <summary>
        /// this endpoint is for consumeApi. and needs edit bcos of new validation in the action class.
        /// </summary>
        /// <returns></returns>
        [HttpPost("posting")]
        /*public async Task<Student> ConsumeSave(Student student)
        {
            var std = new Student()
            {
                Id = student.Id,
                SurName = student.SurName,
                FirstName = student.FirstName,
                Sex = student.Sex,
                Age = student.Age,
                ClassArmId = student.ClassArmId,
                StudentNo = student.StudentNo,
                Country = student.Country,
            };
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            string json =  JsonConvert.SerializeObject(std);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("https://localhost:44353/post-student", httpContent);
            if (res.IsSuccessStatusCode)
            {
                res.EnsureSuccessStatusCode();
            }
            return std;
        }*/
        

        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var allStdnt = await _GetStudents.GetStudents();
            return Ok(allStdnt);

        }

        /*[HttpGet("get-student-with-full_Details/{id}")]
        public async Task<IActionResult> GetOneStudent(int id)
        {
            StudentController stdnt = new StudentController();
            await stdnt.GetStudents(id);
            return (IActionResult)stdnt;
        }*/

        //working.
        [HttpPost("register-new-studnt")]
        public async Task<IActionResult> RegisterNewStudent([FromBody] AddStudent pupil)
        {
            var stdnt = await _register.Regr(pupil.SurName, pupil.FirstName, pupil.Age, pupil.Sex, pupil.Country, pupil.ClassArmId);
            return Ok(stdnt);
        }

        [HttpPost("register-with-validation")]
        public async Task<IActionResult> newOOP([FromBody] AddStudent std)
        {
            try
            {
                var stdnt = await _register.Regr(std.FirstName, std.SurName, std.Age, std.Sex, std.Country, std.ClassArmId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "new error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel() { ErrorMessage = "error occured" });
            }
        }

        //working.
        [HttpGet("get-student-with-full_Details/{id}")]
        public async Task<IActionResult> GetOneStudent(string id)
        {
            var pupil = await _GetStudent.GetS(id);
            return Ok(pupil);
            //return StatusCode(StatusCodes.Status200OK, new Succ() { ErrorMessage = ""})
        }


        [HttpPatch("update-studentRecord/{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] UpdateStudent student)
        {
            var student1 = await _update.UpdateS(id, student.FirstName, student.SurName, student.Age, student.Country);
            return Ok(student1);
        }

        //working.
        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var pupil = await _delete.DeleteS(id);
            return Ok("deleted successfully!");
        }

        /*[HttpGet("get-pupil-encrpted/{id}")]
        public async Task<IActionResult> GetOnestudentinfo(int id, [FromBody] GetStudentbyId stdnt)
        {
         var pupil = await SMDContext.Students_Logic.Where(s => s.Id == id.ToString()).FirstOrDefaultAsync();
         stdnt.Id = pupil.Id;
         stdnt.SurName = pupil.SurName;
         stdnt.FirstName = pupil.FirstName;
         return (IActionResult)stdnt;
        }*/

        /*public async Task<IActionResult> GetOneStudentL()
        {
            var stud = await SMDContext.Students_Logic.Include(s=>s.Country).FirstOrDefault(c=>c.Id).ToString();
        }*//*

        [HttpPost("register-new-student")]
        public async Task<IActionResult> RegisterNewStudent(BusinessLogicLayer.Students_Logic pupil)
        {
            
                var student = new Models.Student()
                {
                    Id = "103",
                    SurName = "Fadolamu",
                    FirstName = "Oluwafunmilayo",
                    Age = 25,
                    Sex = "Female",
                    ClassArmId = "1555",
                    Country = "Germany",
                    StudentNo = 20004,
                };
                SMDContext.Add(student);
                await SMDContext.SaveChangesAsync();
            
            return (IActionResult)pupil;
        }

        [HttpPost("insert-new-record")]
        public async Task<IActionResult> InsertNewRecord(BusinessLogicLayer.Students_Logic person)
        {
            
                var pupil = new Models.Student()
                {
                    Id = "102",
                    FirstName = "Clement",
                    SurName = "Ochayi",
                    Age = 22,
                    Sex = "Male",
                    Country = "USA",
                    StudentNo = 20002
                };
                SMDContext.Add(pupil);
                await SMDContext.SaveChangesAsync();
          
            return (IActionResult)person; ;
        }

        [HttpPost("register-new-Std")]
        public async Task<IActionResult> RegisterStudent([FromBody] Student student)//this method allows user input from the endpoint when called.
        {
           
                var newStudent = new Models.Student()
                {
                    Id = student.Id,
                    SurName = student.SurName,
                    FirstName = student.FirstName,
                    Age = student.Age,
                    Sex = student.Sex,
                  //  ClassArmId = student.ClassArmId,
                    Country = student.Country,
                    //StudentNo = student.StudentNo,
                };
                SMDContext.Add(newStudent);
                await SMDContext.SaveChangesAsync();

            return (IActionResult)student;
        }

        [HttpPatch("update-studentRecord/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Models.Student studnt)
        {
            var sted = SMDContext.Students_Logic.Where(student => student.Id == id.ToString()).Select(student => student).FirstOrDefault();
            *//*sted.FirstName = studnt.FirstName;
            sted.Age = studnt.Age;
            sted.Sex = studnt.Sex;*//*
            sted.Country = studnt.Country;
            //sted.StudentNo = studnt.StudentNo;
            sted.ClassArmId = studnt.ClassArmId;
            await SMDContext.SaveChangesAsync();
            return (IActionResult)studnt;
        }

        [HttpPatch("change-few-data/{id}")]// this method allows the user to input see and edit only the required. base on the model created.
        public async Task<IActionResult> MakeChanges(int id, [FromBody] UpdateStudent pupil)
        {
            var sted = SMDContext.Students_Logic.Where(student => student.Id == id.ToString()).Select(student => student).FirstOrDefault();
            sted.Country = pupil.Country;
            await SMDContext.SaveChangesAsync();
            return (IActionResult)pupil;
        }

        [HttpDelete("remove-student-from-table")]
        public async Task<IActionResult> DeleteStudent(BusinessLogicLayer.Students_Logic person)
        {
            var student = new Models.Student()
            {
                Id = "103"
            };
            SMDContext.Remove(student);
            await SMDContext.SaveChangesAsync();
            return (IActionResult)person;
        }

        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(int id, [FromBody] Models.Student student)
        {
            var pupil = SMDContext.Students_Logic.Where(p => p.Id == id.ToString()).Select(student => student).FirstOrDefault();
            SMDContext.Remove(pupil);
            await SMDContext.SaveChangesAsync();
            return (IActionResult)student;
        }*/
    }
}
