using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiCoreTrainings.Controllers
{
    [Produces("application/json")]
    //[Authorize]
    [Route("api/people")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<IActionResult> Post([FromBody]Person person, CancellationToken cancellationToken)
        {
            var isSuccess = await _personService.TryAddPersonAsync(person, cancellationToken);

            if (isSuccess)
            {
                return NoContent();
                //TODO should be NoContent 
                //TODO or better: CreatedAtAction to GET
                // If Ok is empty then should be NoContent instead
            }

            return BadRequest("Person with the same Id already exists");
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[Authorize(Policy = "AtLeast18")]
        public async Task<ActionResult> Put(Person person, CancellationToken cancellationToken)
        {
            var isSuccess = await _personService.TryUpdatePersonAsync(person, cancellationToken);

            if (isSuccess)
            {
                return NoContent();
            }

            return BadRequest("Update person failed");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var people = await Task.Run(() => _personService.GetPeople(), cancellationToken);

            if (people.IsEmpty)
            {
                return NoContent();
            }

            return Ok(people);
        }

        [HttpGet("paged")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult GetPagedPeople(CancellationToken cancellationToken, int pageIndex = 1)
        {
            var pagedPeople = _personService.GetPagedPeople(pageIndex);

            if (pagedPeople.Count == 0)
            {
                return NoContent();
            }

            return Ok(pagedPeople);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var person = await Task.Run(() => _personService.GetPerson(id), cancellationToken);
            if (person == null)
            {
                return NoContent();
            }

            return Ok(person);
        }

        [HttpGet("GetPerson")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> GetAnyPersonIfNotSpecify(CancellationToken cancellationToken, [FromQuery]int id = 0)
        {
            var person = await Task.Run(() => _personService.GetPerson(id), cancellationToken);

            if (person == null)
            {
                return NoContent();
            }

            return Ok(person);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Patch(CancellationToken cancellationToken, int id, string surname)
        {
            var isSuccess = await _personService.UpdateSurnameAsync(id, surname, cancellationToken);

            if (isSuccess)
            {
                return NoContent();
            }

            return BadRequest("Update surname failed");
        }
    }
}