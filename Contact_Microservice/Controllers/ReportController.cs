using Contact_Microservice.Business.Handlers.Reports.Queries;
using Contact_Microservice.Entities;
using CoreLibrary.Entities;
using CoreLibrary.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ApiControllerBase
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<IEnumerable<ReportOfLocation>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getReportofpeoplebylocation")]
        public async Task<IActionResult> GetReportOfPeopleByLocation()
        {
            var result = await Mediator.Send(new ReportOfPeopleByLocationQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
