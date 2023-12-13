using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModel
{
    public class RaportModel
    {
        public string Location { get; set; } = "";
        public int PersonCount { get; set; }
        public int PhoneNoCount { get; set; }
        public string? RaportStatus { get; set; } = "";
        public DateTime RequestDate { get; set; }
    }
}
