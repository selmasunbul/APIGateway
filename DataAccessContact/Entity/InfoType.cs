using DataAccess.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataAccess
{
    [DataContract]
    [Table("InfoType")]
    public class InfoType: EntityBase
    {
        [Column]
        [DataMember]
        [Display(Name = "Adı")]
        [Required]
        public string Name { get; set; } = "";

        [NotMapped]
        [InverseProperty("InfoType")]
        public virtual ICollection<Comminication>? Comminications { get; set; }

    }
}
