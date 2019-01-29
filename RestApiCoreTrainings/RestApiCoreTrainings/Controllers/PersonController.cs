using System;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace RestApiCoreTrainings.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("addPerson")]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<ActionResult> Post([FromBody]Person person)
        {
            await Task.Run(() => { _personService.TryAddPersonAsync(person); });
            return Ok();
        }

        [HttpPut("updatePerson")]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<ActionResult> Put(Person person)
        {
            await Task.Run(() => { _personService.TryUpdatePersonAsync(person); });
            return Ok();
        }

        [HttpDelete("deletePerson/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Run(() => { _personService.TryRemovePersonAsync(id); });
            return Ok();
        }

        [HttpGet("getPeople/{showDeleted?}")]                  
        public async Task<ActionResult> GetAll(bool showDeleted = false)
        {
            var people = await Task.Run(() => _personService.GetPeople());
            return Ok(people);
        }

        [HttpGet("getPerson/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NullReferenceException();
            var person = await Task.Run(() => _personService.GetPerson(id));
            return Ok(person);
        }
    }
}