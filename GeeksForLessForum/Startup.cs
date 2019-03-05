using GeeksForLessForum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeeksForLessForum.Startup))]
namespace GeeksForLessForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRoles();
        }

        private void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(ApplicationDbContext.Create()
                ));

            var UserManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(ApplicationDbContext.Create()
                ));

            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "SuperHero";
                user.Email = "kvv.login@gmail.com";

                string userPWD = "035K44v791V";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating  Manager role     
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
