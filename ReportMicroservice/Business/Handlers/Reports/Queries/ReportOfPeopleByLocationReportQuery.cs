
using CoreLibrary.Utilities;
using CoreLibrary.Utilities.MessageBrokers;
using MediatR;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Entities;
using RestSharp;

namespace ReportMicroservice.Business.Handlers.Reports.Queries
{
    public class ReportOfPeopleByLocationReportQuery : IRequest<ApiDataResult<Report>>
    {      

        public class ReportOfPeopleByLocationReportQueryHandler : IRequestHandler<ReportOfPeopleByLocationReportQuery, ApiDataResult<Report>>
        {
            private readonly IReportRepository _reportRepository;
            private readonly IMediator _mediator;
            private readonly IMessageBroker _rabbitMq;

            public ReportOfPeopleByLocationReportQueryHandler(IReportRepository reportRepository, IMediator mediator, IMessageBroker rabbitMq)
            {
                _reportRepository = reportRepository;
                _mediator = mediator;
                _rabbitMq = rabbitMq;
            }
            public async Task<ApiDataResult<Report>> Handle(ReportOfPeopleByLocationReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    Report _report = new Report { CreateDate = DateTime.Now, Id = Guid.NewGuid(), State = ReportState.Hazırlanıyor };
                    _report = _reportRepository.Add(_report);
                    await _reportRepository.SaveChangesAsync();
                    _rabbitMq.MessagePublishObject<Report>(_report, "ReportOfPeopleByLocation");

                    return new ApiDataResult<Report>("Success", _report, true);
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<Report>(false, "Error", ex.Message);
                }
            }
        }
    }
}
