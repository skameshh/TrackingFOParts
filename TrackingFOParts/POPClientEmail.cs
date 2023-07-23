using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFOParts
{
    [Serializable]
    public class POPClientEmail
    {
        public POPClientEmail()
        {
            this.Attachments = new List<Attachment>();
        }
        public int MessageNumber { get; set; }
        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public DateTime DateSent { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
}
