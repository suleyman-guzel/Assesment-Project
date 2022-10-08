using CoreLibrary.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact_Microservice.Entities
{
    public class Contact:IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int ContactType { get; set; }
        public string Contents { get; set; }
       
        public Guid PersonUID { get; set; }
        [ForeignKey("PersonUID")]
        public virtual Person Person { get; set; }
    }
}
