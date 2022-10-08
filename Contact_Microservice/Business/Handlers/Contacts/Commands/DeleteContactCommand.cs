using Contact_Microservice.DataAccess.Abstract;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Contacts.Commands
{
    public class DeleteContactCommand : IRequest<ApiDataResult<bool>>
    {
        public Guid ContactId { get; set; }

        public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ApiDataResult<bool>>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMediator _mediator;
            public DeleteContactCommandHandler(IContactRepository contactRepository, IMediator mediator)
            {
                _contactRepository = contactRepository;
                _mediator = mediator;
            }

            public async Task<ApiDataResult<bool>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var contact = await _contactRepository.GetAsync(x => x.Id == request.ContactId);
                    _contactRepository.Delete(contact);
                    await _contactRepository.SaveChangesAsync();
                    return new ApiDataResult<bool>(true, "Contact Removed");
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<bool>(false,"Error",ex.Message);                    
                }
            }
        }
    }
}
