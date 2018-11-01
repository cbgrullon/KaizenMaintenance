using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen_Maintenance.Models;

namespace Kaizen_Maintenance.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            var user = new UserSearchViewModel();
            user.Posted = false;
            user.SearchText = User.Identity.Name;
            return View(user);
        }
        [HttpPost]
        public ActionResult Index(UserSearchViewModel user)
        {
            using (var UC = new UserControl())
            {
                var userFinded = UC.FindUsersByUserName(user.SearchText);
                user.Posted = true;
                if (userFinded != null) user.Results.Add(userFinded);
                return View(user);
            }
        }
        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            using (var UC = new UserControl())
            {
                var userFinded = UC.FindUserById(id);

                return View(userFinded);
            }
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult DisableUser(string Id)
        {
            using (var UC = new UserControl())
            {
                UC.DisableUser(Id);
            }
            return RedirectToAction("Index");
        }
        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                using (var UC = new UserControl())
                {
                    var user = new User();
                    user.UserName = userVM.UserName;
                    user.Password = userVM.Password;
                    UC.CreateUser(user);
                }
                return RedirectToAction("Index");
                // TODO: Add insert logic here
            }
            return View(userVM);
        }



        // GET: Usuarios/Edit/5
        public ActionResult ManageRoles(string id)
        {
            using (var UC = new UserControl())
            {
                var user = UC.FindUserById(id);
                if (user != null)
                {
                    var vm = new ManageRolesViewModel();
                    vm.UserName = user.UserName;
                    vm.UserId = user.UserId;
                    vm.SelectedRol = UC.GetUserRole(id);
                    vm.Roles = UC.GetRoles();
                    return View(vm);
                }
                return RedirectToAction("Index");
            }
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(ManageRolesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var UC = new UserControl())
                {
                    UC.ChangeUserRole(vm.UserId, vm.SelectedRol);
                    return RedirectToAction("Index");
                }
            }
            return View(vm);
        }

        // GET: Usuarios/Delete/5
        public ActionResult ChangePassword(string id)
        {
            using (var UC = new UserControl())
            {
                var resetPassVm = new ResetPasswordViewModel();
                var user = UC.FindUserById(id);
                resetPassVm.UserId = user.UserId;
                resetPassVm.Usuario = user.UserName;
                return View(resetPassVm);
            }
            
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid)
            {
                using (var UC = new UserControl())
                {
                    var user = UC.FindUserById(reset.UserId);
                    UC.ResetUserPassword(user,reset.Password);
                }
                return RedirectToAction("Index");
            }
            return View(reset);
        }
    }
}
