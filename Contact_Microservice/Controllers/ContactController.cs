using Contact_Microservice.Business.Handlers.Contacts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiControllerBase
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addcontact")]
        public async Task<IActionResult> AddContactToPerson([FromBody] CreateContactCommand c)
        {
            var result = await Mediator.Send(c);
            return result.Success ? Ok(result) : BadRequest(result);
        }



        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("deletecontact")]
        public async Task<IActionResult> DeleteContactToPerson([FromBody] DeleteContactCommand cd)
        {
            var result = await Mediator.Send(cd);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
