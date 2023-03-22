using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IStudent // this interface is been registered on the Startup.cs 
    {
        /*Student Regr();*/
        Task<Student> Regr(string firstName, string surName, int age, string sex, string country, Guid classarmId);
        Task<Student> GetS(string id);
        Task<Student> UpdateS(string id, string firstName, string surName, int age, string country);
        Task<List<Student>> GetStudents();
       /* Task<List<Student>> ConsumeApi();*/ //this is meant to consume webApi from another webapi but....
        Task<Student> DeleteS(string id);
        //Task<Student> Regr_OOP(Student std);
    }
}
