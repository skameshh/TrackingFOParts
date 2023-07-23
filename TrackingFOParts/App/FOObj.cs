using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFOParts.App
{
    public class FOObj
    {
        public String VendorId { get; set; }
        public String ConfNumber { get; set; }
        public DateTime RecvdOn { get; set; }

        public String Step1 { get; set; } = "OFF";
        public String Step2 { get; set; } = "OFF";
        public String Step3 { get; set; } = "OFF";
        public String Step4 { get; set; } = "OFF";
        public String Step5 { get; set; } = "OFF";

    }
}
