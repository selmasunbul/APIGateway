using DataAccess.Base;
using DataAccess.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataAccess
{
    [DataContract]
    [Table("Comminication")]
    public class Comminication: EntityBase
    {
        [Column]
        [DataMember]
        [Display(Name = "Bilgi Tipi")]
        [Required]
        public Guid InfoTypeId { get; set; }


        [Column]
        [DataMember]
        [Display(Name = "Kişi")]
        [Required]
        public Guid PersonId { get; set; }


        [Column]
        [DataMember]
        [Display(Name = "İçerik")]
        [Required]
        public string Content { get; set; } = "";

        [NotMapped]
        [DataMember(IsRequired = false)]
        [ForeignKey("InfoTypeId")]
        public virtual InfoType? InfoType { get; set; }



        [NotMapped]
        public virtual string InfoTypeName => GetInfoTypeName(this.InfoTypeId);

        private string GetInfoTypeName(Guid InfoTypeId)
        {
            using (DBContext ctx = new DBContext())
            {
                var InternalInfoType = ctx.InternalInfoType
                    .Where(ic => ic.Id == InfoTypeId)
                    .Select(x => x.Name)
                    .FirstOrDefault();  

                return InternalInfoType ?? ""; 
            }
        }

        [NotMapped]
        [DataMember(IsRequired = false)]
        [ForeignKey("PersonId")]
        public virtual Person? Person { get; set; }
    }
}
