using CoreLibrary.Entities;
using CoreLibrary.Utilities;
using CoreLibrary.Utilities.Converters.ToExcel;
using MediatR;
using Newtonsoft.Json;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.DTO;
using ReportMicroservice.Entities;
using RestSharp;

namespace ReportMicroservice.Business.Handlers.Reports.Commands
{
    public class CreateReportOfPeopleByLocationCommand: IRequest<bool>
    {
        public Report Report { get; set; }

        public class CreateReportOfPeopleByLocationCommandHandler : IRequestHandler<CreateReportOfPeopleByLocationCommand, bool>
        {
            private readonly ToExcelConverter _excelConverter;

            IServiceScopeFactory _serviceScopeFactory;
            public CreateReportOfPeopleByLocationCommandHandler(IServiceScopeFactory serviceScopeFactory)
            {
                _excelConverter = new ToExcelConverter();
                _serviceScopeFactory = serviceScopeFactory;
               
            }
            public async Task<bool> Handle(CreateReportOfPeopleByLocationCommand request, CancellationToken cancellationToken)
            {
                var client = new RestClient("http://host.docker.internal:5000/api/Report/getReportofpeoplebylocation");
                var reportrequest = new RestRequest();                 
                var result = client.Get<ApiDataResult<IEnumerable<ReportOfLocation>>>(reportrequest);
                string fullpath = _excelConverter.ConvertToExcel<ReportOfLocation>(result.Data,"./ReportFiles/",request.Report.Id.ToString());


                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _reportRepository = scope.ServiceProvider.GetRequiredService<IReportRepository>();

                    var report = _reportRepository.Get(x => x.Id == request.Report.Id);
                    report.ReportUrl = fullpath;
                    report.State = ReportState.Complated;
                    _reportRepository.Update(report);
                    await _reportRepository.SaveChangesAsync();
                }
                return await Task.FromResult(true);
            }
        }
    }
}
