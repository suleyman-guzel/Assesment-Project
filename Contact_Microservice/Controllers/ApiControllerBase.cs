using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Microservice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// It is for getting the Mediator instance creation process from the base controller.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


    }
}
