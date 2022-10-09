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


            //Antalyalı ünlüler
            var UUID = Guid.NewGuid();         
            modelBuilder.Entity<Person>().HasData(new Person {Company="Sanatcı a.ş",Name="Levent", SurName="Yüksel",Id= UUID});
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID, Contents = "antalya" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID, Contents = "05414414444" });

            var UUID1 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Futbol a.ş", Name = "Burak", SurName = "Yılmaz", Id = UUID1 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID1, Contents = "antalya" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID1, Contents = "05414414444" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID1, Contents = "05415555555" });

            var UUID2 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Oyuncu a.ş", Name = "Mehmet", SurName = "Özgür", Id = UUID2 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID2, Contents = "antalya" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID2, Contents = "05415552233" });




            //İzmirli Ünlüler
            var UUID3 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Oyuncu a.ş", Name = "Elçin", SurName = "Sangu", Id = UUID3 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID3, Contents = "izmir" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID3, Contents = "05411112233" });

            var UUID4 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Oyuncu a.ş", Name = "Selin", SurName = "Şekerci", Id = UUID4 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID4, Contents = "izmir" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID4, Contents = "05413332211" });

            var UUID5 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Oyuncu a.ş", Name = "Ece", SurName = "Uslu", Id = UUID5 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID5, Contents = "izmir" });




            //Urfalı ünlüler
            var UUID6 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Şarkıcı a.ş", Name = "İbrahim", SurName = "Tatlıses", Id = UUID6 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID6, Contents = "urfa" });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Phone, PersonUID = UUID6, Contents = "05417778899" });

            var UUID7 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Şarkıcı a.ş", Name = "Müslüm", SurName = "Gürses", Id = UUID7 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID7, Contents = "urfa" });

            var UUID8 = Guid.NewGuid();
            modelBuilder.Entity<Person>().HasData(new Person { Company = "Şarkıcı a.ş", Name = "Mahmut", SurName = "Tuncer", Id = UUID8 });
            modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.NewGuid(), ContactType = (int)ContactType.Location, PersonUID = UUID8, Contents = "urfa" });



            modelBuilder.Entity<Person>().Navigation(e => e.Contacts).AutoInclude();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {            
            options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString"));           
        }

    }
}
