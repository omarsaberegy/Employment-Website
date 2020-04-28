using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebsiteZft.Models
{
    public class Job
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Job Name")]
        public string JobName { get; set; }

         [Display(Name = "Job Image")]
        public string JobImage  { get; set; }
         [AllowHtml]
         [Display(Name = "Job Requirements")]
        public string JobContent { get; set; }

         public string UserID { get; set; }

        [Display(Name="Categoty Name")]
         public int CategoryId { get; set; }


         public virtual Category Category { get; set; }
         public virtual ApplicationUser User { get; set; }



    }
}