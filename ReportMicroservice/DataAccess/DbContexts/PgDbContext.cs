using ReportMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ReportMicroservice.DataAccess.DbContexts
{
    public class PgDbContext:DbContext
    {
        protected readonly IConfiguration Configuration;

        public PgDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DbSet<Report> Reports { get; set; }       


      
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {            
            options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString"));           
        }

    }
}
