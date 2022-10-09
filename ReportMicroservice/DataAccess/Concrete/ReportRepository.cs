using CoreLibrary.DataAccess.EntityFramework;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.DataAccess.DbContexts;
using ReportMicroservice.Entities;

namespace ReportMicroservice.DataAccess.Concrete
{
    public class ReportRepository : EfEntityRepositoryBase<Report, PgDbContext>, IReportRepository
    {
        public ReportRepository(PgDbContext context) : base(context)
        {
        }
    }
}
