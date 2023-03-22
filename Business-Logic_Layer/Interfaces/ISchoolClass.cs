using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISchoolClass
    {
        Task<List<Class>> GetClasses();
        Task<Class> GetC(int id);
        Task<Class> UpdateC(int id, string ClassName);
        Task<Class> DropC(int id);
        Task<Class> NewClass(Guid armId, string classname);
    }
}
