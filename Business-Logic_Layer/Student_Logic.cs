using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Business_Logic_Layer.Interfaces;
using Infrastructure;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer
{
    public class Students_Logic : IStudent //D.I, injecting the IStudent interface to this class.
    {
        private SchoolSystemDbContext SMDContext;
        private ILogger<Students_Logic> _logger;
        public Students_Logic(SchoolSystemDbContext SMDb, ILogger<Students_Logic> logger)
        {
            SMDContext = SMDb;
            _logger = logger;

        }

        public Students_Logic()
        {
        }

        /*string baseUri = "https://localhost:44353/";
        
        public async Task<List<Student>> ConsumeApi()
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

        public async Task<List<Core.Models.Student>> GetStudents()
        {
            List<GetStudents> students = new List<GetStudents>();//created a list of "students" from the "GetStudents" class.
                                                                 //which will help pull all Studentss when accessing this endpoint irrespective of the validations on the model.
            var studentsFromDb = await SMDContext.Students.ToListAsync();
            foreach (var item in studentsFromDb)
            {
                GetStudents std = new GetStudents();
                std.FirstName = item.FirstName;
                std.Age = item.Age;
                std.Country = item.Country;
                students.Add(std);
            }
            return studentsFromDb;
        }

        public async Task<Student> GetStudents(int id)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id.ToString()).Select(s => s).FirstOrDefaultAsync();
            return pupil;
        }

        /*public async Task<Student> Regr(string firstname, string surname, int age, string sex, Guid classArmId, string country)
        {
            var totalCountsOfStudents = await SMDContext.Students.CountAsync();
            var student = new Student(firstname, surname, age, sex, country, totalCountsOfStudents)
            {

                ClassArmId = classArmId
                
            };
            SMDContext.Add(student);
            await SMDContext.SaveChangesAsync();

            return student;
        }*/
        /*public async Task<Student> Regr_OOP(Student std)
        {
            try
            {
                var newStudent = Core.Models.Student.StudentFactory.Create(std.SurName, std.FirstName, std.Age, std.Sex, std.Country).GenerateStudentNo(
                std.StudentNo).SetClassArmId(std.ClassArmId);
                SMDContext.Add(newStudent);
                await SMDContext.SaveChangesAsync();               
            }
            catch (Exception)
            {
                Console.WriteLine("error occured");
                
            }
            return std;
        }*/
        
        public async Task<Student> Regr(string firstName, string surName, int age, string sex, string country, Guid classarmId)
        {
            _logger.LogInformation("entered the Regr method");
            try
            {
                var totalCountsOfStudents = await SMDContext.Students.CountAsync();//tjis line gets the total count of available students...
                if(totalCountsOfStudents <= 0)
                {
                    _logger.LogInformation("theres no student in the database for now");
                }
                else
                {
                    for (int i = 0; i < 0; i++)
                    {
                        _logger.LogInformation("student being registered");
                    }
                }
                var student = new Student(firstName, surName, age, sex, country, totalCountsOfStudents)
                {
                    ClassArmId = classarmId //theres is just this line here bcos other fields are set to private and so can not be used from this Logic class.
                };

                //var std = Student.StudentFactory.Create(firstName, surName, age, sex, country);
                SMDContext.Add(student);
                await SMDContext.SaveChangesAsync();
                return student;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "an error occured");
                throw;
            }
            //return student;
        }
        public async Task<Student> GetS(string id)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            return pupil;
        }

        public async Task<Student> UpdateS(string id, string firstName, string surName, int age, string country)
        {

            try
            {
                var stdnt = SMDContext.Students.Where(v => v.Id == id).Select(student => student).FirstOrDefault();
                // var student = new Student(firstName, surName, age, sex, country, studentNo);
                stdnt.SetFirstName(firstName);
                SMDContext.Update(stdnt);
                await SMDContext.SaveChangesAsync();
                return stdnt;
            }
            catch (Exception e)
            {
                _logger.LogTrace("error om the logic. check and refresh page");
                throw;
            }
        }

        public async Task<Student> DeleteS(string id)
        {
            var pupil = SMDContext.Students.Where(D => D.Id == id).Select(Student => Student).FirstOrDefault();
            SMDContext.Remove(pupil);
            await SMDContext.SaveChangesAsync();
            return pupil;
        }
    }
}
