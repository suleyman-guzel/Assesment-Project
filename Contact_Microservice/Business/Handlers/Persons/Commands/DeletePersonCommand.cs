using Contact_Microservice.DataAccess.Abstract;
using CoreLibrary.Utilities;
using MediatR;

namespace Contact_Microservice.Business.Handlers.Persons.Commands
{
    public class DeletePersonCommand:IRequest<ApiDataResult<bool>>
    {
        public Guid UID { get; set; }
        public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, ApiDataResult<bool>>
        {
            private readonly IPersonRepository _personRepository;
            private IMediator _mediator;
            public DeletePersonCommandHandler(IPersonRepository personRepository,IMediator mediator)
            {
                _mediator = mediator;   
                _personRepository = personRepository;
            }
            public async Task<ApiDataResult<bool>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var persontodelete = _personRepository.Get(p => p.Id == request.UID);
                    _personRepository.Delete(persontodelete);
                    await _personRepository.SaveChangesAsync();
                    return new ApiDataResult<bool>(true, "success");
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<bool>(false, "Error", ex.Message);                
                }
                
            }
        }
    }
}
