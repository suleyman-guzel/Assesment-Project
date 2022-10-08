using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.DataAccess.DbContexts;
using Contact_Microservice.Entities;
using CoreLibrary.DataAccess.EntityFramework;

namespace Contact_Microservice.DataAccess.Concrete
{
    public class PersonRepository : EfEntityRepositoryBase<Person, PgDbContext>, IPersonRepository
    {
        public PersonRepository(PgDbContext context) : base(context)
        {

        }
    }
}
