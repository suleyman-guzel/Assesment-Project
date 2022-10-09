using CoreLibrary.Utilities;
using MediatR;
using Newtonsoft.Json;
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
            public Task<bool> Handle(CreateReportOfPeopleByLocationCommand request, CancellationToken cancellationToken)
            {
                var client = new RestClient("http://host.docker.internal:5000/api/Report/getReportofpeoplebylocation");
                var reportrequest = new RestRequest().AddQueryParameter("location", request.Report.Parameter);
                //var result = client.Get(reportrequest);
                var response = client.Execute(reportrequest);
                //var result = JsonConvert.DeserializeObject<ApiDataResult<IEnumerable<PersonDto>>>(response.Content);
                var result = client.Get<ApiDataResult<IEnumerable<PersonDto>>>(reportrequest);
                return Task.FromResult(true);
            }
        }
    }
}
