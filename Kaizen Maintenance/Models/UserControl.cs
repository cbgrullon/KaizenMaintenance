using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaizen_Maintenance.Models
{
    public class UserControl: IDisposable
    {
        private ApplicationDbContext Context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public UserControl()
        {
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            Context = new ApplicationDbContext();
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
        }
        public User FindUserById(string Id)
        {
            var user = new User();
            var userFinded = userManager.FindById(Id);
            if (userFinded == null) return null;
            user.UserId = userFinded.Id;
            user.UserName = userFinded.UserName;
            user.Roles.AddRange(userFinded.Roles.Select(x => x.RoleId));
            return user;
        }
        public void CreateUser(User user)
        {
            ApplicationUser appUser = new ApplicationUser();
            appUser.UserName = user.UserName;
            appUser.Email = "";
            userManager.Create(appUser, user.Password);
        }
        public void ChangePassword(User user, string NewPassword)
        {
            userManager.ChangePassword(user.UserId, user.Password, NewPassword);
        }
        public void ChangeUserRole(string UserId, string newRole)
        {
            var UserRoles = userManager.GetRoles(UserId).ToArray();
            userManager.RemoveFromRoles(UserId,UserRoles);
            userManager.AddToRole(UserId,newRole);
        }
        //public void AddRoleToUser(string Role, User user)
        //{
        //    userManager.AddToRole(user.UserId, Role);
        //}
        public UserViewModel FindUsersByUserName(string UserName)
        {
            var user = new UserViewModel();
            var userFinded = userManager.FindByName(UserName);
            user.UserId = userFinded.Id;
            user.UserName = userFinded.UserName;
            return user;
        }
        public void ResetUserPassword(User user, string NewPassword)
        {
            var token = userManager.GeneratePasswordResetToken(user.UserId);
            userManager.ResetPassword(user.UserId,token,NewPassword);
        }
        public List<Role> GetRoles()
        {
            return roleManager.Roles.Select(x => new Role { Id=x.Id,Name=x.Name}).ToList();
        }
        public string GetUserRole(string UserId)
        {
            return userManager.GetRoles(UserId).FirstOrDefault();

        }
        public void Dispose()
        {
            roleManager.Dispose();
            userManager.Dispose();
            Context.Dispose();
        }
    }
}