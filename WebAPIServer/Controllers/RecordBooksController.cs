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
    [Route("api/RecordBooks")]
    public class RecordBooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecordBooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/RecordBooks
        [HttpGet]
        public IEnumerable<RecordBook> GetRecordBooks()
        {
            return _unitOfWork.RecordBookRepository.GetAll();
        }

        // GET: api/RecordBooks/5
        [HttpGet("{id}")]
        public ActionResult GetRecordBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recordBook = _unitOfWork.RecordBookRepository.Get(id);
            if (recordBook == null)
            {
                return NotFound();
            }

            return Ok(recordBook);
        }

        // PUT: api/RecordBooks/5
        [HttpPut("{id}")]
        public ActionResult PutRecordBook([FromRoute] int id, [FromBody] RecordBook recordBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recordBook.RecordBookId)
            {
                return BadRequest();
            }

            _unitOfWork.RecordBookRepository.Update(recordBook);    
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordBookExists(id))
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

        // POST: api/RecordBooks
        [HttpPost]
        public ActionResult PostRecordBook([FromBody] RecordBook recordBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.RecordBookRepository.Create(recordBook);
            _unitOfWork.Save();

            return CreatedAtAction("GetRecordBook", new { id = recordBook.RecordBookId }, recordBook);
        }

        // DELETE: api/RecordBooks/5
        [HttpDelete("{id}")]
        public ActionResult DeleteRecordBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recordBook = _unitOfWork.RecordBookRepository.Get(id); ;
            if (recordBook == null)
            {
                return NotFound();
            }

            _unitOfWork.RecordBookRepository.Delete(recordBook);
            _unitOfWork.Save();
            return Ok(recordBook);
        }

        private bool RecordBookExists(int id)
        {
            return _unitOfWork.RecordBookRepository.Get(id) !=null ;
        }
    }
}