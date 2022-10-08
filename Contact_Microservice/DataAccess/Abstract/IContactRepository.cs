using Contact_Microservice.Entities;
using CoreLibrary.DataAccess;

namespace Contact_Microservice.DataAccess.Abstract
{
    public interface IContactRepository:IEntityRepository<Contact>
    {
    }
}
