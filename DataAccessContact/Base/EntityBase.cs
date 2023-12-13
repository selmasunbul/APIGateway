using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataAccess.Base
{
    [DataContract]
    public class EntityBase : IEntityBase
    {
        [Key]
        [Column]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column]    
        public Guid? UserId { get; set; }


        [Column]   
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;


        [Column]   
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;


        [Column]    
        public bool IsDeleted { get; set; } = false;
    }
}
