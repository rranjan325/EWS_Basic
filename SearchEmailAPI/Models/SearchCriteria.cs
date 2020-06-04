using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEmailAPI.Models
{
    public class SearchCriteria
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ExchangeUrl { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SubjectPattern { get; set; }
    }
}
