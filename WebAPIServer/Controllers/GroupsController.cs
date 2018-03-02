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
    [Route("api/Groups")]
    public class GroupsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Groups
        [HttpGet]
        public IEnumerable<Group> GetGroups()
        {
            return _unitOfWork.GroupRepository.GetAll();

        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public ActionResult GetGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = _unitOfWork.GroupRepository.Get(id);              
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public ActionResult PutGroup([FromRoute] int id, [FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.GroupId)
            {
                return BadRequest();
            }

            _unitOfWork.GroupRepository.Update(group);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [HttpPost]
        public ActionResult PostGroup([FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.GroupRepository.Create(group);
            _unitOfWork.Save();

            return CreatedAtAction("GetGroup", new { id = group.GroupId }, group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = _unitOfWork.GroupRepository.Get(id);
            if (group == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.GroupRepository.Delete(group);
                _unitOfWork.Save();

                return Ok(group);
            }
            catch(DbUpdateException)
            {
                return BadRequest("Нельзя удалить группу, за которой закреплены студенты");
            }
        }

        private bool GroupExists(int id)
        {
            return _unitOfWork.GroupRepository.Get(id) != null;
        }
    }
}