/*using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [Route("api/controllers")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private SchoolSystemDbContext SMDContext;
        public ClassController(SchoolSystemDbContext SMDb)
        {
            SMDContext = SMDb;
        }
        [HttpGet("get-all-classes")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await SMDContext.Classes.ToListAsync();
            return Ok(classes);
        }
        [HttpGet("get-all-with-encrpt")]
        public async Task<IActionResult> GetAll()
        {
            List<GetClass> classes = new List<GetClass>();
            var classesFromDb = await SMDContext.Classes.ToListAsync();
            foreach (var item in classesFromDb)
            {
                GetClass cl = new GetClass();
                cl.ClassName = item.ClassName;
            }
            return Ok(classes);
        }

        [HttpPost("Label-a-class")]
        public async Task<IActionResult> CreateClassProfile(Class cl)
        {
            var classroom = new Class()
            {
                ClassName = cl.ClassName,
                ArmId = cl.ArmId
            };
            SMDContext.Classes.Add(classroom);
            await SMDContext.SaveChangesAsync();
            return Ok(classroom);
        }
        [HttpPost("Add-new-classRoom-encrpt")]
        public async Task<IActionResult> AddClassProfile([FromBody] AddClass cl)
        {
            var classRoom = new Class()
            {
                ClassName = cl.ClassName
            };
            SMDContext.Add(classRoom);
            await SMDContext.SaveChangesAsync();
            return Ok(classRoom);
        }

        [HttpPatch("update-class-record-encrpt/{id}")]
        public async Task<IActionResult> EditClassDetails(int id, [FromBody] UpdateClass cl)
        {
            var ClassFromDb = SMDContext.Classes.Where(e => e.Id == Guid.NewGuid()).Select(Class => Class).FirstOrDefault();
            ClassFromDb.ClassName = cl.ClassName;
            await SMDContext.SaveChangesAsync();
            return Ok(Ok(ClassFromDb));

        }
        [HttpPatch("update-class-record-/{id}")]
        public async Task<IActionResult> EditClassDetails(int id, Class cl)
        {
            var editedClassroom = SMDContext.Classes.Where(e => e.Id == Guid.NewGuid()).Select(Class => Class).FirstOrDefault();
            editedClassroom.ClassName = cl.ClassName;
            editedClassroom.ArmId = cl.ArmId;
            await SMDContext.SaveChangesAsync();
            return Ok(Ok(editedClassroom));

        }

        [HttpDelete("remove-Class/{id}")]
        public async Task<IActionResult> DropClass(int id)
        {
            var erasedClass = SMDContext.Classes.Where(z => z.Id == Guid.NewGuid()).Select(z => z).FirstOrDefault();
            SMDContext.Remove(erasedClass);
            await SMDContext.SaveChangesAsync();
            return Ok("deleted");
        }
    }
}
*/