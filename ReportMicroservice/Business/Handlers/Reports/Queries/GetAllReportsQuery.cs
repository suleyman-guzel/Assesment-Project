using CoreLibrary.Utilities;
using MediatR;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Entities;

namespace ReportMicroservice.Business.Handlers.Reports.Queries
{
    public class GetAllReportsQuery: IRequest<ApiDataResult<IEnumerable<Report>>>
    {
        public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, ApiDataResult<IEnumerable<Report>>>
        {
            private readonly IReportRepository _reportRepository;
            private readonly IMediator _mediator;

            public GetAllReportsQueryHandler(IReportRepository reportRepository, IMediator mediator)
            {
                _reportRepository = reportRepository;
                _mediator = mediator;
            }

            public async Task<ApiDataResult<IEnumerable<Report>>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var reportList = await _reportRepository.GetListAsync();
                    return new ApiDataResult<IEnumerable<Report>>("Success",reportList,true);
                }
                catch (Exception ex)
                {
                    return new ApiDataResult<IEnumerable<Report>>(false,"Error",ex.Message);
                }
            }
        }
    }
}
