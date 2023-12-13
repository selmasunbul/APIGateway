using DataAccess.Base;
using DataAccess.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataAccess
{
    [DataContract]
    [Table("Person")]
    public class Person: EntityBase
    {




        [Column]
        [DataMember]
        [Display(Name = "Adı")]
        [Required]
        public string Name { get; set; } = "";

        [Column]
        [DataMember]
        [Display(Name = "Soyadı")]
        [Required]
        public string Surname { get; set; } = "";
        
        [Column]
        [DataMember]
        [Display(Name = "Firma")]
        [Required]
        public string Firm { get; set; } = "";

        [NotMapped]
        [InverseProperty("Person")]
        public virtual ICollection<Comminication>? Comminications => GetComminications(this.Id);

        private List<Comminication> GetComminications(Guid PersonId)
        {
            using (DBContext ctx = new DBContext())
            {
                var internalComminicationList = ctx.InternalComminication
                    .Where(ic => ic.PersonId == PersonId)
                    .ToList();  

                if (internalComminicationList != null && internalComminicationList.Any())
                {
                    return internalComminicationList;
                }

                return new List<Comminication>(); 
            }
        }

    }
}