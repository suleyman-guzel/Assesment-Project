using Contact_Microservice.Business.Handlers.Contacts.Commands;
using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.Entities;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Contact_Microservice.Business.Handlers.Contacts.Commands.CreateContactCommand;
using static Contact_Microservice.Business.Handlers.Contacts.Commands.DeleteContactCommand;

namespace ContactMicroservice_Test
{
    public class ContactUnitTest
    {
        Mock<IContactRepository> _contactRepository;
        private Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _contactRepository = new Mock<IContactRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Contact_CreateCommand_Success()
        {
            Contact contact = null;

            var command = new CreateContactCommand();
            command.ContactType = (int)ContactType.Phone;
            command.Contents = "05414446633";

            

            _contactRepository.Setup(x => x.Add(It.IsAny<Contact>())).Returns(new Contact());

            var handler = new CreateContactCommandHandler(_contactRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _contactRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be("Contact Added");
        }

        [Test]
        public async Task Contact_CreateCommand_Error()
        {
            Contact contact = null;

            var command = new CreateContactCommand();
            command.ContactType = (int)ContactType.Phone;
            command.Contents = "05414446633";



            _contactRepository.Setup(x => x.Add(It.IsAny<Contact>())).Throws(new Exception ());

            var handler = new CreateContactCommandHandler(_contactRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());
            x.Success.Should().BeFalse();
            x.Message.Should().Be("Error");
        }

        [Test]
        public async Task Contact_DeleteCommand_Success()
        {

            var command = new DeleteContactCommand() { ContactId = Guid.Parse("0dd5d6e5-1c40-4eff-8ff0-f35a82d83ac5") };

            _contactRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                .ReturnsAsync(new Contact() { Id = Guid.Parse("0dd5d6e5-1c40-4eff-8ff0-f35a82d83ac5") });

            _contactRepository.Setup(x => x.Delete(It.IsAny<Contact>()));
            var handler = new DeleteContactCommandHandler(_contactRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _contactRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be("Contact Removed");
        }

        [Test]
        public async Task Contact_DeleteCommand_Empty_Contact()
        {

            var command = new DeleteContactCommand() { ContactId = Guid.Parse("0dd5d6e5-1c40-4eff-8ff0-f35a82d83ac5") };

            _contactRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                .Throws(new Exception ());

            _contactRepository.Setup(x => x.Delete(It.IsAny<Contact>()));
            var handler = new DeleteContactCommandHandler(_contactRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());
            x.Success.Should().BeFalse();
            x.Message.Should().Be("Error");
        }
    }
}
