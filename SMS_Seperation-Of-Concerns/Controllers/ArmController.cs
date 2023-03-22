using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [Route("api/controllers")]
    [ApiController]
    public class ArmsController : ControllerBase
    {
        private SchoolSystemDbContext SMDContext;
        public ArmsController(SchoolSystemDbContext SMDb)
        {
            SMDContext = SMDb;
        }
        [HttpGet("get-arms")]
        public async Task<IActionResult> GetAllArms()
        {
            var SchoolArms = await SMDContext.Arms.ToListAsync();
            return Ok(SchoolArms);
        }

        [HttpPost("post-new-arm")]
        public async Task<IActionResult> LabelNewClass([FromBody] Arm _arm)
        {
            var newArm = new Arm()
            {
                Name = _arm.Name
            };
            SMDContext.Add(newArm);
            await SMDContext.SaveChangesAsync();
            return Ok(newArm);
        }

        [HttpGet("get-arm/{id}")]
        public async Task<IActionResult> GetSingleArm(int id)
        {
            var singleArm = await SMDContext.Arms.Where(Q => Q.Id == Guid.NewGuid()).Select(Arm => Arm).FirstOrDefaultAsync();
            return Ok(singleArm);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Arm arm)
        {
            var edited = SMDContext.Arms.Where(v => v.Id == Guid.NewGuid()).Select(v => v).FirstOrDefault();
            edited.Name = arm.Name;
            SMDContext.Add(edited);
            await SMDContext.SaveChangesAsync();
            return Ok(edited);
        }

        [HttpDelete("delete-arms/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var armDeleted = SMDContext.Arms.Where(d => d.Id == Guid.NewGuid()).Select(d => d).FirstOrDefault();
            SMDContext.Remove(armDeleted);
            await SMDContext.SaveChangesAsync();
            return Ok(Delete(id));
        }
    }
}
