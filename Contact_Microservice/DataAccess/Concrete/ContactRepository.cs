using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.DataAccess.DbContexts;
using Contact_Microservice.Entities;
using CoreLibrary.DataAccess.EntityFramework;

namespace Contact_Microservice.DataAccess.Concrete
{
    public class ContactRepository : EfEntityRepositoryBase<Contact, PgDbContext>, IContactRepository
    {
        public ContactRepository(PgDbContext context) : base(context)
        {
        }
    }
}
