using Contact_Microservice.Business.Handlers.Persons.Queries;
using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using MediatR;
using Moq;
using System.Linq.Expressions;
using NUnit.Framework;
using FluentAssertions;
using static Contact_Microservice.Business.Handlers.Persons.Queries.GetPersonQuery;
using static Contact_Microservice.Business.Handlers.Persons.Queries.GetPersonListQuery;
using Contact_Microservice.Business.Handlers.Persons.Commands;
using static Contact_Microservice.Business.Handlers.Persons.Commands.CreatePersonCommand;
using static Contact_Microservice.Business.Handlers.Persons.Commands.DeletePersonCommand;

namespace ContactMicroservice_Test
{
    public class PersonUnitTest
    {
        Mock<IPersonRepository> _personRepository;
        private Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _personRepository = new Mock<IPersonRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Get_Person_Success()
        {
            var query = new GetPersonQuery();
            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
               .ReturnsAsync(new Person());

            var handler = new GetPersonQueryHandler(_personRepository.Object, _mediator.Object);

            var x = await handler.Handle(query, new CancellationToken());
            x.Success.Should().BeTrue();
        }

        [Test]
        public async Task Get_Persons_Success()
        {
            var query = new GetPersonListQuery();

            _personRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(new List<Person>
                {
                    new () {Name ="Burak",SurName="Yýlmaz",Company="Futbol a.þ",Id=Guid.Parse("0cb78979-5935-4793-be77-3aae7cdc7c4a") },
                    new () {Name ="Elçin",SurName="Sangu",Company="Oyuncu a.þ",Id=Guid.Parse("24fb9dd1-9ca7-4bed-a95b-1dae06d4a4bb") }
                });

            var handler = new GetPersonListQueryHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(query, new CancellationToken());

            x.Success.Should().BeTrue();
            ((List<Person>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task Person_CreateCommand_Success()
        {
            Person person = null;

            var command = new CreatePersonCommand();
            command.Company = "Oyuncu a.þ";
            command.SurName = "Ýnanýr";
            command.Name = "Kadir";
                       
            _personRepository.Setup(x => x.Add(It.IsAny<Person>())).Returns(new Person());

            var handler = new CreatePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _personRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be("Success");
        }

        [Test]
        public async Task Person_CreateCommand_Error()
        {
            Person person = null;

            var command = new CreatePersonCommand();
            command.Company = "Oyuncu a.þ";
            command.SurName = "Ýnanýr";
            command.Name = "Kadir";

            _personRepository.Setup(x => x.Add(It.IsAny<Person>())).Throws(new Exception ());

            var handler = new CreatePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());
           
            x.Success.Should().BeFalse();
            x.Message.Should().Be("Error");
        }


        [Test]
        public async Task Person_DeleteCommand_Success()
        {

            var command = new DeletePersonCommand();

            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(new Person()
                {
                        Company="Oyuncu a.þ",
                        Name =  "Tarýk",
                        SurName = "Akan"
                });

            _personRepository.Setup(x => x.Delete(It.IsAny<Person>()));
            var handler = new DeletePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _personRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be("success");
        }

        [Test]
        public async Task Person_DeleteCommand_Error()
        {

            var command = new DeletePersonCommand();

            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .Throws(new Exception ());

            _personRepository.Setup(x => x.Delete(It.IsAny<Person>())).Throws(new Exception ());

            var handler = new DeletePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            
            x.Success.Should().BeFalse();
            x.Message.Should().Be("Error");
        }

    }
}