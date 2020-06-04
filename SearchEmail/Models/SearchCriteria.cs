using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEmail.Models
{
    
    public class SearchCriteria
    {
        
        [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; }

        
        [Required]
        public string Password { get; set; }

        
        [Required]
        public string ExchangeUrl { get; set; }

        
        [Required]
        public DateTime FromDate { get; set; }

        
        [Required]
        public DateTime ToDate { get; set; }

        
        [Required]
        public string SubjectPattern { get; set; }

    }
}
