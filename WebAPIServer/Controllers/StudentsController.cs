using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIServer.Models;
using System.Web.Http.Cors;

namespace WebAPIServer.Controllers
{

    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Students
        [Route("GetStudents")]
        [HttpGet]
        public List<StudentViewModel> GetStudents()
        {
            var a = _unitOfWork.StudentRepository.GetAll().Select(st => new StudentViewModel(st)).ToList();
            return a;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _unitOfWork.StudentRepository.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [Route("Filter")]
        [HttpGet]
        public IEnumerable<StudentViewModel> GetFilteredStudents(string name = "", string dateOfBirth = "", int? group = null)

        {
            IEnumerable<Student> students =
                 _unitOfWork.StudentRepository.GetAll();

            if (name != String.Empty && name != null)
            {
                students = students.Where(s => s.FullName.ToLower().Contains(name.ToLower()));
            }

            if (dateOfBirth != String.Empty && dateOfBirth != null)
            {
                try
                {
                    DateTime dateTime = Convert.ToDateTime(dateOfBirth);
                    students = students.Where(s => s.DateOfBirth >= dateTime);
                }
                catch (FormatException) { }
            }

            if (group != null)
            {
                students = students.Where(s => s.Group.GroupNumber.Equals(group));
            }

            return students.Select(st => new StudentViewModel(st));
        }




        // PUT: api/Students/5
        [HttpPut("{id}")]
        public ActionResult PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            _unitOfWork.StudentRepository.Update(student);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.StudentRepository.Create(student);
            _unitOfWork.Save();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _unitOfWork.StudentRepository.Get(id);
            if (student == null)
            {
                return NotFound();
            }

            _unitOfWork.StudentRepository.Delete(student);
            _unitOfWork.Save();

            return Ok(student);
        }

        private bool StudentExists(int id)
        {
            return _unitOfWork.StudentRepository.Get(id) != null;
        }
    }


}