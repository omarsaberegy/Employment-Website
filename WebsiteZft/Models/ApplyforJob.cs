using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteZft.Models;
using System.ComponentModel.DataAnnotations;

namespace WebsiteZft.Models
{
    public class ApplyforJob
    {
        public int Id { get; set; }

        public string Message { get; set; }
         [Display(Name="C.V")]
         [DataType(DataType.Upload)]
        public string CV { get; set; }

        public DateTime ApplyDate { get; set; }

        public int JobId { get; set; }

        public string UserId { get; set; }

        public virtual Job job { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}