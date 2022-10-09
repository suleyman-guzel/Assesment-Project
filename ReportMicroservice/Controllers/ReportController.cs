using CoreLibrary.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportMicroservice.Business.Handlers.Reports.Queries;
using ReportMicroservice.Entities;

namespace ReportMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ApiControllerBase
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<Report>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getReportofpeoplebylocation")]
        public async Task<IActionResult> GetReportOfPeopleByLocation()
        {
            var result = await Mediator.Send(new ReportOfPeopleByLocationReportQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiDataResult<IEnumerable<Report>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getreports")]
        public async Task<IActionResult> GetReports()
        {
            var result = await Mediator.Send(new GetAllReportsQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
