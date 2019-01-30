using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiCoreTrainings.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("addPerson")]
        [Authorize(Policy = "AtLeast18")]
        public async Task<ActionResult> Post([FromBody]Person person)
        {
            var isSuccess = await _personService.TryAddPersonAsync(person);

            if (isSuccess)
            {
                return Ok();
            }

            return BadRequest("Person with the same Id already exists");
        }

        [HttpPut("updatePerson")]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<ActionResult> Put(Person person)
        {
            var isSuccess = await _personService.TryUpdatePersonAsync(person);

            if (isSuccess)
            {
                return Ok();
            }

            return BadRequest("Update person failed");
        }

        [HttpDelete("deletePerson/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isSuccess = await _personService.TryRemovePersonAsync(id);

            if (isSuccess)
            {
                return Ok();
            }

            return BadRequest("Delete person failed");
        }

        [HttpGet("getPeople")]                  
        public async Task<ActionResult> GetAll()
        {
            var people = await Task.Run(() => _personService.GetPeople());
            return Ok(people);
        }

        [HttpGet("getPerson/{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var person = await Task.Run(() => _personService.GetPerson(id));
            return Ok(person);
        }

        // TODO check what's going on with argument
        [HttpGet("getPerson2/{id?}")]
        public async Task<ActionResult> GetAnyPersonIfNotSpecify(int id = 0)
        {
            var person = await Task.Run(() => _personService.GetPerson(id));
            return Ok(person);
        }
    }
}