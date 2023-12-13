using DataAccess.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DataAccess.Entity
{
    [DataContract]
    [Table("Raport")]
    public class Raport: EntityBase
    {
        [Column]
        [DataMember]
        [Display(Name = "Raport Durumu")]
        [Required]
        public string RaportStatus { get; set; } = "";

        [Column]
        [DataMember]
        [Display(Name = "Konum")]
        [Required]
        public string Location { get; set; } = "";
        
        [Column]
        [DataMember]
        [Display(Name = "Kişi Sayısı")]
        [Required]
        public int PersonCount { get; set; } 

        [Column]
        [DataMember]
        [Display(Name = "Kişi Sayısı")]
        [Required]
        public int PhoneNoCount { get; set; } 


    }
}
