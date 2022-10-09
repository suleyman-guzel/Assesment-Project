using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using CoreLibrary.Entities;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Reports.Queries
{
    public class ReportOfPeopleByLocationQuery:IRequest<ApiDataResult<IEnumerable<ReportOfLocation>>>
    {        
        public class ReportOfPeopleByLocationQueryHandler : IRequestHandler<ReportOfPeopleByLocationQuery, ApiDataResult<IEnumerable<ReportOfLocation>>>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IMediator _mediator;

            public ReportOfPeopleByLocationQueryHandler(IPersonRepository personRepository, IMediator mediator)
            {
                _personRepository = personRepository;
                _mediator = mediator;
            }

            public async Task<ApiDataResult<IEnumerable<ReportOfLocation>>> Handle(ReportOfPeopleByLocationQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var personList = await _personRepository.GetListAsync();
                    var contactList = personList.SelectMany(x => x.Contacts);
                    var location = contactList.Where(x => x.ContactType == (int)ContactType.Location).Select(x => x.Contents).Distinct().ToList();
                    ReportOfLocation reportoflocation;
                    List<ReportOfLocation> reportOfLocationList = new List<ReportOfLocation>();
                    foreach (var item in location)
                    {
                        reportoflocation = new ReportOfLocation();
                        reportoflocation.Location = item;
                        reportoflocation.PersonCount = personList.Count(x => x.Contacts.Any(y => y.ContactType == (int)ContactType.Location && y.Contents == item));
                        reportoflocation.PhoneCount = personList.Count(x => x.Contacts.Any(y => y.ContactType == (int)ContactType.Location && y.Contents == item)
                        && x.Contacts.Any(y =>y.ContactType == (int)ContactType.Phone));
                        reportOfLocationList.Add(reportoflocation);
                    }

                    return new ApiDataResult<IEnumerable<ReportOfLocation>>("Success", reportOfLocationList, true);
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<IEnumerable<ReportOfLocation>>(false,"Error",ex.Message);
                }
            }
        }

    }
}
