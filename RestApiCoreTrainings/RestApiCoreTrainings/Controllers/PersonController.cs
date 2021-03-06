﻿using System;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Attributes;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _personService.TryAddPersonAsync(person, cancellationToken);
            
            if (!isSuccess)
            {
                // TODO it's only for testing. throwing exception in this situation it's bad pattern
                 throw new HttpCustomException((int)HttpStatusCode.BadRequest);
                // return BadRequest("Person with the same Id already exists");
            }

            return CreatedAtAction("GetPeople", "Person");
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
            
            // TODO best practice - return empty list instead of NoContent
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

            var classBugReport = typeof(Person).GetCustomAttributes<BugReportAttribute>();
            var propertyBugReport = typeof(Person).GetProperty("Age")?.GetCustomAttributes<BugReportAttribute>();
            return Ok(new { person, classBugReport, propertyBugReport });
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