using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Dynamic;
using System.Net.Mail;

namespace SeperationOfConcerns.Infrastructure.Core
{
    [TestClass]
    public class Student
    {
        /*private SchoolSystemDbContext _dbContext;
        public Student(SchoolSystemDbContext dbContext)
        {
            _dbContext = dbContext; 
        }*/

        [TestMethod]
        public void TestMethod1()
        {
            //var dbCont = new SchoolSystemDbContext;
            //var newIstudent = Test.Create<Istudent>();
            //Assert.IsTrue();
            //private readonly Student student = new Student();   
            Student std = new Student();
            
            //var pupil = await SMDContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
        }
    }
}
