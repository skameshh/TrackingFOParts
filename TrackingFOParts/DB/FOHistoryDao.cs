using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFOParts.DB
{
    public class FOHistoryDao
    {
        public int Id { get; set; }
        public String VendorId { get; set; }
        public String ConfNumber { get; set; }
        public DateTime PartRecdOn { get; set; }
        public DateTime PartProcStartedOn { get; set; }
        public DateTime PartProcEndOnOn { get; set; }
        public DateTime PartQCDoneOnOn { get; set; }
        public DateTime PartSentDelvOn { get; set; }
        public DateTime LastUpdOn { get; set; }

    }
}
