using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteZft.Models
{
    public class Contact
    {
        [Required]
        public string MessengerName { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string MessageSubject { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
    }
}