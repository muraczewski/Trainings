using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace RestApiCoreTrainings.Controllers
{
    [Produces("application/json")]
    [Route("api/people")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody]Person person, CancellationToken cancellationToken)
        {
            var isSuccess = await _personService.TryAddPersonAsync(person, cancellationToken);

            if (isSuccess)
            {
                return NoContent();
                // TODO it could be CreatedAtAction e.g. to GetById or GetAll
            }

            return BadRequest("Person with the same Id already exists");
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
        public async Task<ActionResult> GetPeople(CancellationToken cancellationToken, int pageIndex = 1, int pageSize = 5)
            {
            var pagedResult = await _personService.GetPeopleAsync(pageIndex, pageSize, cancellationToken);

            if (pagedResult.TotalItems == 0)
            {
                return NoContent();
            }

            return Ok(new
            {
                pagedResult.TotalItems,
                pagedResult.TotalPages,
                pagedResult.PageIndex,
                pagedResult.HasPreviousPage,
                pagedResult.HasNextPage,
                pagedResult.Items
            });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var person = await _personService.GetPersonAsync(id, cancellationToken);
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