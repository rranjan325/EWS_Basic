using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEmail.Models
{
    public class EmailItem
    {
        public string Id { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string Subject { get; set; }
        public string DisplayTo { get; set; }
        public string DisplayCc { get; set; }

        public string Sender { get; set; }
    }
}
