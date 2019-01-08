using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcphSMSLib
{
    public  class Message
    {
       public int Id { get; set; }
        public string Sender { get; set; }
        public string Destination { get; set; }
        public string MessageText { get; set; }
        public MessageMode Mode { get; set; }
        public DateTime Sended { get; set; }
        public DateTime Recived { get; set; }
        public bool IsSended { get; set; }
        public bool IsRecive { get; set; }
        public bool IsRead { get; set; }
        public bool IsCompleteSend { get; set; }
        public bool IsCompleteRecive { get; set; }
    }
}
