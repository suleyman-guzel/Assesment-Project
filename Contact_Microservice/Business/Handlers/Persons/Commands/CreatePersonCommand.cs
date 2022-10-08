using CoreLibrary.Utilities;
using MediatR;
using Contact_Microservice.Entities;
using Contact_Microservice.DataAccess.Abstract;

namespace Contact_Microservice.Business.Handlers.Persons.Commands
{
    public class CreatePersonCommand : IRequest<ApiDataResult<bool>>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }

        public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, ApiDataResult<bool>>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IMediator _mediator;
            public CreatePersonCommandHandler(IPersonRepository personRepository, IMediator mediator)
            {
                _personRepository = personRepository;
                _mediator = mediator;
            }

            public async Task<ApiDataResult<bool>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var person = new Person() {Company = request.Company,Name= request.Name, SurName = request.SurName,Id = Guid.NewGuid()};
                    var result = _personRepository.Add(person);
                    await _personRepository.SaveChangesAsync();
                    return new ApiDataResult<bool>(true, "Success");
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<bool>(false, "Error", ex.Message);                   
                }
            }
        }

    }
}
