using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class RaporRequestModel
    {
        public string Location { get; set; } = "";
        public int PersonCount { get; set; }
        public int PhoneNoCount { get; set; }
        public string? RaportStatus { get; set; } = "";
        public DateTime RequestDate { get; set; }
    }

    public class RaporModel
    {

        public string RaporDurumu { get; set; } = "";
        public string Konum { get; set; } = "";
        public int KisiSayisi { get; set; }
        public int TelefonNoSayisi { get; set; }
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class RaporStatus
    {
        public string Status { get; set; } = "";
    }
}
