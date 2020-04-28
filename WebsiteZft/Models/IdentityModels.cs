using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace WebsiteZft.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }
        public string UserImage { get; set; }
        public virtual ICollection<Job>  jobs { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<WebsiteZft.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<WebsiteZft.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<WebsiteZft.Models.ApplyforJob> ApplyforJobs { get; set; }


        

        

        

        

        

        

        

        

        
    }
}