using Exercise.Domain;
using Exercise.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebStudents.Models;
using static WebStudents.Models.StudentModel;

namespace WebStudents.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentsController(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // TODO: Extend paging model binding options
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<PagedResult<Student>>> List()
        {
            var result = await _studentRepository.ListAsync().ConfigureAwait(false);
            return Ok(result);
        }

        // HEAD: api/Students/5cda87b52e506b05c06e92e1
        [HttpHead("{id}")]
        public async Task<ActionResult<bool>> Exist(string id)
        {
            if (id == null)
            {
                return BadRequest(new ArgumentNullException(nameof(id)));
            }

            var result = await _studentRepository.ExistsAsync(id);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
        // GET: api/Students/5cda87b52e506b05c06e92e1
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            if (id == null)
            {
                return BadRequest(new ArgumentNullException(nameof(id)));
            }
            var result = await _studentRepository.GetByAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult<Student> Post([FromBody] StudentModel student)
        {
            if (student == null)
            {
                return BadRequest(new ArgumentNullException(nameof(student)));
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _studentRepository.Add(MapFrom(student));
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT: api/Students/5cda87b52e506b05c06e92e1
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(string id, [FromBody] StudentModel student)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new ArgumentException("Value cannot be null or empty.", nameof(id)));
            }

            if (student == null)
            {
                return BadRequest(new ArgumentNullException(nameof(student)));
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _studentRepository.GetByAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound($"'{id}' does not exist");
            }

            student.Id = result.Id;
            var response = await _studentRepository.UpdateAsync(MapFrom(student)).ConfigureAwait(false);
            return Ok(response);
        }

        // DELETE: api/Students/5cda87b52e506b05c06e92e1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new ArgumentException("Value cannot be null or empty.", nameof(id)));
            }

            var result = await _studentRepository.GetByAsync(id);
            if (result == null)
            {
                return NotFound($"'{id}' does not exist");
            }

            await _studentRepository.DeleteAsync(result);
            return NoContent();
        }
    }
}
