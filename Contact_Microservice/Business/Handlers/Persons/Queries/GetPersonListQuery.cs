using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Persons.Queries
{
    public class GetPersonListQuery: IRequest<ApiDataResult<IEnumerable<Person>>>
    {

        public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, ApiDataResult<IEnumerable<Person>>>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IMediator _mediator;
            public GetPersonListQueryHandler(IPersonRepository personRepository, IMediator mediator)
            {
                _personRepository = personRepository;
                _mediator = mediator;
            }
            public async Task<ApiDataResult<IEnumerable<Person>>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var _personList = await _personRepository.GetListAsync();

                    return new ApiDataResult<IEnumerable<Person>>("Success",_personList,true);
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<IEnumerable<Person>>(false, ex.Message);
                }
            }
        }
    }
}
