using Contact_Microservice.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Contact_Microservice.DataAccess.DbContexts
{
    public class PgDbContext:DbContext
    {
        protected readonly IConfiguration Configuration;

        public PgDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Contacts)
                .WithOne(b => b.Person);
          
            var UUID = Guid.NewGuid();
            var contact = new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID, Contents = "12312312312" };

           
            modelBuilder.Entity<Person>().HasData(new Person {Company="gozili a.ş1",Name="süleyman", SurName="güzel",Id= UUID});
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID, Contents = "12312312312" });
            modelBuilder.Entity<Person>().Navigation(e => e.Contacts).AutoInclude();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {            
            options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString"));
            //Database.Migrate();
        }

    }
}
