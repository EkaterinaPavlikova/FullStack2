using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIServer.Models;

namespace WebAPIServer.Controllers
{
    [Produces("application/json")]
    [Route("api/StudentSubject")]
    public class StudentSubjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentSubjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/StudentSubject
        [HttpGet]
        public IEnumerable<StudentSubject> GetStudentSubject()
        {
            return _unitOfWork.StudentSubjectRepository.GetAll();
        }

        // GET: api/StudentSubject/5
        [HttpGet("{id}")]
        public ActionResult GetStudentSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentSubject = _unitOfWork.StudentSubjectRepository.Get(id);

            if (studentSubject == null)
            {
                return NotFound();
            }

            return Ok(studentSubject);
        }

        // PUT: api/StudentSubject/5
        [HttpPut("{id}")]
        public ActionResult PutStudentSubject([FromRoute] int id, [FromBody] StudentSubject studentSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentSubject.StudentId)
            {
                return BadRequest();
            }

            _unitOfWork.StudentSubjectRepository.Update(studentSubject);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentSubjectExists(id))
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

        // POST: api/StudentSubject
        [HttpPost]
        public ActionResult PostStudentSubject([FromBody] StudentSubject studentSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.StudentSubjectRepository.Create(studentSubject);
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateException)
            {
                if (StudentSubjectExists(studentSubject.StudentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentSubject", new { id = studentSubject.StudentId }, studentSubject);
        }

        // DELETE: api/StudentSubject/5
        [HttpDelete("{id}")]
        public ActionResult DeleteStudentSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentSubject = _unitOfWork.StudentSubjectRepository.Get(id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            _unitOfWork.StudentSubjectRepository.Delete(studentSubject);
            _unitOfWork.StudentSubjectRepository.Delete(studentSubject);

            return Ok(studentSubject);
        }

        private bool StudentSubjectExists(int id)
        {
            return _unitOfWork.StudentSubjectRepository.Get(id) != null;
        }
    }
}