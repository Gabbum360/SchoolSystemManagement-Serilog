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
        Task<AddStudent> Regr(string firstName, string surName, int age, string sex, string country, Guid classarmId);
        Task<GetStudents> GetS(string id);
        Task<GetStudents> UpdateS(string id, string firstName, string surName, int age, string country);
        Task<List<GetStudents>> GetStudents();
       /* Task<List<Student>> ConsumeApi();*/ //this is meant to consume webApi from another webapi but....
        Task<Student> DeleteS(string id);
        //Task<Student> Regr_OOP(Student std);
    }
}
