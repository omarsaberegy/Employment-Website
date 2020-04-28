using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteZft.Models;

[assembly: OwinStartupAttribute(typeof(WebsiteZft.Startup))]
namespace WebsiteZft
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers () 
       
        {

            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if(!rolemanager.RoleExists("Administrator"))
            {

                //role creation
                role.Name = "Administrator";
                rolemanager.Create(role);


                //user of type admin creation.
                 //var user = new ApplicationUser() { UserName = model.UserName , UserType=model.UserType }
                ApplicationUser user = new ApplicationUser();
                user.UserName = "OmarSaberRef";
                user.UserType = "Administrator";
                var check = usermanager.Create(user, "omar12344");
                if(check.Succeeded)
                {

                    usermanager.AddToRoleAsync(user.Id,user.UserType);
                }




            }


        }

    }
}
