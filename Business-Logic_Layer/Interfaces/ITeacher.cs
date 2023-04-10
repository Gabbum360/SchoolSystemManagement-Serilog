using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface ITeacher
    {
        // Teacher Regr();
        Task<Teacher> RegT(string Name, int Age, string Sex, string Email, string Country, string staffNo);
        Task<GetTeacher> GetT(int id);
        Task<Teacher> UpdateT(int id, UpdateTeacher teacher);
        Task<UpdateTeacher> UpdateT(Guid id, string Name);
        Task<List<Teacher>> GetTeachers();
        Task<bool> DeleteT(Guid id, string name);
    }
}
