using System;
using System.Threading.Tasks;
using Exercise.Domain;
using Exercise.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebStudents.Models;
using static WebStudents.Models.StudentModel;

namespace WebStudents.Controllers.Api
{
    // TODO: Refactor controller logic into services
    // TODO: Create a student view model that represents the data with restrictions on the size of the data, the fact that all the data will be required, range validation etc
    // TODO: Create a test suite that represents all the HTTP Data returned to test the way this implementation works once I have the patterns and oractises 
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
        public async Task<ActionResult<PagedResult<Student, string>>> GetAsync()
        {
            var result = await _studentRepository.ListAsync().ConfigureAwait(false);
            return Ok(result);
        }

        // GET: api/Students/5cda87b52e506b05c06e92e1
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            if (id == null)
            {
                BadRequest("No id assigned");
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
                return BadRequest("Student data is required");
            }

            if (ModelState.IsValid)
            {
                var result = _studentRepository.Add(MapFrom(student));
                return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Students/5cda87b52e506b05c06e92e1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] StudentModel student)
        {            
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new ArgumentException("Value cannot be null or empty.", nameof(id)));
            }

            if (student == null)
            {
                return BadRequest(new ArgumentNullException(nameof(student)));
            }

            if (ModelState.IsValid)
            {

                var result = await _studentRepository.GetByAsync(id);
                if (result == null)
                {
                    return NotFound($"'{id}' does not exist");
                }

                student.Id = result.Id;
                return Ok(await _studentRepository.UpdateAsync(MapFrom(student)));
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/ApiWithActions/5cda87b52e506b05c06e92e1
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
