using BusinessLogicLayer;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    /*public interface IStudent // this interface is been registered on the Startup.cs 
    {
        *//*Student Regr();*//*
        Task<AddStudent> Regr(string firstName, string surName, int age, string sex, string country, Guid classarmId);
        Task<GetStudents> GetS(string id);
        Task<GetStudents> UpdateS(string id, string firstName, string surName, int age, string country);
        Task<List<GetStudents>> GetStudents();
       *//* Task<List<Student>> ConsumeApi();*//* //this is meant to consume webApi from another webapi but....
        Task<bool> DeleteS(string id);
        //Task<Student> Regr_OOP(Student std);
    }*/
    public interface IDelete
    {
        Task<bool> DeleteS(string id);
    }
    public interface IRegister
    {
        Task<AddStudent> Regr(string firstName, string surName, int age, string sex, string country, Guid classarmId);
    }
    public interface IUpdate
    {
        Task<GetStudents> UpdateS(string id, string firstName, string surName, int age, string country);
    }
    public interface IGetStudent
    {
        Task<GetStudents> GetS(string id);
    }
    public interface IGetStudents
    {
        Task<List<GetStudents>> GetStudents();
    }
}
