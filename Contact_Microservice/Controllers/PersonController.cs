using Contact_Microservice.Business.Handlers.Persons.Commands;
using Contact_Microservice.Business.Handlers.Persons.Queries;
using Contact_Microservice.Entities;
using CoreLibrary.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ApiControllerBase
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<IEnumerable<Person>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getallperson")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetPersonListQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getperson")]
        public async Task<IActionResult> Get(Guid personId)
        {
            var result = await Mediator.Send(new GetPersonQuery() {PersonId = personId });
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addperson")]
        public async Task<IActionResult> AddPerson([FromBody] CreatePersonCommand p)
        {
            var result = await Mediator.Send(p);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("deleteperson")]
        public async Task<IActionResult> DeletePerson([FromBody] DeletePersonCommand pd)
        {
            var result = await Mediator.Send(pd);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
