using CoreLibrary.Entities;

namespace ReportMicroservice.Entities
{
    public class Report:IEntity
    {
        public Guid Id { get; set; }
        public string? ReportUrl { get; set; }
        public string Parameter { get; set; }
        public DateTime CreateDate { get; set; }
        public ReportState State { get; set; }
    }
}
