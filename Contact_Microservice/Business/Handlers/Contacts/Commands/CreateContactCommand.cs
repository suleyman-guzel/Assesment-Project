using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Contacts.Commands
{
    public class CreateContactCommand : IRequest<ApiDataResult<bool>>
    {
        public int ContactType { get; set; }
        public string Contents { get; set; }
        public Guid PersonUID { get; set; }


        public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ApiDataResult<bool>>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMediator _mediator;
            public CreateContactCommandHandler(IContactRepository contactRepository, IMediator mediator)
            {
                _mediator = mediator;
                _contactRepository = contactRepository;
            }

            public async Task<ApiDataResult<bool>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Contact _contact = new Contact() { ContactType = request.ContactType, Contents = request.Contents, PersonUID = request.PersonUID, Id = Guid.NewGuid() };
                    _contactRepository.Add(_contact);
                    await _contactRepository.SaveChangesAsync();
                    return new ApiDataResult<bool>(true, "Contact Added");
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<bool>(false, "Error", ex.Message);
                }
            }
        }
    }
}


