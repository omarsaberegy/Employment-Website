using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebsiteZft.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Category Descreption")]
        public string CategoryDescreption { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}