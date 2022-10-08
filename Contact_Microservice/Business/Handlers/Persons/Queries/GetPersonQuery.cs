using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Persons.Queries
{
    public class GetPersonQuery:IRequest<ApiDataResult<Person>>
    {
        public Guid PersonId { get; set; }

        public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, ApiDataResult<Person>>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IMediator _mediator;

            public GetPersonQueryHandler(IPersonRepository personRepository, IMediator mediator)
            {
                _personRepository = personRepository;
                _mediator = mediator;
            }

            public async Task<ApiDataResult<Person>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var person = await _personRepository.GetAsync(x => x.Id == request.PersonId);
                    return new ApiDataResult<Person>("success",person,true);
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<Person>(false,"Error",ex.Message);
                }
            }
        }
    }
}
