using CoreLibrary.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact_Microservice.Entities
{
    public class Person:IEntity
    {
        public Person()
        {
            if (Contacts == null)
            {
                Contacts = new List<Contact>();      
            }
        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }

        [InverseProperty(nameof(Contact.Person))]
        public virtual List<Contact> Contacts { get; set; }
    }
}
