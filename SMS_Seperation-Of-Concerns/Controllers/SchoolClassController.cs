﻿using Business_Logic_Layer.Interfaces;
using Core.Models;
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
    public class SchoolClassController : ControllerBase
    {
        private ISchoolClass _schoolClass;
        public SchoolClassController(ISchoolClass schoolClasses)
        {
            _schoolClass = schoolClasses;
        }
        [HttpGet("get-all-classes")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _schoolClass.GetClasses();
            return Ok(classes);
        }

        /*[HttpPost("Label-a-class")]
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
        }*/
        /*[HttpPost("Add-new-classRoom-encrpt")]
        public async Task<IActionResult> AddClassProfile([FromBody] AddClass cl)
        {
            var classRoom = new Class()
            {
                ClassName = cl.ClassName
            };
            SMDContext.Add(classRoom);
            await SMDContext.SaveChangesAsync();
            return Ok(classRoom);
        }*/

        [HttpPatch("update-class-record-encrpt/{id}")]
        public async Task<IActionResult> EditClassDetails(int id, [FromBody] UpdateClass cl)
        {
            var classToEdit = await _schoolClass.UpdateC(id, cl.ClassName);
            return Ok(classToEdit);
        }

        /*[HttpPatch("update-class-record-/{id}")]
        public async Task<IActionResult> EditClassDetails(int id, Class cl)
        {
            var editedClassroom = SMDContext.Classes.Where(e => e.Id == Guid.NewGuid()).Select(Class => Class).FirstOrDefault();
            editedClassroom.ClassName = cl.ClassName;
            editedClassroom.ArmId = cl.ArmId;
            await SMDContext.SaveChangesAsync();
            return Ok(Ok(editedClassroom));

        }*/
        [HttpPost("add-new-class")]
        public async Task<IActionResult> RegrClass([FromBody] AddClass model)
        {
            var cl = await _schoolClass.NewClass(model.ArmId, model.ClassName);
            return Ok(cl);
        }

        [HttpDelete("remove-Class/{id}")]
        public async Task<IActionResult> DropClass(int id)
        {
            var erasedClass = await _schoolClass.DropC(id);
            return Ok(erasedClass);
        }
    }
}
