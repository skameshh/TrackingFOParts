using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFOParts.DB
{
    public class FOStatusDao
    {
        public int Id { get; set; }
        public String VendorId { get; set; }
        public String ConfNumber { get; set; }
        public String CurrentStatus { get; set; }
        public DateTime LastUpdOn { get; set; }
    }
}
