using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiCoreTrainings.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/people")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<IActionResult> Post([FromBody]Person person, CancellationToken cancellationToken)
        {
            var isSuccess = await _personService.TryAddPersonAsync(person, cancellationToken);

            if (isSuccess)
            {
                return Ok();
                //TODO should be NoContent 
                //TODO or better: CreatedAtAction to GET
                // If Ok is empty then should be NoContent instead
            }

            return BadRequest("Person with the same Id already exists");
        }

        [HttpPut]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<ActionResult> Put(Person person, CancellationToken cancellationToken)
        {
            var isSuccess = await _personService.TryUpdatePersonAsync(person, cancellationToken);

            if (isSuccess)
            {
                return Ok();
            }

            return BadRequest("Update person failed");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isSuccess = await _personService.TryRemovePersonAsync(id, cancellationToken);

            if (isSuccess)
            {
                return NoContent();
            }

            return BadRequest("Delete person failed");
        }

        [HttpGet]                  
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var people = await Task.Run(() => _personService.GetPeople(), cancellationToken);
            return Ok(people);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var person = await Task.Run(() => _personService.GetPerson(id), cancellationToken);
            return Ok(person);
        }

        [HttpGet("GetPerson")]
        public async Task<ActionResult> GetAnyPersonIfNotSpecify(CancellationToken cancellationToken, [FromQuery]int id = 0)
        {
            var person = await Task.Run(() => _personService.GetPerson(id), cancellationToken);
            return Ok(person);
        }
    }
}