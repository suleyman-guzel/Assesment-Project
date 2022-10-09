using CoreLibrary.Utilities.MessageBrokers;
using FluentAssertions;
using MediatR;
using Moq;
using ReportMicroservice.Business.Handlers.Reports.Queries;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Entities;
using System.Linq.Expressions;
using static ReportMicroservice.Business.Handlers.Reports.Queries.GetAllReportsQuery;
using static ReportMicroservice.Business.Handlers.Reports.Queries.ReportOfPeopleByLocationReportQuery;

namespace ReportMicroservice_Test
{
    public class Tests
    {
        Mock<IReportRepository> _reportRepository;
        Mock<IMessageBroker> _rabbitmq;
        private Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _reportRepository = new Mock<IReportRepository>();
            _mediator = new Mock<IMediator>();
            _rabbitmq = new Mock<IMessageBroker>();
        }

        [Test]
        public async Task Get_Reports_Success()
        {
            var query = new GetAllReportsQuery();

            _reportRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Report, bool>>>()))
                .ReturnsAsync(new List<Report>
                {
                    new () { State = ReportState.Tamamlandý,ReportUrl ="" }
                    
                });

            var handler = new GetAllReportsQueryHandler(_reportRepository.Object, _mediator.Object);
            var x = await handler.Handle(query, new CancellationToken());

            x.Success.Should().BeTrue();
            ((List<Report>)x.Data).Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Test]
        public async Task ReportOfPeopleByLocationReport_Send_Queue_Success()
        {
            var command = new ReportOfPeopleByLocationReportQuery();

            _reportRepository.Setup(x => x.Add(It.IsAny<Report>())).Returns(new Report() {Id=Guid.Parse("071165d5-6e92-4336-b672-4f5da56d5834"),ReportUrl="",CreateDate = DateTime.Now });
            _rabbitmq.Setup(x => x.MessagePublishObject<Report>(It.IsAny<Report>(), "ReportOfPeopleByLocation")).Verifiable();

            var handler = new ReportOfPeopleByLocationReportQueryHandler(_reportRepository.Object, _mediator.Object,_rabbitmq.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _reportRepository.Verify(x => x.SaveChangesAsync());
            
            x.Success.Should().BeTrue();
            x.Message.Should().Be("Success");
        }

        [Test]
        public async Task ReportOfPeopleByLocationReport_Send_Queue_Error()
        {
            var command = new ReportOfPeopleByLocationReportQuery();

            _reportRepository.Setup(x => x.Add(It.IsAny<Report>())).Returns(new Report() { Id = Guid.Parse("071165d5-6e92-4336-b672-4f5da56d5834"), ReportUrl = "", CreateDate = DateTime.Now });
            _rabbitmq.Setup(x => x.MessagePublishObject<Report>(It.IsAny<Report>(), "ReportOfPeopleByLocation")).Throws(new Exception ());

            var handler = new ReportOfPeopleByLocationReportQueryHandler(_reportRepository.Object, _mediator.Object, _rabbitmq.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _reportRepository.Verify(x => x.SaveChangesAsync());

            x.Success.Should().BeFalse();
            x.Message.Should().Be("Error");
        }
    }
}