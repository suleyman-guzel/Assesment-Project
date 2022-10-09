using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Reports.Queries
{
    public class ReportOfPeopleByLocationQuery:IRequest<ApiDataResult<IEnumerable<Person>>>
    {
        public string Location { get; set; }

        public class ReportOfPeopleByLocationQueryHandler : IRequestHandler<ReportOfPeopleByLocationQuery, ApiDataResult<IEnumerable<Person>>>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IMediator _mediator;

            public ReportOfPeopleByLocationQueryHandler(IPersonRepository personRepository, IMediator mediator)
            {
                _personRepository = personRepository;
                _mediator = mediator;
            }

            public async Task<ApiDataResult<IEnumerable<Person>>> Handle(ReportOfPeopleByLocationQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var personList = await _personRepository.GetListAsync(x => x.Contacts.Any(x => x.ContactType == 3 && x.Contents == request.Location));
                    return new ApiDataResult<IEnumerable<Person>>("Success", personList, true);
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<IEnumerable<Person>>(false,"Error",ex.Message);
                }
            }
        }

    }
}
