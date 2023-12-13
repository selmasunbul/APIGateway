using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModel
{
    public class ComminicationModel
    {
        public Guid InfoTypeId { get; set; }
        public Guid PersonId { get; set; }
        public string Content { get; set; } = "";



    }
}
