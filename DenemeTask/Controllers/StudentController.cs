using DataAccessLayer;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DenemeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        public IActionResult StudentList()
        {
            using var c = new Context();
            var values = c.students.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddStudent(Student s)
        {
            using var c = new Context();
            c.Add(s);
            c.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult StudentDelete(int id)
        {
            using var c = new Context();
            var student = c.students.Where(x => x.ID == id).ToList().FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            c.Remove(student);
            c.SaveChanges();
            return Ok();


        }

        [HttpPut]
        public IActionResult StudentUpdate(Student s)
        {
            using var c = new Context();
            var student = c.students.Where(x => x.ID == s.ID).ToList().FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            student.Name = s.Name;
            student.Surname = s.Surname;
            student.Vize = s.Vize;
            student.Final = s.Final;
            c.Update(student);
            c.SaveChanges();
            return Ok();

        }

        [HttpGet("{id}")]
        public IActionResult StudentByİd(int id)
        {
            using var c = new Context();
            var student = c.students.Where(x => x.ID == id).ToList().FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
